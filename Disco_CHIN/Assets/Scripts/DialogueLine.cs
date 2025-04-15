using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DialogueLine/Object")]
//scriptable object is a storage container, allows you to store a large amount of shared data--independent from script
public class DialogueLine : ScriptableObject
{
    public string speakerName;
    public Image npcSprite;
    public float typingSpeed;


    [TextArea] public List<string> dialogueLinesList = new List<string>();
    //list of text to show 
    //[TextArea] public string textLine;
    //next line if there are no choices
    public DialogueLine nextLine;
    //choices if there are any
    public DialogueChoice[] choices;
}

[System.Serializable]
public class DialogueChoice
{
    //choices if there are any
    //public DialogueChoice[] choices;
    public string choiceText; //what the choice says
    public DialogueLine nextLine; //what happens when u pick it

    public int dialogueIndex; //connected to list

    public string requiredStat;
    public int requiredValue;

    public string rewardStat;
    public int rewardAmt;
}
