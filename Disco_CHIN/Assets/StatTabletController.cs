using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTabletController : MonoBehaviour
{
    public static StatTabletController Instance;

    [Header("UI Manager")]
    //public DialogueLine dialogueData;
    public GameObject tabletPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowTabletUI(bool show)
    {
        tabletPanel.SetActive(show);
    }
}
