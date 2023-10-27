using UnityEngine;

namespace Haron
{
    internal class HaronAttackBehavior : IHaronBehavior
    {
        private HaronController hc;

        public HaronAttackBehavior(HaronController hc)
        {
            this.hc = hc;
        }
        public void Enter()
        {
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBehavior()
        {
            throw new System.NotImplementedException();
        }
    }
}