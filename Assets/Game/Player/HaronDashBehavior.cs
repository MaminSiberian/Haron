using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaronDashBehavior : IHaronBehavior
{
    private HaronController hc;
    private float startDash;
    private float curenTimeCooldawn;
   // private bool isEndDash;
    private Vector2 DirectionDash;

    public HaronDashBehavior(HaronController hc)
    {
        this.hc = hc;
    }
    public void Enter()
    {
        //hc.UI.OnDashUsed();
        hc.State = HaronBehavior.Dash;
        DirectionDash = hc.directionMove;
        startDash = Time.time;
        hc.isReloadDash = false;
        //isEndDash = false;
    }

    public void Exit()
    {
        hc.currentTimeCooldawnDash = 0;
        //hc.UI.OnDashReady();
    }

    public void UpdateBehavior()
    {
        Dash();
    }

    private void Dash()
    {
        if (Time.time < startDash + hc.durationDash)
        {
            var t = (Time.time - startDash) / hc.durationDash;
            hc.rb.velocity = hc.directionMove * hc.forceDash * hc.dashCurve.Evaluate(t);
            //startDash += Time.fixedDeltaTime;
        }
        else
        {
            hc.SetBehaviorFloating();
           // isEndDash = true;
        }
    }
}
