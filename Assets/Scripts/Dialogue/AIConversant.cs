using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;
        [SerializeField] PlayerController player;

        public void StartTheConversation()
        {
            //Set room narration panel to inactive
            StartConversation(player);

        }
        public bool StartConversation(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }
            else //if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }
    }
}
