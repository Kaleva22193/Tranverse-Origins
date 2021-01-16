using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Rooms;


namespace RPG.UI
{
    public class RoomUI : MonoBehaviour
    {
        PlayerMover playerMover;
        //[SerializeField] GameObject directionPreFab = null;
        //[SerializeField] Transform directionRoot = null;
        [SerializeField] GameObject[] moveButtons;
        [SerializeField] TextMeshProUGUI roomNarration;

        //Comment for git change

        // Start is called before the first frame update
        void Start()
        {
            playerMover = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMover>();
            playerMover.onLocationUpdated += UpdateUI;

            


            UpdateUI();
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerMover.IsActive());
            if (!playerMover.IsActive())
            {
                return;
            }

            if (!playerMover.IsMoving())
            {
                Debug.Log("UI Updated");
                //BuildMovementButtons();
                DisplayMoveChoices();
            }
            else
            {
                //...Coming soon....
            }

        }

        private void DisplayMoveChoices()
        {
            //look at current location for list of movement directions
            Debug.Log(playerMover.GetText());
            roomNarration.text = playerMover.GetText();

            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(false);
            }

            int listCounter = 0;
            foreach (string direction in playerMover.GetMovementDirections())
            {
                Debug.Log(listCounter);
                Debug.Log(direction);
                if (direction == Directions.NorthWest.ToString())
                {
                    ButtonImageSetterOn(0, listCounter);
                }
                else if (direction == Directions.North.ToString())
                {
                    ButtonImageSetterOn(1, listCounter);
                }
                else if (direction == Directions.NorthEast.ToString())
                {
                    ButtonImageSetterOn(2, listCounter);
                }
                else if (direction == Directions.West.ToString())
                {
                    ButtonImageSetterOn(3, listCounter);
                }
                else if (direction == Directions.East.ToString())
                {
                    ButtonImageSetterOn(5, listCounter);
                }
                else if (direction == Directions.SouthEast.ToString())
                {
                    ButtonImageSetterOn(6, listCounter);
                }
                else if (direction == Directions.South.ToString())
                {
                    ButtonImageSetterOn(7, listCounter);
                }
                else if (direction == Directions.SouthWest.ToString())
                {
                    ButtonImageSetterOn(8, listCounter);
                }
                listCounter++;               
            }
        }
        
        private void ButtonImageSetterOn(int number, int counter)
        {
            if(moveButtons.Length > number)
                moveButtons[number].SetActive(true);
        }

        private void LinkDirectiontoLocation(int number)
        {
            List<RoomLocation> children = playerMover.GetChoices().ToList();
            for (int i = 0; i < children.Count; i++)
            {
                if (i == number)
                {
                    playerMover.SelectMove(children[i]);
                    UpdateUI();
                }
            }
        }

        public void MoveDirection(int dir)
        {
            Directions direction = (Directions) dir;

            Debug.Log("Moving : " + direction.ToString());

            int roomIndex = 0;
            foreach (string moveDirection in playerMover.GetMovementDirections())
            {
                Debug.Log(moveDirection);
                if (moveDirection == direction.ToString())
                {
                    Debug.Log("room index is: " + roomIndex);
                    if(playerMover.GetMovementDirections().Count <= roomIndex)
                    {
                        Debug.LogError("Room index is missing from playerMover.GetMovementDirections()!!", playerMover);
                        return;
                    }
                    playerMover.SelectMove(playerMover.GetChoices().ElementAt(roomIndex));
                }
                roomIndex++;
            }
            UpdateUI();
        }

        //private void BuildMovementButtons()
        //{
        //    foreach(Transform item in directionRoot)
        //    {
        //        Destroy(item.gameObject);
        //    }

        //    foreach (RoomLocation choice in playerMover.GetChoices())
        //    {
        //        GameObject choiceInstance = Instantiate(directionPreFab, directionRoot);
        //        foreach (Transform child in choiceInstance.transform)
        //        {
        //            child.gameObject.SetActive(true);
        //        }
        //        var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();

        //        textComp.text = choice.GetText();

        //        Button button = choiceInstance.GetComponentInChildren<Button>();
        //        button.gameObject.SetActive(true);
        //        button.onClick.AddListener(() =>
        //        {
        //            playerMover.SelectMove(choice);
        //            UpdateUI();
        //        });
        //    }
        //}
    }
}
