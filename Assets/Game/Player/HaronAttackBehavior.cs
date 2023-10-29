using System;
using UnityEngine;

namespace Haron
{
    internal class HaronAttackBehavior : IHaronBehavior
    {
        private HaronController hc;
        private float cuurentDelayAttack;
        private float currentTimeAttack;
        private float currentTimeCooldown;

        public HaronAttackBehavior(HaronController hc)
        {
            this.hc = hc;
        }
        public void Enter()
        {
            hc.isAttacEND = false;
            cuurentDelayAttack = 0;
            hc.State = HaronBehavior.Attack;
            currentTimeAttack = 0;            
            currentTimeCooldown = 0;
        }

        public void Exit()
        {
            hc.isAttacEND = true;
        }

        public void UpdateBehavior()
        {
            cuurentDelayAttack += Time.fixedDeltaTime;

            if (cuurentDelayAttack >= hc.delayAttack)
            {
                hc.areaAttack.gameObject.SetActive(true);
                hc.areaAttack.localScale = Vector3.one;
                currentTimeAttack += Time.fixedDeltaTime;
                currentTimeCooldown += Time.fixedDeltaTime;
                if (currentTimeAttack >= hc.durationAttack)
                {
                    hc.areaAttack.gameObject.SetActive(false);
                    hc.areaAttack.localScale = Vector3.zero;
                }

                //if (hc.directionAttack != Vector2.zero)
                //{
                //    RotationPivotAttack();
                //    DistanceAttack();
                //}

                if (currentTimeCooldown >= hc.cooldownAttack)
                {
                    hc.SetBehaviorFloating();
                }            
            }
        }

        //private void DistanceAttack()
        //{

        //    hc.areaAttack.position = (Vector2)hc.transform.position + hc.directionAttack * hc.distanceAttack;
        //}

        //private void RotationPivotAttack()
        //{
        //    hc.pivotAttack.up = hc.directionAttack;
        //}
    }
}