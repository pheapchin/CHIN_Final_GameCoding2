using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Scrolling Text Box")]
    //reference to scriptable obj
    public DialogueLine currentLine;
    //container for dialogue lines
    public Transform dialogueParent;
    public GameObject dialoguePrefab;
    //prefab for button choices
    public GameObject choiceButtonPrefab;
    //container for button choices
    public Transform choiceParent;
    public Button continueButton;
    public Canvas scrollingBox;

    //text boxes
    /*[Header("Character Text Boxes")]
    public Transform charDialogueParent;
    public GameObject charDialoguePrefab;
    public GameObject charChoiceButtonPrefab;
    public Transform charChoiceParent;
    public Button charContinueButton;
    public Image charSprite;*/


    // Start is called before the first frame update
    void Start()
    {
        //turns off canvas at start
        scrollingBox.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartingDialogue()
    {
        UpdateDialogue(currentLine);
    }

    public void UpdateDialogue(DialogueLine dialogueLine)
    {
        currentLine = dialogueLine;
        StartCoroutine(DisplayDialogue(currentLine));

        //when dialogue begins appear but not necessary
        continueButton.enabled = true;
    }

    /*public void ShowDialogue(DialogueLine _dialogueLine)
    {
        //if we have a next line
        if(_dialogueLine.nextLine != null)
        {
            //then turn on button
            continueButton.gameObject.SetActive(true);
        }
    }*/

    IEnumerator DisplayDialogue(DialogueLine line)
    {
        //if(dialogueParent == null) return;//if no dialogue, exit function

        foreach(string _dialogueLine in currentLine.dialogueLinesList)
        {
            //make a new copy of the button
            GameObject textBubble = Instantiate(dialoguePrefab, dialogueParent);
            //grab getComponentInChildren if u want a text bubble holder and then grab the childed text
            TextMeshProUGUI grabText = textBubble.GetComponent<TextMeshProUGUI>();
            //set the text to whatever string we are currently looping
            grabText.text = _dialogueLine;

            if (!string.IsNullOrEmpty(line.speakerName))
            {
                grabText.text = $"<b>{line.speakerName}:</b>{_dialogueLine}";
            }

            yield return new WaitForSeconds(1f);
        }

        //moves continue button to last
        continueButton.transform.SetAsLastSibling();

        //clear old choice buttons so they dont stack
        foreach(Transform child in choiceParent) Destroy(child.gameObject);
        //hide continue button by default
        continueButton.gameObject.SetActive(false);
        //button choices appear after the latest chat line
        choiceParent.transform.SetAsLastSibling();

        if (line.choices != null && line.choices.Length > 0)
        {
            foreach (DialogueChoice choice in line.choices)
            {
                //create a button
                GameObject newButtonChoice = Instantiate(choiceButtonPrefab, choiceParent);
                //comeback when we have options
                TextMeshProUGUI buttonText = newButtonChoice.GetComponentInChildren<TextMeshProUGUI>();

                bool meetsRequirement = true;

                //if req stat field has something, not empty 
                if (!string.IsNullOrEmpty(choice.requiredStat))
                {
                    //checks player stats and retuns current value stored in variable
                    //int playerStat = GetPlayerStatValue(choice.requiredStat);
                    //checks if it is greater than or equal to required value, if it is set it to true

                    int playerStat = PlayerStats.Instance.GetStat(choice.requiredStat);
                    meetsRequirement = playerStat >= choice.requiredValue;
                }

                //update button text
                buttonText.text = choice.choiceText;
                if (!meetsRequirement)
                {
                    //buttonText.text += "<color=red>" + choice.requiredStat + ": " + choice.requiredValue + "</color>";
                    buttonText.text += $"<color=red>({choice.requiredStat} : {choice.requiredValue})</color>";
                }

                //grab button component of choice button
                Button buttonComp = newButtonChoice.GetComponent<Button>();
                buttonComp.onClick.AddListener(() =>
                {
                    if (!string.IsNullOrEmpty(choice.rewardStat))
                    {
                        PlayerStats.Instance.IncreaseStat(choice.rewardStat, choice.rewardAmt);
                    }
                });

                buttonComp.interactable = meetsRequirement;

                if (meetsRequirement)
                {
                    newButtonChoice.GetComponent<OptionalChoices>().SetUp(this, choice.nextLine, choice.choiceText);
                }
            }
        }
        else if(line.nextLine != null)
        {
            //if there are no choices but there is a line, show continue button
            //clear everything out that was set to happen
            //bc we're using the same button for different lines we dont want them to stack over each other
            continueButton.onClick.RemoveAllListeners();
            //when button is clciked run this code
            continueButton.onClick.AddListener(() =>
            {
                //continue to next line
                UpdateDialogue(line.nextLine);
                continueButton.gameObject.SetActive(false);
            });
        }
    }

    

    /*int GetPlayerStatValue(string StatName)
    {
        switch (StatName)
        {
            case "charisma": return PlayerStats.Instance.charisma;
            case "logic": return PlayerStats.Instance.logic;
            default: return 0;
        }
    }*/
}
