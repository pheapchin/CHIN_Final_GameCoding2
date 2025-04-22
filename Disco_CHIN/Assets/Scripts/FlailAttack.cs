using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlailAttack", menuName = "Attack")]
public class FlailAttack : PlayerAbilities
{
    //logic, if bool is active and the button is clicked again then move onto nexxt attack item in the strings

    //public Animator animator;
    //public float attackRange = 0.5f;
    public GameObject attackContainer;

    public bool attackedOnce;
    public bool attackedTwice;
    public bool attackedThird;


    // Start is called before the first frame update
    void Start()
    {
        attackContainer = GameObject.FindWithTag("AtkContainer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Attack()
    {
        //play animation attack

        attackedOnce = true;

        

        yield return new WaitForSeconds(.15f);
    }
}
