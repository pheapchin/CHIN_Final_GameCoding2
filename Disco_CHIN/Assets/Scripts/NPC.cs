using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("DialogueData")]
    public DialogueLine dialogueData;
    private DialogueController dialogueUI;
    /*public GameObject choiceButtonPrefab;
    public Transform choiceParent;*/

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private void Start()
    {
        dialogueUI = DialogueController.Instance;
    }

    public bool CanInteract()
    {
        //if dialogue box isnt active you can interact w npc
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null)
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueUI.SetNPCInfo(dialogueData.speakerName, dialogueData.npcSprite);
        dialogueUI.ShowDialogueUI(true);

        //setpause
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Locked;
        //StartCoroutine(TypeLine());
        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLinesList[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLinesList.Count)
        {
            //if another line, type next line
            //StartCoroutine(TypeLine());
            DisplayCurrentLine();
            //need to instantiate option buttons
        }
        else
        {
            EndDialogue();
        }

        //clears existing choices
        dialogueUI.ClearChoices();

        //check if there are choices
        foreach(DialogueChoice dialogueChoice in dialogueData.choices)
        {
                Debug.Log("there are choices available");
                DisplayChoices(dialogueData);
                return;
            //if(dialogueChoice.dialogueIndex == dialogueIndex)
            //{
            //}
        }
    }

    public void UpdateDialogue(DialogueLine dialogueLine)
    {
        dialogueData = dialogueLine;
        //UPDATE THISSSS
        DisplayCurrentLine();
        //StartCoroutine(TypeLine(dialogueData));
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        //clears
        dialogueUI.SetDialogueText("");

        foreach(char letter in dialogueData.dialogueLinesList[dialogueIndex])
        {
            //dialogueText.text += letter;
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.dialogueLinesList.Count > dialogueIndex)
        {
            yield return new WaitForSeconds(10f);
            NextLine();
        }
    }

    void DisplayChoices(DialogueLine line)
    {
        if (line.choices != null && line.choices.Length > 0)
        {
            foreach (DialogueChoice choice in line.choices)
            {
                //create a button
                GameObject newButtonChoice = dialogueUI.CreateChoiceButton(choice.choiceText); //Instantiate(choiceButtonPrefab, choiceContainer);
                //comeback when we have options
                TextMeshProUGUI buttonText = newButtonChoice.GetComponentInChildren<TextMeshProUGUI>();

                //grab button component of choice button
                Button buttonComp = newButtonChoice.GetComponent<Button>();
                newButtonChoice.GetComponent<OptionalChoices>().NPCSetUp(this, choice.nextLine, choice.choiceText);
            }
        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        //set pause false
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
    }
}
