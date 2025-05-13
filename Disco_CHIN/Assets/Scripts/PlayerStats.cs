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

    [Header("Healing Radius")]
    public List<int> healTickTimers = new List<int>();

    //public int movementSpeed;
    //public int atkSpeed;
    Animator animator;

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

        animator = GetComponent<Animator>();
        animator.SetBool("isHit", false);
    }


    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Health = " + currentHealth.ToString());
        animator.SetBool("isHit", true);
        StartCoroutine(HitWait());
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

    public void ApplyHealing(int ticks)
    {
        if (healTickTimers.Count <= 0)
        {
            healTickTimers.Add(ticks);
            StartCoroutine(Heal());
            Debug.Log("Started Heals");
        }
        else
        {
            healTickTimers.Add(ticks);
        }
    }

    IEnumerator Heal()
    {
        //only runs when there is something in the list
        while (healTickTimers.Count > 0)
        {
            for (int i = 0; i < healTickTimers.Count; i++)
            {
                healTickTimers[i]--;
            }
            //5 dmg per ticks
            currentHealth += 5;
            healthBar.SetHealth(currentHealth);
            //removes anything that is 0: removes i if i = 0
            healTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator HitWait()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("isHit", false);
    }
}
