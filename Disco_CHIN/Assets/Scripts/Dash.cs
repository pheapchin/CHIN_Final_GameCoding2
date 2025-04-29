 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LunarDash", menuName = "Dash")]
public class Dash : PlayerAbilities
{
    //stops you from dashing while exectuting
    public bool isDashed;
    //float dashSpeed = 10f;
    //float dashDuration = 1f;
    //float dashCooldown = 1f;
    public float power = 150f;

    public IEnumerator Run(Rigidbody rb)
    {
        isDashed = true;
        rb.AddForce(rb.transform.forward * power, ForceMode.Impulse);
        //rb.velocity = new Vector3(moveX, 0, moveZ) * dashSpeed;

        yield return new WaitForSeconds(.15f);

        rb.velocity = Vector3.zero;
        isDashed = false;
    }
    
}
