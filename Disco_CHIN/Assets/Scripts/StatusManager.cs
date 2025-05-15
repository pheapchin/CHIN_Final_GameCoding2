using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private EnemyAI healthScript;
    //GameObject enemy;

    public List<int> burnTickTimers = new List<int>();
    public List<int> slowTickTimers = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        healthScript = GetComponent<EnemyAI>();
    }

    public void ApplyDOT(int ticks)
    {
        if(burnTickTimers.Count <= 0)
        {
            burnTickTimers.Add(ticks);
            StartCoroutine(Burn());
            Debug.Log("Started Burn");
        }
        else
        {
            burnTickTimers.Add(ticks);
        }
    }

    public void ApplySlow(int ticks)
    {
        if(slowTickTimers.Count <= 0)
        {
            slowTickTimers.Add(ticks);
            StartCoroutine(Slow());
            Debug.Log("Started Slow");
        }
        else
        {
            slowTickTimers.Add(ticks);
        }
    }

    IEnumerator Burn()
    {
        //only runs when there is something in the list
        while(burnTickTimers.Count > 0)
        {
            for(int i = 0; i < burnTickTimers.Count; i++)
            {
                burnTickTimers[i]--;
            }
            //5 dmg per ticks
            healthScript.health -= 2;
            //removes anything that is 0: removes i if i = 0
            burnTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }

    IEnumerator Slow()
    {
        while(slowTickTimers.Count > 0)
        {
            for(int i = 0; i < slowTickTimers.Count; i++)
            {
                slowTickTimers[i]--;
            }
            //not actually slowing them but it is changing the numbers, fix this
            healthScript.agent.speed -= 3;
            slowTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(.5f);
        }
    }
}
