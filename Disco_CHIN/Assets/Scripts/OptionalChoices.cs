using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionalChoices : MonoBehaviour
{
    NPC npc;

    DialogueManager dialogueManager;
    DialogueLine dialogueLine;
    public TextMeshProUGUI dialogueText;

    public void SetUp(DialogueManager _dialogueManager, DialogueLine _dialogueLine, string _dialogueText)
    {
        dialogueManager = _dialogueManager;
        dialogueLine = _dialogueLine;
        dialogueText.text = _dialogueText;
    }

    public void NPCSetUp(NPC _npc, DialogueLine _dialogueLine, string _dialogueText)
    {
        npc = _npc;
        dialogueLine = _dialogueLine;
        dialogueText.text = _dialogueText;
    }

    public void SelectOption()
    {
        //dialogueManager.UpdateDialogue(dialogueLine);
        //NEED TO UPDATE THIS
        npc.UpdateDialogue(dialogueLine);
    }
}
