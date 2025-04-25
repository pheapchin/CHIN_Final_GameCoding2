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

    [SerializeField]
    private Material material;

    // Start is called before the first frame update
    private void Awake()
    {
        material = renderers.GetComponent<Renderer>().material;
        //renderers = GetComponent<Renderer>();
        //material = GetComponent<Material>();
        //material.DisableKeyword("_Emission");
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
