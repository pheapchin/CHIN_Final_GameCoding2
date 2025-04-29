using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Left Click")]
    //melee
    public GameObject Melee;
    public GameObject MeleeTwo;
    public GameObject MeleeThree;
    public bool isAttacking = false;
    public bool isAttackingTwo = false;
    public bool isAttackingThree = false;
    float atkDuration = 0.25f;
    float atkTimer = 0.1f;

    [Header("Right Click")]
    //ranged
    public Transform Aim;
    public GameObject bullet;
    public float fireForce = 10f;
    float shootCooldown = 1.5f;
    float shootTimer = 0.5f;

    [Header("TapTimer")]
    public int tapTimes;
    public float resetTimer;
    //public bool isHoldingDown;

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();
        shootTimer += Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            //StartCoroutine("ResetTapTimes");
            OnAttack();
            tapTimes++;
        }
        /*if(tapTimes <= 2)
        {
            OnAttackTwo();
            tapTimes++;
        }
        if(tapTimes >= 3)
        {
            OnAttackThree();
            tapTimes = 0;
        }*/

        if(Input.GetMouseButton(1))
        {
            OnShoot();
        }
    }

    void OnShoot()
    {
        if(shootTimer > shootCooldown)
        {
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
            intBullet.GetComponent<Rigidbody>().AddForce(Aim.forward * fireForce, ForceMode.Impulse);
            Destroy(intBullet, 0.5f);
        }
    }

    void OnAttack()
    {
        if (!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
            //call animation for attack
        }
    }

    /*void OnAttackTwo()
    {
        if (!isAttackingTwo)
        {
            MeleeTwo.SetActive(true);
            isAttackingTwo = true;
            //isAttacking = false;
        }
    }

    void OnAttackThree()
    {
        if (!isAttackingThree)
        {
            MeleeThree.SetActive(true);
            isAttackingThree = true;
            //isAttackingTwo = false;
        }
    }*/

    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
            }
        }

        /*if (isAttackingTwo)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttackingTwo = false;
                MeleeTwo.SetActive(false);
            }
        }

        if (isAttackingThree)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttackingThree = false;
                MeleeThree.SetActive(false);
            }
        }*/
    }

    /*IEnumerator ResetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0;
    }*/
}
