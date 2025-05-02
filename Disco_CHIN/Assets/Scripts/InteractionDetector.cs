using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; //closest interactable

    public bool interacting = false;

    //public Canvas scrollingBox;
    //assign the renderers here

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (interacting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //checks if there is an interactable in range and if there is then calls the interact function
                interactableInRange?.Interact();
                //scrollingBox.GetComponent<Canvas>().enabled = true;
                Debug.Log("Interacted");
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interacting = true;
            interactableInRange = interactable;
            Debug.Log("interactable");

            //setting emission
            //Material.SetColor()
            other.GetComponent<Highlight>().ToggleHighlight();
            Debug.Log("toggled");
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            //fix this
            interactable = null;
            Debug.Log("non interactable");
            other.GetComponent<Highlight>().DisableHighlight();
            interacting = false;
        }
    }
}
