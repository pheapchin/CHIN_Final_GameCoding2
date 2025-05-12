using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTabletController : MonoBehaviour
{
    public static StatTabletController Instance;

    [Header("Tablet Manager")]
    //public DialogueLine dialogueData;
    public GameObject tabletPanel;

    [Header("Reward Manager")]
    public GameObject rewardsPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowTabletUI(bool show)
    {
        tabletPanel.SetActive(show);
    }

    public void ShowRewardUI(bool show)
    {
        rewardsPanel.SetActive(show);
    }
}
