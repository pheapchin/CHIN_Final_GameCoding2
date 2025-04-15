using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LunarDash", menuName ="Abilities")]
public class Dash : PlayerAbilities
{
    //stops you from dashing while exectuting
    public bool isDashed;
    public float power = 50f;

    public IEnumerator Run(Rigidbody rb)
    {
        isDashed = true;
        rb.velocity = rb.transform.forward * power;

        yield return new WaitForSeconds(.15f);

        rb.velocity = Vector3.zero;
        isDashed = false;
    }
    
}
