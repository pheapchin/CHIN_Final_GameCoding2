using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public UnityEvent onInteract;

    private void Start()
    {
        Interaction();
    }

    private void Update()
    {
        
    }

    public void Interaction()
    {
        onInteract.Invoke();
    }
}
