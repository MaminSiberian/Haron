using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haron
{
    public enum DirectionState
    {
        right,left
    }

    public enum HaronBehavior
    {
        Floating, Attack, Dash, QTE, Death
    }

    public class HaronController : MonoBehaviour, IDamagable, IHealing
    {

        //inspector
        [SerializeField] internal int maxHP;
        [SerializeField] internal int CurrentHP;

        [SerializeField] internal Transform areaAttack;
        //[SerializeField] internal Transform pivotAttack;
        [SerializeField] internal float delayAttack;
        [Range(0f, 3f)][SerializeField] internal float distanceAttack;
        [Range(0f, 1f)][SerializeField] internal float durationAttack;
        [Range(0f, 1f)][SerializeField] internal float cooldownAttack;

        [Range(0, 50)][SerializeField] internal int damage;
        [Range(0f, 1f)][SerializeField] private float timeCaiot;
        [Space(10)]
        [Range(0f, 50f)][SerializeField] internal float speedMove;
        [Space]
        [SerializeField] internal AnimationCurve dashCurve;
        [Range(0f, 100f)][SerializeField] internal float forceDash;
        [Range(0f, 3f)][SerializeField] internal float cooldownDash;
        [Range(0f, 1f)][SerializeField] internal float durationDash;
        [Space]
        [SerializeField] internal SpriteRenderer spriteRenderer;


        //debug
        [SerializeField] private DirectionState directionState;
        [SerializeField] private HaronBehavior state;
        [SerializeField] internal Vector2 directionMove;
        [SerializeField] internal Vector2 directionAttack;
        [SerializeField] internal Rigidbody2D rb;
        [SerializeField] Vector2 velosity;
        [SerializeField] internal bool isAttacking = false;
        [SerializeField] internal bool isDash;
        [SerializeField] internal bool isReloadDash;
        [SerializeField] internal float currentTimeCooldawnDash;
        //local
        private Dictionary<Type, IHaronBehavior> behavioraMap;
        internal IHaronBehavior behaviorCurrent;
        internal GameplayUI UI;
        internal bool isF = false;

        public DirectionState DirectionState { get => directionState; internal set => directionState = value; }
        public HaronBehavior State { get => state; internal set => state = value; }

        private void Start()
        {
            UI = FindObjectOfType<GameplayUI>();
            this.InitBehaviors();
            this.SetBehaviorDefault();
            rb = GetComponent<Rigidbody2D>();
            CurrentHP = maxHP;
            UI.SetHPValue(CurrentHP);
        }

        private void InitBehaviors()
        {
            this.behavioraMap = new Dictionary<Type, IHaronBehavior>();
            this.behavioraMap[typeof(HaronFloatingBehavior)] = new HaronFloatingBehavior(this);
            this.behavioraMap[typeof(HaronAttackBehavior)] = new HaronAttackBehavior(this);
            this.behavioraMap[typeof(HaronDashBehavior)] = new HaronDashBehavior(this);
            this.behavioraMap[typeof(HaronQTEBehavior)] = new HaronQTEBehavior(this);
            this.behavioraMap[typeof(HaronDeathBehavior)] = new HaronDeathBehavior(this);          
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
            CaiotTime();
            ResetCooldownDash();
            UI.SetDashValue(currentTimeCooldawnDash, cooldownDash);
        }

        private void ResetCooldownDash()
        {
            if (currentTimeCooldawnDash >= cooldownDash)
            {
                isReloadDash = true;
               
            }
            else
            {
                currentTimeCooldawnDash += Time.fixedDeltaTime;
            }
        }

        private void CaiotTime()
        {
            if (isAttacking)
            {
                Invoke("ResetIsActiveAttack", timeCaiot);
            }

            if (isDash)
            {                
                Invoke("ResetIsActiveDash", timeCaiot);
            }

            if (isF)
            {
                Invoke("ResetIsActiveF", timeCaiot);
            }
        }
        private void ResetIsActiveAttack()
        {
            isAttacking = false;

        }
        private void ResetIsActiveDash()
        {
            isDash = false;
 
        }
        private void ResetIsActiveF()
        {
            isF = false;
        }

        public void SetBehaviorFloating()
        {
            var behavior = this.GetBehavior<HaronFloatingBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorQTE()
        {
            var behavior = this.GetBehavior<HaronQTEBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorAttack()
        {
            var behavior = this.GetBehavior<HaronAttackBehavior>();
            this.SetBehavior(behavior);
        }
        public void SetBehaviorDash()
        {
            
            var behavior = this.GetBehavior<HaronDashBehavior>();
            this.SetBehavior(behavior);

        }

        public void SetBehaviorDeath()
        {
            var behavior = this.GetBehavior<HaronDeathBehavior>();
            this.SetBehavior(behavior);
        }

        public void GetDamage(int damage)
        {
            if (CurrentHP - damage > 0)
            {
                CurrentHP -= damage;
                
            }
            else
            {
                CurrentHP = 0;
                SetBehaviorDeath();
            }
            UI.SetHPValue(CurrentHP);

        }

        public void GetHeal(int healPoint)
        {
            if (CurrentHP + healPoint < maxHP)
            {
                CurrentHP += healPoint;
            }
            else
            {
                CurrentHP = maxHP;
            }
            UI.SetHPValue(CurrentHP);
        }
    }
}
