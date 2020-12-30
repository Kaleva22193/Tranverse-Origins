using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {

        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton = null;
        [SerializeField] Button quitButton = null;
        [SerializeField] GameObject AIResponse = null;
        [SerializeField] GameObject choicePreFab = null;
        [SerializeField] Transform choiceRoot = null;

        public GameObject GameObject { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.QuitDialogue());

            UpdateUI();
        }
        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
            {
                return;
            }
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
           
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                
                GameObject choiceInstance = Instantiate(choicePreFab, choiceRoot);
                foreach (Transform child in choiceInstance.transform)
                {
                    child.gameObject.SetActive(true);
                }
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
               
                textComp.text = choice.GetText();

                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                    UpdateUI();
                });
            }
        }
    }
}
