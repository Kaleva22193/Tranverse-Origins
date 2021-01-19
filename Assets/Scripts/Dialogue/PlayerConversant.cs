using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using RPG.UI;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        
        AIConversant currentConversant = null;
        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        public event Action onConversationUpdated;

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            Debug.Log("onConversationUpdated invoke has: " + onConversationUpdated.GetInvocationList().Length);
            onConversationUpdated();            
        }
        public void QuitDialogue()
        {
            TriggerExitAction();
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
        }
        public bool IsActive()
        {
            return currentDialogue != null;
        }
        public bool IsChoosing()
        {
            return isChoosing;
        }
        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            return currentNode.GetText();
        }
        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }
        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
            Next();
        }
        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int response = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode = children[response];
            TriggerEnterAction();
            onConversationUpdated();
        }
        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
        private void TriggerEnterAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());               
            }
        }
        private void TriggerExitAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }
        private void TriggerAction(string action)
        {
            if (action == "")
            {
                return;
            }
            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}
