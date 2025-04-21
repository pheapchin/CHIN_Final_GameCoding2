using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTablet : MonoBehaviour, IInteractable
{
    //public static StatTablet Instance;

    private bool isStatBlockActive;
    //public Canvas statBlock;
    private StatTabletController tabletUI;

    // Start is called before the first frame update
    void Start()
    {
        tabletUI = StatTabletController.Instance;
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
        if (isStatBlockActive)
        {
            CloseMenu();
        }
        else
        {
        DisplayMenu();
        }
    }

    void DisplayMenu()
    {
        isStatBlockActive = true;

        tabletUI.ShowTabletUI(true);
    }

    void CloseMenu()
    {
        isStatBlockActive = false;

        tabletUI.ShowTabletUI(false);
    }
}
