using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaronDeathBehavior : IHaronBehavior

{
    private HaronController hc;

    public HaronDeathBehavior(HaronController hc)
    {
        this.hc = hc;
    }
    public void Enter()
    {
        hc.State = HaronBehavior.Death;
        Debug.Log("Behavior DEATH");
    }

    public void Exit()
    {
       
    }

    public void UpdateBehavior()
    {
       
    }
}
