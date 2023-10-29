using Haron;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaronDeathBehavior : IHaronBehavior

{
    private HaronController hc;
    public static event Action OnPlayerDeath;

    public HaronDeathBehavior(HaronController hc)
    {
        this.hc = hc;
    }
    public void Enter()
    {
        hc.GetDamage(1000);
        hc.State = HaronBehavior.Death;
        OnPlayerDeath?.Invoke();        
        Debug.Log("Behavior DEATH");
    }

    public void Exit()
    {
       
    }

    public void UpdateBehavior()
    {
       
    }
}
