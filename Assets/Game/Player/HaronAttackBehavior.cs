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
                Debug.Log(1);
                hc.areaAttack.gameObject.SetActive(true);
                hc.areaAttack.localScale = Vector3.one;
                currentTimeAttack += Time.fixedDeltaTime;
                currentTimeCooldown += Time.fixedDeltaTime;
                if (currentTimeAttack >= hc.durationAttack)
                {
                    Debug.Log(2);
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
                    Debug.Log(3);
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