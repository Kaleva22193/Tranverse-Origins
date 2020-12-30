using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Rooms
{
    public class PlayerMover : MonoBehaviour
    {
        RoomManager roomManager;
        public Vector2 playerLocation;
        string roomName;
        string roomLocation = "Entrance";
        List<string> roomTriggers = new List<string>();
        [SerializeField] GameObject narratorPanel;



        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void MovePlayerPosition(Vector2Int position)
        {
            // Move North

            // Move East 
            // Move South
        }        
        public void PopulateRoomTriggers(string room)
        {
            if (room == "Entrance")
            {
                //Actions
                narratorPanel.SetActive(true);
                /*narrator panel text populate*/
                //Movement directions
                //NPC's
                //interactable Objects
                //static objects
                //traps

            }
        }
    }
}

