using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    public AbilityState state = AbilityState.ready;

    //allows for polymorphism and changes under a specific "unit", so if you had multiple enemies with similar functions
    //but wanted them to do different things on death you could use a virtual void
    public virtual void Activate(GameObject parent)
    {

    }
}
