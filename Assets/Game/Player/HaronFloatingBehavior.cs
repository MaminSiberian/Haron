using System;
using UnityEngine;

namespace Haron
{
    internal class HaronFloatingBehavior : IHaronBehavior
    {
        private HaronController hc;
        private SpriteRenderer spriteRenderer;       


        public HaronFloatingBehavior(HaronController hc)
        {
            this.hc = hc;
            spriteRenderer = hc.spriteRenderer;
        }

        public void Enter()
        {
            //Debug.Log("Enter Rotation state");
            hc.rb.velocity = Vector2.zero;
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
            }

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
            hc.rb.AddForce(hc.directionMove * hc.speedMove);
        }

        private void Rotation()
        {
            float angle = AccessoryMetods.GetAngleFromVectorFloat(hc.directionMove);
            if ((angle < 90) || (angle >= 270))
            {
                hc.transform.localScale = Vector2.one;                
                hc.DirectionState = DirectionState.right;
            }
            else if ((angle >= 90) && (angle < 270))
            {
                hc.transform.localScale = new Vector3(-1,1,1);
                hc.DirectionState = DirectionState.left;
            }
        }
   
    }
}