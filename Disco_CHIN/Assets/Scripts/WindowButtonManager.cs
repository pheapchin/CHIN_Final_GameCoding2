using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowButtonManager : MonoBehaviour
{
    public int buttonClicks = 0;
    private GameObject rewardInteract;

    private void Update()
    {
        Reward reward = GameObject.FindGameObjectWithTag("RewardInteract").GetComponent<Reward>();
    }

    public void IncreaseSpeed(float _speed)
    {
        Debug.Log("button pressed");
        buttonClicks++;
        PlayerRB playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRB>();
        playerRB.AmpSpeed(_speed);
        //could change this to a function that instead uses the float speed as a multiplier to the player's moovement speed
        if(buttonClicks == 4)
        {
            GameObject.Find("AMP").GetComponent<Button>().enabled = false;
            GameObject.Find("AMP").GetComponent<Image>().color = Color.gray;
        }
    }

    public void IncreaseProjectiles()
    {
        buttonClicks++;
        if (buttonClicks == 1)
        {
            Debug.Log("button pressed");
            Attack attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
            attack.ExtraBullets();
        }
        if (buttonClicks == 2)
        {
            Attack attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
            attack.TripleBullets();
            GameObject.Find("Xtra Bullets").GetComponent<Button>().enabled = false;
            GameObject.Find("Xtra Bullets").GetComponent <Image>().color = Color.gray;
        }
    }

    public void MaxHealthIncrease(int health)
    {
        buttonClicks++;
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerStats.AddHealth(health);
        if(buttonClicks == 20)
        {
            GameObject.Find("ADDHealth").GetComponent<Button>().enabled = false;
            GameObject.Find("ADDHealth").GetComponent<Image>().color = Color.gray;
        }
    }

    public void HealHealth(int health)
    {
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerStats.HealHealth(health);

        Reward reward = GameObject.FindGameObjectWithTag("RewardInteract").GetComponent<Reward>();
        reward.CloseMenu();
    }

    public void DOTRadius()
    {
        //GameObject dot = GameObject.FindGameObjectWithTag("DOT");
        //dot.SetActive(true);
        Attack attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        attack.ActivateDOT();

        Reward reward = GameObject.FindGameObjectWithTag("RewardInteract").GetComponent<Reward>();
        reward.CloseMenu();
    }

    public void SlowRadius()
    {
        Attack attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        attack.ActivateSlow();

        Reward reward = GameObject.FindGameObjectWithTag("RewardInteract").GetComponent<Reward>();
        reward.CloseMenu();
    }

    public void HealingRadius()
    {
        Light healLight = GameObject.FindGameObjectWithTag("Heal").GetComponent<Light>();
        SphereCollider heal = GameObject.FindGameObjectWithTag("Heal").GetComponent<SphereCollider>();
        Debug.Log("found heal");
        healLight.enabled = true;
        heal.enabled = true;

        Reward reward = GameObject.FindGameObjectWithTag("RewardInteract").GetComponent<Reward>();
        reward.CloseMenu();
    }
}
