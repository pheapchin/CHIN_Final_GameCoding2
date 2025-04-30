using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowButtonManager : MonoBehaviour
{
    public int buttonClicks = 0;

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
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerStats.AddHealth(health);
    }
}
