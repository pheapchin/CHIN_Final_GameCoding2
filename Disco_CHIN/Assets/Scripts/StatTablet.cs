using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTablet : MonoBehaviour, IInteractable
{
    //public static StatTablet Instance;

    private bool isStatBlockActive;
    public Canvas statBlock;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CanInteract()
    {
        return !isStatBlockActive;
    }

    public void Interact()
    {
        DisplayMenu();
    }

    void DisplayMenu()
    {
        isStatBlockActive = true;
        statBlock.enabled = true;
    }
}
