using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public Image[] healthBars; //0-2 left to right
    //PlayerStats playerHealth;
    //int health;

    public Slider healthSlider;

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //lets you use one var for health
        //health = playerHealth.health;

        //switch (health)
        //{
        //    case 4:
        //        foreach(Image img in healthBars)
        //        {
        //            //when your health is 3/max, all the images will be enabled 
        //            img.gameObject.SetActive(true);
        //        }
        //        break;

        //    case 3:
        //        healthBars[0].gameObject.SetActive(true);
        //        healthBars[1].gameObject.SetActive(true);
        //        healthBars[2].gameObject.SetActive(true);
        //        healthBars[3].gameObject.SetActive(false);
        //        break;

        //    case 2:
        //        healthBars[0].gameObject.SetActive(true);
        //        healthBars[1].gameObject.SetActive(true);
        //        healthBars[2].gameObject.SetActive(false);
        //        healthBars[3].gameObject.SetActive(false);
        //        break;

        //    case 1:
        //        healthBars[0].gameObject.SetActive(true);
        //        healthBars[1].gameObject.SetActive(false);
        //        healthBars[2].gameObject.SetActive(false);
        //        healthBars[3].gameObject.SetActive(false);
        //        break;

        //    case 0:
        //        healthBars[0].gameObject.SetActive(false);
        //        healthBars[1].gameObject.SetActive(false);
        //        healthBars[2].gameObject.SetActive(false);
        //        healthBars[3].gameObject.SetActive(false);

        //        Debug.Log("Player Dead");
        //        break;
        //}
    }
}
