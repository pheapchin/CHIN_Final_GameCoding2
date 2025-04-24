using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    //assign the renderers here
    [SerializeField]
    private Renderer renderers;

    [SerializeField]
    private Color color = Color.white;

    private Material material;

    // Start is called before the first frame update
    private void Awake()
    {
       
    }

    public void ToggleHighlight()
    {
        //enable EMISSION
        material.EnableKeyword("_EMISSION");
        //set color
        material.SetColor("_EmsissionColor", color);
    }

    public void DisableHighlight()
    {
       material.DisableKeyword("_EMISSION");
    }
}
