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

        //setpause
        Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void CloseMenu()
    {
        isStatBlockActive = false;

        tabletUI.ShowTabletUI(false);

        //set pause false
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.None;
    }
}
