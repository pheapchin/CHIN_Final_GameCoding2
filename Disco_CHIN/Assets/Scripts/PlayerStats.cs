using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    //health stats:
    [Header("Player Stats: ")]
    public int health;
    public int movementSpeed;
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

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        Debug.Log("Health = " + health.ToString());
    }
}
