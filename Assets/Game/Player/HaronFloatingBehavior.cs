using UnityEngine;

namespace Haron
{
    internal class HaronFloatingBehavior : IHaronBehavior
    {
        private HaronController hc;
        private float timeA = 0;
        private float timeD = 0;
        // private float timeR = 0;
        private Vector2 dirDeaceleration;

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

            if (hc.direction != Vector2.zero)
            {
                Acceleration();
                //Rowing();
            }
            else
            {
                Deaceleration();
            }
        }

        //private void Rowing()
        //{

        //}

        private void Rotation()
        {
            float angle = AccessoryMetods.GetAngleFromVectorFloat(hc.direction);
            if ((angle < 45) || (angle >= 315))
            {
                hc.transform.rotation = Quaternion.Euler(0,0,0);
                hc.DirectionState = DirectionState.right;
            }
            else if ((angle >= 45) && (angle < 135))
            {
                hc.transform.rotation = Quaternion.Euler(0,0,90);
                hc.DirectionState = DirectionState.up;
            }
            else if ((angle >= 135) && (angle < 225))
            {
                hc.transform.rotation = Quaternion.Euler(0, 0, 180);
                hc.DirectionState = DirectionState.left;
            }
            else if ((angle >= 225) && (angle < 315))
            {
                hc.transform.rotation = Quaternion.Euler(0, 0, 270);
                hc.DirectionState = DirectionState.down;
            }
        }

        private void Acceleration()
        {
            var currentSpeed = hc.acceleration.Evaluate(timeA) * hc.speedmove;
            Debug.Log(currentSpeed);
            timeA += Time.fixedDeltaTime;
            hc.rb.velocity = hc.direction * currentSpeed;
            timeD = 0;
            dirDeaceleration = hc.direction;
        }

        private void Deaceleration()
        {
            var currentSpeed = hc.deaceleration.Evaluate(timeD) * hc.speedmove;
            Debug.Log(currentSpeed);
            timeD += Time.fixedDeltaTime;
            hc.rb.velocity = dirDeaceleration * currentSpeed;
            timeA = 0;
        }
    }
}