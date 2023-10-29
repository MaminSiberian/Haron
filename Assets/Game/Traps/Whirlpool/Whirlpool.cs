using Haron;
using System;
using System.Collections;
using UnityEngine;
//using UnityEngine.Windows;

public class Whirlpool : MonoBehaviour
{
    [SerializeField] private float forceAttraction;
    [SerializeField] private Vector3 direction;
    [Range(0f, 100f)][SerializeField] public float targetForceQTE;
    [SerializeField] public float currentForceQTE;
    [SerializeField] public float reductionForceQTE;
    [Range(0f, 50f)][SerializeField] public float forceQTE;
    [SerializeField] private float duration;
    [SerializeField] private float forceToPushObject;
    [SerializeField] private AnimationCurve forceCurve;

    private bool isQTE = false;
    private HaronController hc;
    private GameplayUI UI;
    private float distanceFirst;
    private float distanceCurrent;

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
            distanceFirst = Vector2.Distance(transform.position, hc.transform.position);
            hc.SetBehaviorQTE();
            Rotation(hc.transform.position - transform.position);
            StartCoroutine(QTE());
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
    private IEnumerator QTE()
    {
        isQTE = true;
        UI.SetActiveQTE();
        StartCoroutine(BlinkF());
        while (currentForceQTE < targetForceQTE)
        {
            if (currentForceQTE > 0)
                currentForceQTE -= reductionForceQTE * Time.fixedDeltaTime;
            else
                currentForceQTE = 0;
            if (hc.isF)
            {
                hc.isF = false;
                currentForceQTE += forceQTE;
            }
            UI.SetQTEValue(currentForceQTE);
            yield return new WaitForSeconds(Time.fixedDeltaTime);

        }

        if (currentForceQTE > targetForceQTE)
        {
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
        distanceCurrent = Vector2.Distance(transform.position, hc.transform.position);
        float onePercentDist = (distanceFirst * 0.01f);
        float percentDist = (100 - (distanceCurrent / onePercentDist)) * 0.01f;
        Debug.Log(distanceFirst + " " + distanceCurrent + " " + percentDist);
        while (Time.time < startTime + duration)
        {
            var t = (Time.time - startTime) / duration;
            hc.rb.velocity = -direction * forceToPushObject * percentDist * forceCurve.Evaluate(t);
            yield return new WaitForFixedUpdate();
        }
        hc.SetBehaviorFloating();
        StopCoroutine(QTE());
        
        StopCoroutine(PushObject());
    }

    private void Rotation(Vector2 direction)
    {
        float angle = AccessoryMetods.GetAngleFromVectorFloat(direction);
        if ((angle < 90) || (angle >= 270))
        {
            hc.transform.localScale = Vector2.one;
            //hc.transform.rotation = Quaternion.Euler(0, 0, 0);
            hc.DirectionState = DirectionState.right;
        }
        else if ((angle >= 90) && (angle < 270))
        {
            hc.transform.localScale = new Vector3(-1, 1, 1);
            //hc.transform.rotation = Quaternion.Euler(0, 0, 180);
            hc.DirectionState = DirectionState.left;
        }
    }
}
