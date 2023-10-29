using Haron;
using System.Collections;
using System.Collections.Generic;
using UI;
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
    [SerializeField] private Animator[] _anim;
    [SerializeField] private AudioClip _attack;
    


    //private float startTime;
    private HaronController hc;
    private IDamagable targetDamage;
    private GameplayUI UI;
    private bool isQTE = false;
    private float distanceFirst;
    private float distanceCurrent;
    private AudioSource _source;
    private float startVolume;

    private void Start()
    {
        UI = FindObjectOfType<GameplayUI>();
        _source = GetComponent<AudioSource>();
        startVolume = _source.volume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<HaronController>() != null)
        {
            currentForceQTE = 0;
            hc = collision.GetComponent<HaronController>();
            targetDamage = collision.GetComponent<IDamagable>();
            distanceFirst = Vector2.Distance(transform.position, hc.transform.position);
            Rotation(hc.transform.position - transform.position);
            hc.SetBehaviorQTE();
            StartCoroutine(QTE());
            StartCoroutine(Damage());

            StartCoroutine(Anim());
            _source.PlayOneShot(_attack);
            if (Random.Range(0, 2) == 1)
            {
                UIDirector.SendMessage(Messages.handsAttacking, 3f);
            }
        }
    }

    private IEnumerator FadeAudio()
    {
        while(true)
        {
            _source.volume -= 0.1f;  
            if(_source.volume <= 0)
            {
                break;
                
            }
            yield return new WaitForSeconds(0.35f);
        }
        _source.Stop();
        _source.volume = startVolume;
    }
    private IEnumerator Anim()
    {
        for (int i = 0; i < _anim.Length; i++)
        {
            _anim[i].SetBool("IsAttack", true);
            yield return new WaitForSeconds(0.1f);
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
        distanceCurrent = Vector2.Distance(transform.position, hc.transform.position);
        float onePercentDist = (distanceFirst * 0.01f);
        float percentDist = (100 - (distanceCurrent / onePercentDist)) * 0.01f;
        Debug.Log(distanceFirst + " " + distanceCurrent + " " + percentDist);
        while (Time.time < startTime + duration)
        {
            var t = (Time.time - startTime) / duration;
            hc.rb.velocity = -direction * forceToPushObject * forceCurve.Evaluate(t);
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine(QTE());
        hc.SetBehaviorFloating();
        for (int i = 0; i < _anim.Length; i++)
        {
            _anim[i].SetBool("IsAttack", false);
        }
        StopCoroutine(PushObject());
        StartCoroutine(FadeAudio());
        if (Random.Range(0, 2) == 1)
        {
            UIDirector.SendMessage(Messages.handsStopped, 2f);
        }
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
            // hc.transform.rotation = Quaternion.Euler(0, 0, 180);
            hc.DirectionState = DirectionState.left;
        }

    }
}
