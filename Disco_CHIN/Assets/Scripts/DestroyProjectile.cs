using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(projectile, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(projectile, .5f);
    }

}
