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
        [SerializeField] GameObject[] moveButtons;
        [SerializeField] TextMeshProUGUI roomNarration;

        [SerializeField] GameObject investigateButton;
        [SerializeField] GameObject talkButton;

        // Start is called before the first frame update
        void Start()
        {
            playerMover = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMover>();
            playerMover.onLocationUpdated += UpdateUI;

            talkButton.SetActive(false);

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
                DisplayMoveChoices();
                LocationHasNPC();
            }
            else
            {
                //...Coming soon....
            }

        }

        private void DisplayMoveChoices()
        {

            //Debug.Log(playerMover.GetText());
            roomNarration.text = playerMover.GetText();

            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(false);
            }

            int listCounter = 0;
            foreach (string direction in playerMover.GetMovementDirections())
            {
                //Debug.Log(listCounter);
                //Debug.Log(direction);
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

        private void LocationHasNPC()
        {
            if (playerMover.HasNPC())
            {
                talkButton.SetActive(true);
            }            
        }

        public void MoveDirection(int dir) // dir = Directions enum int value
        {
            Directions direction = (Directions) dir;    // Cast int to Directions to get the string value.
            Debug.Log("Moving : " + direction.ToString());

            int roomIndex = 0;  // This bit is similar to above in DisplayMoveChoices, it's the same as listCounter.
            foreach (string moveDirection in playerMover.GetMovementDirections())   // Go through each movement direction available.
            {
                if (moveDirection == direction.ToString())  // similar to if(direction == Directions.XX.ToString() above.
                {
                    Debug.Log("room index is: " + roomIndex);   
                    if(playerMover.GetMovementDirections().Count <= roomIndex)  // This shouldn't be needed, but I kept getting errors from GetMovementDirections.
                    {
                        Debug.LogError("Room index is missing from playerMover.GetMovementDirections()!!", playerMover);
                        return; //Just get out of here!
                    }
                    playerMover.SelectMove(playerMover.GetChoices().ElementAt(roomIndex)); // Now move the player to correct direction!
                    // .ElementAt(int index) is the same as doing .ToList()[i]
                }
                roomIndex++;
            }
            UpdateUI();
        }
    }
}
