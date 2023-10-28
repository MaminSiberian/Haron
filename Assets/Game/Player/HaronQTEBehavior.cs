using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaronQTEBehavior : IHaronBehavior
{
    
    private HaronController hc;

    public HaronQTEBehavior(HaronController hc)
    {
        this.hc = hc;
    }

    public void Enter()
    {
        hc.State = HaronBehavior.QTE;
        
    }

    public void Exit()
    {

    }

    public void UpdateBehavior()
    {
       
    }

   
}
