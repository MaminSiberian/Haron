using System;
using UnityEngine;

namespace Haron
{
    internal class HaronFloatingBehavior : IHaronBehavior
    {
        private HaronController hc;
        private float timeA = 0;
        private float timeD = 0;
        private SpriteRenderer spriteRenderer;
        // private float timeR = 0;
        private Vector2 dirDeaceleration;
        


        public HaronFloatingBehavior(HaronController hc)
        {
            this.hc = hc;
            spriteRenderer = hc.spriteRenderer;
        }

        public void Enter()
        {
            //Debug.Log("Enter Rotation state");
            hc.rb.velocity = Vector2.zero;
            // hc.currentMaxDistanceHook = hc.maxDistanseHook;
        }

        public void Exit()
        {
            //Debug.Log("Exit Rotation state");
        }

        public void UpdateBehavior()
        {
            if (hc.directionMove != Vector2.zero)
            {
                Rotation();

            }

            if (hc.directionMove != Vector2.zero)
            {
                MoveDirection();
                //Rowing();
            }
            //else
            //{
            //    Deaceleration();
            //}

            if ((hc.isAttacking))
            {
                hc.isAttacking = false;
                hc.SetBehaviorAttack();
            }

            if (hc.isDash && hc.isReloadDash)
            {
                hc.isDash = false;
                hc.SetBehaviorDash();
            }
        }

        private void MoveDirection()
        {
            hc.rb.AddForce(hc.directionMove * hc.speedMove);//, ForceMode2D.Impulse);
           // hc.rb.velocity = hc.directionMove * hc.speedMove;
        }

        private void Rotation()
        {
            float angle = AccessoryMetods.GetAngleFromVectorFloat(hc.directionMove);
            if ((angle < 90) || (angle >= 270))
            {
                hc.transform.rotation = Quaternion.Euler(0,0,0);
                
                hc.DirectionState = DirectionState.right;
            }
            //else if ((angle >= 45) && (angle < 135))
            //{
            //    hc.transform.rotation = Quaternion.Euler(0,0,90);
            //    hc.DirectionState = DirectionState.up;
            //}
            else if ((angle >= 90) && (angle < 270))
            {
                hc.transform.rotation = Quaternion.Euler(0, 180, 0);
                
                hc.DirectionState = DirectionState.left;
            }
            //else if ((angle >= 225) && (angle < 315))
            //{
            //    hc.transform.rotation = Quaternion.Euler(0, 0, 270);
            //    hc.DirectionState = DirectionState.down;
            //}
        }



        //private void Acceleration()
        //{
        //    var currentSpeed = hc.acceleration.Evaluate(timeA) * hc.speedmove;
        //    timeA += Time.fixedDeltaTime;
        //    hc.rb.velocity = hc.directionMove * currentSpeed;
        //    timeD = 0;
        //    dirDeaceleration = hc.directionMove;
        //}

        //private void Deaceleration()
        //{
        //    var currentSpeed = hc.deaceleration.Evaluate(timeD) * hc.speedmove;
        //    timeD += Time.fixedDeltaTime;
        //    hc.rb.velocity = dirDeaceleration * currentSpeed;
        //    timeA = 0;
        //}
    }
}