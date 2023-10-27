using UnityEngine;

namespace Haron
{
    internal class HaronFloatingBehavior : IHaronBehavior
    {
        private HaronController hc;
        private float timeMove = 0;
        private float currentRotateAngle;
        public HaronFloatingBehavior(HaronController hc)
        {
            this.hc = hc;
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
            if (hc.direction != Vector2.zero)
            {
                Rotation();
                
            }

            if (hc.isMoving )
            {
                Move();                
            }
            else
            {
                timeMove = 0;
            }
        }

        private void Rotation()
        {   
            float angle = AccessoryMetods.GetAngleFromVectorFloat(hc.direction) + 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            hc.transform.rotation = Quaternion.Slerp(hc.transform.rotation, rotation, hc.speedRotation * Time.fixedDeltaTime); ;
            currentRotateAngle = hc.transform.localEulerAngles.z - 90;
            Debug.Log(currentRotateAngle);
        }

        private void Move()
        {
            var currentSpeed = hc.acceleration.Evaluate(timeMove) * hc.speedmove;
            timeMove += Time.fixedDeltaTime;
            hc.rb.velocity = hc.direction * currentSpeed * Time.fixedDeltaTime;
        }
    }
}