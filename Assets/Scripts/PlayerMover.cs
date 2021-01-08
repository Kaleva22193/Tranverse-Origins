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
        RoomLocation currentLocation;

        public event Action onLocationUpdated;
        
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
        public IEnumerable<RoomLocation> GetDirections()
        {
            return currentRoom.GetPlayerChildren(currentLocation);
        }
        public void SelectMove(RoomLocation chosenLocation)
        {
            currentLocation = chosenLocation;
            TriggerEnterAction();
            Next();
        }
        public void Next()
        {

        }
    }
}

