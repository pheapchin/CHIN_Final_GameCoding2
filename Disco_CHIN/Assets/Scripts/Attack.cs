using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Left Click")]
    //melee
    public GameObject Melee;
    public GameObject MeleeTwo;
    //public GameObject MeleeThree;
    public bool isAttacking = false;
    public bool isAttackingTwo = false;
    //public bool isAttackingThree = false;
    float atkDuration = 0.25f;
    float atkTimer = 2f;

    [Header("Right Click")]
    //ranged
    public Transform Aim;
    public Transform aimTwo;
    public Transform aimThree;
    public GameObject bullet;
    public float fireForce = 10f;
    float shootCooldown = 1.5f;
    float shootTimer = 0.5f;

    [Header("TapTimer")]
    public int tapTimes;
    public float resetTimer;
    //public bool isHoldingDown;

    GameObject player;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isShooting", false);
        animator.SetBool("isAttacking", false);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();
        shootTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            OnAttack();
            tapTimes++;
            StartCoroutine("ResetTapTimes");
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
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }

    void OnShoot()
    {
        if(shootTimer > shootCooldown)
        {
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
            intBullet.GetComponent<Rigidbody>().AddForce(Aim.forward * fireForce, ForceMode.Impulse);
            GameObject intBulletTwo = Instantiate(bullet, aimTwo.position, aimTwo.rotation);
            intBulletTwo.GetComponent<Rigidbody>().AddForce(aimTwo.forward * fireForce, ForceMode.Impulse);
            GameObject intBulletThree = Instantiate(bullet, aimThree.position, aimThree.rotation);
            intBulletThree.GetComponent<Rigidbody>().AddForce(aimThree.forward * fireForce, ForceMode.Impulse);
            Destroy(intBullet, 0.5f);
            Destroy(intBulletTwo, 0.7f);
            Destroy(intBulletThree, 0.7f);
        }
    }

    public void ExtraBullets()
    {
        aimTwo = GameObject.FindGameObjectWithTag("AimTwo").transform;
        Debug.Log("Found AimTwo");
    }
    
    public void TripleBullets()
    {
        aimThree = GameObject.FindGameObjectWithTag("AimThree").transform;
        Debug.Log("Found AimThree");
    }


    public void ActivateDOT()
    {
        //GameObject dot = GameObject.FindGameObjectWithTag("DOT");
        GameObject dot = player.transform.Find("DOT").gameObject;
        Debug.Log("found dot");
        dot.SetActive(true);
    }

    public void ActivateSlow()
    {
        GameObject slow = player.transform.Find("Slow").gameObject;
        Debug.Log("found slow");
        slow.SetActive(true);
    }

    void OnAttack()
    {
        if (!isAttacking)
        {
            MeleeTwo.SetActive(false);
            Melee.SetActive(true);
            isAttacking = true;
            //call animation for attack
            animator.SetBool("isAttacking", true);

            if (!isAttackingTwo && tapTimes >= 1)
            {
                Melee.SetActive(false);
                MeleeTwo.SetActive(true);
                isAttackingTwo = true;
                //isAttacking = false;
            }
        }
    }


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
                animator.SetBool("isAttacking", false);
            }
        }

        if (isAttackingTwo)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttackingTwo = false;
                MeleeTwo.SetActive(false);
                animator.SetBool("isAttacking", false);
            }
        }

    }

    IEnumerator ResetTapTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        tapTimes = 0;
    }
}
