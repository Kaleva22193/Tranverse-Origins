using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Rooms
{
    
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] Room roomObject;
        //public Vector2Int playerLocation;
        [SerializeField] List<string> actions = new List<string>();
        [SerializeField] List<string> locations = new List<string>();
        

        //int roomLocation;

        //Move Buttons
        [SerializeField] GameObject movePanel;
        [SerializeField] GameObject[] moveButtons;

        
        // Start is called before the first frame update
        void Start()
        {
            //CreateRoomMap(roomObject.RoomWidth(), roomObject.RoomLength());            
        }

        // Update is called once per frame
        void Update()
        {
            //ValidMoves(playerLocation);
            //SetMoveButtons(roomLocation);
        }
        public int[,] CreateRoomMap(int width, int length)
        {
            int[,] roomMap = new int[width, length]; //Maybe add dimensions for floors for different floors

            return roomMap;
        }

        //public void ValidMoves(string location)
        //{
        //    // Determine the corners
        //    // Refernce for the max comun letter
        //    // maybe not needed anymore string columnMax = HorizontalRowLetters(roomObject.RoomLength());
        //    // maybe not needed anymore string maxMinusOne = HorizontalRowLetters(roomObject.RoomLength() - 1);

        //    // Corners are 0A, 0roomLength, roomWidthA, roomWidth-roomLength
        //    if (location == "0A")
        //    {
        //        //Debug.Log("Lower Left Corner");
        //        roomLocation = 1;
        //        return;
        //    }
        //    else if (location == "0" + columnMax)
        //    { 
        //        //Debug.Log("Lower Right Corner");
        //        roomLocation = 2;
        //        return;
        //    }
        //    else if (location == roomObject.RoomWidth() - 1  + columnMax)
        //    {
        //        //Debug.Log("Upper Right Corner");
        //        roomLocation = 3;
        //        return;
        //    }
        //    else if (location == roomObject.RoomWidth() - 1 + "A")
        //    {
        //        //Debug.Log("Upper Left Corner");
        //        roomLocation = 4;
        //        return;
        //    }
        //    else if (location.IndexOf('0') == 0)
        //    {
        //        //South Wall facing North
        //        roomLocation = 5;
        //        return;
        //    }
        //    else if (location.IndexOf(columnMax) == 1)
        //    {
        //        //East Wall facing west
        //        roomLocation = 6;
        //        return;
        //    }
        //    else if (location.IndexOf((roomObject.RoomWidth() - 1).ToString()) == 0)
        //    {
        //        //North Wall facing South
        //        roomLocation = 7;
        //        return;
        //    }
        //    else if (location.IndexOf('A') == 1)
        //    {
        //        //West Wall facing East
        //        roomLocation = 8;
        //        return;
        //    }

        //    roomLocation = 9;
        //    return;
        //}

        public void SetMoveButtons(int roomLocation)
        {
            movePanel.SetActive(false);
            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(false);
            }
            //Debug.Log(roomLocation);
            if(roomLocation > 0)
            {
                movePanel.SetActive(true);
                switch (roomLocation)
                {
                    case 1:  
                        ButtonImageSetterOn(1, 2, 5);
                        break;
                    case 2:
                        ButtonImageSetterOn(0, 1, 3);
                        break;
                    case 3:
                        ButtonImageSetterOn(3, 6, 7);
                        break;
                    case 4:
                        ButtonImageSetterOn(5, 7, 8);
                        break;
                    case 5:
                        ButtonImageSetterOn(0, 1, 2, 3, 5);
                        break;
                    case 6:
                        ButtonImageSetterOn(0, 1, 3, 6, 7);
                        break;
                    case 7:
                        ButtonImageSetterOn(3, 5, 6, 7, 8);
                        break;
                    case 8:
                        ButtonImageSetterOn(1, 2, 5, 7, 8);
                        break;
                    case 9:
                        ButtonImageSetterOn();
                        break;

                }
            }
        }
        public void ButtonImageSetterOn()
        {
            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(true);
                Image buttonImage = moveButtons[i].GetComponent<Image>();
                buttonImage.enabled = true;
            }
        }
        public void ButtonImageSetterOn(int one, int two, int three)
        {
            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(true);
                Image buttonImage = moveButtons[i].GetComponent<Image>();
                if (i == one || i == two || i == three)
                {
                    buttonImage.enabled = true;
                }
                else
                {
                    buttonImage.enabled = false;
                }
            }
        }
        public void ButtonImageSetterOn(int one, int two, int three, int four, int five)
        {
            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(true);
                Image buttonImage = moveButtons[i].GetComponent<Image>();
                if (i == one || i == two || i == three || i == four || i == five)
                {
                    buttonImage.enabled = true;
                }
                else
                {
                    buttonImage.enabled = false;
                }
            }
        }

    }
}
