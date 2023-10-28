using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsInWather : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int cooldownDamage;
    [SerializeField] private float forceAttraction;
    [SerializeField] private Vector3 direction;
    [Range(0f, 100f)][SerializeField] public float targetForceQTE;
    [SerializeField] public float currentForceQTE;
    [SerializeField] public float reductionForceQTE;
    [Range(0f, 50f)][SerializeField] public float forceQTE;
    [SerializeField] private float duration;
    [SerializeField] private float forceToPushObject;
    [SerializeField] private AnimationCurve forceCurve;
    [SerializeField] private Animator _anim;

    //private float startTime;
    private HaronController hc;
    private IDamagable targetDamage;
    private GameplayUI UI;
    private bool isQTE  =false;

    private void Start()
    {
        UI = FindObjectOfType<GameplayUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<HaronController>() != null)
        {
            currentForceQTE = 0;
            hc = collision.GetComponent<HaronController>();
            targetDamage = collision.GetComponent<IDamagable>();
            hc.SetBehaviorQTE();
            Rotation(hc.transform.position - transform.position);
            StartCoroutine(QTE());
            StartCoroutine(Damage());
            _anim.SetBool("IsAttack", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hc != null)
        {
            direction = (transform.position - collision.transform.position).normalized;
            hc.rb.velocity = direction * forceAttraction * Time.deltaTime;
            hc.rb.AddForce(forceAttraction * direction);
        }

    }


    private IEnumerator Damage()
    {
        while (targetDamage != null)
        {
            targetDamage.GetDamage(damage);
            yield return new WaitForSeconds(cooldownDamage);
        }
    }
    private IEnumerator QTE()
    {
        isQTE = true;
        UI.SetActiveQTE();
        StartCoroutine(BlinkF());
        //currentForceQTE = targetForceQTE / 3;
        while (currentForceQTE < targetForceQTE)
        {
            if (currentForceQTE > 0)
                currentForceQTE -= reductionForceQTE * Time.fixedDeltaTime;
            else
                currentForceQTE = 0;
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentForceQTE += forceQTE;
            }
            UI.SetQTEValue(currentForceQTE);
            yield return new WaitForSeconds(Time.fixedDeltaTime);

        }
        if (currentForceQTE > targetForceQTE)
        {
            targetDamage = null;
            StartCoroutine(PushObject());
        }
        UI.SetDeactiveQTE();
        isQTE = false;
        StopCoroutine(BlinkF());
    }


    private IEnumerator BlinkF()
    {
        while (isQTE)
        {
            UI.SetQTEpresFEnable();
            yield return new WaitForSeconds(0.3f);
            UI.SetQTEpresFDisable();
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator PushObject()
    {
        var startTime = Time.time;
        hc.SetBehaviorFloating();
        while (Time.time < startTime + duration)
        {
            var t = (Time.time - startTime) / duration;
            hc.rb.velocity = -direction * forceToPushObject * forceCurve.Evaluate(t);
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine(QTE());
        _anim.SetBool("IsAttack", false);
        StopCoroutine(PushObject());

    }


    
    private void Rotation(Vector2 direction)
    {
        float angle = AccessoryMetods.GetAngleFromVectorFloat(direction);
        if ((angle < 45) || (angle >= 315))
        {
            hc.transform.rotation = Quaternion.Euler(0, 0, 0);
            hc.DirectionState = DirectionState.right;
        }
        else if ((angle >= 45) && (angle < 135))
        {
            hc.transform.rotation = Quaternion.Euler(0, 0, 90);
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
}
