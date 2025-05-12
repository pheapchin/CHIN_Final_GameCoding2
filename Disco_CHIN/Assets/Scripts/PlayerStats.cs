using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    //health stats:
    [Header("Player Stats: ")]
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    //public int movementSpeed;
    public int atkSpeed;

    public Dictionary<string, int> stats = new Dictionary<string, int>();

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //base stats
        stats["charisma"] = 1;
        stats["logic"] = 1;
    }

    public int GetStat(string statName)
    {
        if (stats.ContainsKey(statName))
        {
            return stats[statName];
        }

        return 0;
    }

    public void IncreaseStat(string _statName, int amount)
    {
        //if the stat key does noy exist then create it and set it to 0
        if(!stats.ContainsKey(_statName))
        {
            stats[_statName] = 0;
        }

        stats[_statName] += amount;

        Debug.Log($"Increased {_statName} by {amount}. New Value {stats[_statName]}");
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Health = " + currentHealth.ToString());
    }

    public void AddHealth(int _health)
    {
        maxHealth += _health;
        //currentHealth = maxHealth;
        //increases max value on the health bar by 1 (corresponds to the number of the button increase
        healthBar.IncreaseMaxValue(1);
        //sets the bar to update the current health to the max value
        healthBar.SetHealth(currentHealth);
        Debug.Log("Health = " + currentHealth.ToString());
    }

    public void HealHealth(int _health)
    {
        currentHealth += _health;
        //currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Health = " + currentHealth.ToString());
    }
}
