using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Rooms
{
    
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] Room roomObject;
   
        [SerializeField] List<string> actions = new List<string>();
        [SerializeField] List<string> locations = new List<string>();
        

        //int roomLocation;

        //Move Buttons
        [SerializeField] GameObject movePanel;
        [SerializeField] GameObject[] moveButtons;

        
        // Start is called before the first frame update
        void Start()
        {
                       
        }

        // Update is called once per frame
        void Update()
        {
  
        }
        void UpdateUI()
        {

        }

        public void SetMoveButtons(int roomLocation)
        {
            movePanel.SetActive(false);
            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(false);
            }
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
