using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

namespace RPG.Rooms
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] Room testRoom;
        Room currentRoom;
        RoomLocation currentLocation = null;
        bool isMoving = false;

        public event Action onLocationUpdated;


        IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            StartRoom(testRoom);
        }

        public void StartRoom(Room newRoom)
        {
            currentRoom = newRoom;
            currentLocation = currentRoom.GetRootNode();
            TriggerEnterAction();
            onLocationUpdated();
        }
        public void LeaveRoom()
        {
            TriggerExitAction();
            currentLocation = null;
            currentRoom = null;
            onLocationUpdated();
        }
        public bool IsActive()
        {
            return currentLocation != null;
        }
        public string GetText()
        {
            if (currentLocation == null)
            {
                return "";
            }
            return currentLocation.GetText();
        }
        public IEnumerable<RoomLocation> GetChoices()
        {
            return currentRoom.GetPlayerChildren(currentLocation);
        }
        public void SelectMove(RoomLocation chosenLocation)
        {
            currentLocation = chosenLocation;
            if (chosenLocation.HasNPC())
            {
                //psst DialogeUI here is a dialogue...
            }
            //isMoving = true;
            //TriggerEnterAction();            
        }
        private void TriggerEnterAction()
        {
            if (currentLocation != null)
            {
                isMoving = false;
                TriggerAction(currentLocation.GetOnEnterAction());
            }
        }
        public List<string> GetMovementDirections()
        {
            if (currentLocation != null)
            {
                return currentLocation.MovementDirections();
            }
            List<string> noDirection = new List<string>();
            return noDirection;
        }
        private void TriggerAction(string action)
        {
            if (action == "")
            {
                return;
            }
            //foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            //{
            //    trigger.Trigger(action);
            //}
        }
        private void TriggerExitAction()
        {
            if (currentLocation != null)
            {
                isMoving = false;
                TriggerAction(currentLocation.GetOnExitAction());
            }
        }
        public bool IsMoving()
        {
            return isMoving;
        }
        public bool HasNPC()
        {
            return currentLocation.HasNPC();
        }
    }
}

