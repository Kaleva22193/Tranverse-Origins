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
                Image buttonImage = moveButtons[i].GetComponent<Image>();
                buttonImage.enabled = false;
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

            for (int i = 0; i < moveButtons.Length; i++)
            {
                moveButtons[i].SetActive(true);
                
                if (i == number)
                {
                    Image buttonImage = moveButtons[i].GetComponent<Image>();
                    buttonImage.enabled = true;
                    Button button = moveButtons[i].GetComponentInChildren<Button>();
                    Debug.Log("Counter is set at: " + counter);
                    //button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => LinkDirectiontoLocation(counter));
                }                           

            }
            
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
