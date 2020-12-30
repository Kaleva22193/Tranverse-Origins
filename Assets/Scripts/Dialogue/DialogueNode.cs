using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool isPlayerSpeaking = false;  //enum for maore than one player and ai?
        [SerializeField] [TextArea]string text = null;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(10, 10, 300, 100);
        [SerializeField] string onEnterAction = null;
        [SerializeField] string onExitAction = null;

        public Rect GetRect()
        {
            return rect;
        }
        public string GetText()
        {
            return text;
        }
        public List<string> GetChildren()
        {
            return children;
        }
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
        public string GetOnEnterAction()
        {
            return onEnterAction;
        }
        public string GetOnExitAction()
        {
            return onExitAction;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Child Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Child Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }


#endif
    }
}
