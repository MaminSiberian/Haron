using System;
using UnityEngine;

namespace Haron
{
    internal class HaronAttackBehavior : IHaronBehavior
    {
        private HaronController hc;
        private float currentTimeAttack;
        private float currentTimeCooldown;

        public HaronAttackBehavior(HaronController hc)
        {
            this.hc = hc;
        }
        public void Enter()
        {
            hc.State = HaronBehavior.Attack;
            currentTimeAttack = 0;
            hc.areaAttack.gameObject.SetActive(true);
            currentTimeCooldown = 0;
        }

        public void Exit()
        {

        }

        public void UpdateBehavior()
        {
            currentTimeAttack += Time.fixedDeltaTime;
            currentTimeCooldown += Time.fixedDeltaTime;
            if (currentTimeAttack >= hc.durationAttack)
            {
                hc.areaAttack.gameObject.SetActive(false);
            }

            if (hc.directionAttack != Vector2.zero)
            {
                RotationPivotAttack();
                DistanceAttack();
            }

            if (currentTimeCooldown >= hc.cooldownAttack)
            {
                hc.SetBehaviorFloating();
            }
        }

        private void DistanceAttack()
        {
            
            hc.areaAttack.position =(Vector2)hc.transform.position + hc.directionAttack * hc.distanceAttack;
        }

        private void RotationPivotAttack()
        {
            hc.pivotAttack.up = hc.directionAttack;
        }
    }
}