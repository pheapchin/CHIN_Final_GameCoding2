using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;

    private void Start()
    {
        buttonText.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("hovering button");
        
        buttonText.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exited hover");

        buttonText.enabled = false;
    }
}
