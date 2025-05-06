using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour, IInteractable
{
    private bool isTeleported;
    public int levelToLoad;

    public bool CanInteract()
    {
        return !isTeleported;
    }

    public void Interact()
    {
        Teleported();
    }

    public void Teleported()
    {
        SceneManager.LoadScene(levelToLoad);
        isTeleported = true;
    }

    public void StartButtons()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
