using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haron
{
    public enum DirectionState
    {
        right, up, left, down 
    }

    public enum HaronBehavior
    {
        Floating, Attack, Dash,
    }

    public class HaronController : MonoBehaviour
    {

        //inspector
        [SerializeField] internal float speedmove;
        [SerializeField] internal AnimationCurve acceleration;
        [SerializeField] internal AnimationCurve deaceleration;

        //debug
        [SerializeField] private DirectionState directionState;
        [SerializeField] private HaronBehavior state;
        [SerializeField] internal Vector2 direction;
        [SerializeField] internal Rigidbody2D rb;
        [SerializeField] Vector2 velosity;
        [SerializeField] internal bool isAttacking = false;
        //local
        private Dictionary<Type, IHaronBehavior> behavioraMap;
        internal IHaronBehavior behaviorCurrent;


        public DirectionState DirectionState {  get => directionState; internal set => directionState = value; }
        public HaronBehavior State { get => state; internal set => state = value; }

        private void Start()
        {
            this.InitBehaviors();
            this.SetBehaviorDefault();
            rb = GetComponent<Rigidbody2D>();
        }

        private void InitBehaviors()
        {
            this.behavioraMap = new Dictionary<Type, IHaronBehavior>();
            this.behavioraMap[typeof(HaronFloatingBehavior)] = new HaronFloatingBehavior(this);
            this.behavioraMap[typeof(HaronAttackBehavior)] = new HaronAttackBehavior(this);
            //this.behavioraMap[typeof(HookAIMBehavior)] = new HookAIMBehavior(this);
            //this.behavioraMap[typeof(HookRotationBehavior)] = new HookRotationBehavior(this);
            //this.behavioraMap[typeof(HookCatcEmptyhBehavior)] = new HookCatcEmptyhBehavior(this);
            //this.behavioraMap[typeof(HookCatchPointBehavior)] = new HookCatchPointBehavior(this);
            //this.behavioraMap[typeof(HookCathcEnemyAndProjectileBehavior)] = new HookCathcEnemyAndProjectileBehavior(this);
            //this.behavioraMap[typeof(HookRotationWithObjectBehavior)] = new HookRotationWithObjectBehavior(this);
            //this.behavioraMap[typeof(HookThrowCaptureObject)] = new HookThrowCaptureObject(this);
            //this.behavioraMap[typeof(HookStunBehavior)] = new HookStunBehavior(this);
        }
        private void SetBehaviorDefault()
        {
            SetBehaviorFloating();
        }

        public void SetBehavior(IHaronBehavior newBehavior)
        {
            if (this.behaviorCurrent != null)
                this.behaviorCurrent.Exit();

            this.behaviorCurrent = newBehavior;
            this.behaviorCurrent.Enter();
        }



        private IHaronBehavior GetBehavior<T>() where T : IHaronBehavior
        {
            var type = typeof(T);
            return this.behavioraMap[type];
        }

        private void FixedUpdate()
        {
            behaviorCurrent?.UpdateBehavior();
            velosity = rb.velocity;
           // CaiotTime();
        }

        //private void CaiotTime()
        //{
        //    if (isActiveHook)
        //    {
        //        Invoke("ResetIsActiveHook", timeCaiot);
        //    }
        //}

        public void SetBehaviorFloating()
        {
            var behavior = this.GetBehavior<HaronFloatingBehavior>();
            this.SetBehavior(behavior);
        }

        //public void SetBehaviorStan()
        //{
        //    var behavior = this.GetBehavior<HookStunBehavior>();
        //    this.SetBehavior(behavior);
        //}

        //public void SetBehaviorThrowCaptureObject()
        //{
        //    var behavior = this.GetBehavior<HookThrowCaptureObject>();
        //    this.SetBehavior(behavior);
        //}

        //public void SetBehaviorRotationWithObject()
        //{
        //    var behavior = this.GetBehavior<HookRotationWithObjectBehavior>();
        //    this.SetBehavior(behavior);
        //}

        //public void SetBehaviorCatchEnemyAndProjectile()
        //{
        //    var behavior = this.GetBehavior<HookCathcEnemyAndProjectileBehavior>();
        //    this.SetBehavior(behavior);
        //}

        //public void SetBehaviorCarchPoint()
        //{
        //    var behavior = this.GetBehavior<HookCatchPointBehavior>();
        //    this.SetBehavior(behavior);
        //}

        //public void SetBehaviorAIM()
        //{
        //    var behavior = this.GetBehavior<HookAIMBehavior>();
        //    this.SetBehavior(behavior);
        //}

        //public void SetBehaviorCatchEmpty()
        //{
        //    var behavior = this.GetBehavior<HookCatcEmptyhBehavior>();
        //    this.SetBehavior(behavior);
        //}


    }
}
