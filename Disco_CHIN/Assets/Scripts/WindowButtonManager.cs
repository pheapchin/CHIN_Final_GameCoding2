using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowButtonManager : MonoBehaviour
{
    public void IncreaseSpeed(float _speed)
    {
        PlayerRB playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRB>();
        playerRB.AmpSpeed(_speed);
        //could change this to a function that instead uses the float speed as a multiplier to the player's moovement speed
    }
}
