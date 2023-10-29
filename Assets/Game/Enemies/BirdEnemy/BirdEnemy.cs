using Haron;
using System.Collections;
using UI;
using UnityEngine;

public class BirdEnemy : MonoBehaviour, IDamagable, IHPController
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField, Range(0.5f, 10)] private float moveSpeed = 1.5f;
    [SerializeField, Range(1, 7)] private float distanceVisible = 4f;
    [SerializeField, Range(1, 10)] private int damage = 2;
    [SerializeField] private float cooldownTime = 1f;
    [SerializeField, Range(0.5f, 4f)] private float distanceForDamage = 0.7f;
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _death;
    [SerializeField] private float dalayDeath;
    private AudioSource _source;
    private float currentcdTime;
    private bool isAttack;
    private GameObject _player;
    private bool isAlive = true;

    public int MaxHP { get => maxHealth;  }
    public int CurrentHP { get => health; }


    private void Awake()
    {
        
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        _anim.SetTrigger("Hit");
        _source.PlayOneShot(_hit);
        if(health <= 0f)
        {
            _anim.SetTrigger("Death");
            Death();
        }
        LevelDirector.AddObject(this.gameObject);
    }
    private void OnEnable()
    {
        LevelDirector.OnRespawn += Reset;
    }
    private void OnDisable()
    {
        LevelDirector.OnRespawn -= Reset;
    }
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        health = maxHealth;
        currentcdTime = 0f;
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isAlive)
        {
            float distance = Vector2.Distance(transform.position, _player.transform.position);
            if (distance < distanceVisible)
            {
                
                if (distance > distanceForDamage)
                {

                    transform.position = Vector2.Lerp(transform.position,
                        _player.transform.position, moveSpeed * Time.deltaTime);

                }
                else if (distance <= distanceForDamage)
                {
                    Attack();
                }
            }
            else
            {
                isAttack = false;
            }
            currentcdTime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceVisible);
        Gizmos.DrawWireSphere(transform.position, distanceForDamage);
    }

    public void Death()
    {
        isAlive = false;
        _anim.SetTrigger("Death");
        _source.PlayOneShot(_death);
        StartCoroutine(StartDeath());
    }

    public void Attack()
    {
        if(currentcdTime < 0f)
        {
            _player.GetComponent<HaronController>().GetDamage(damage);
            _anim.SetTrigger("Attack");
            currentcdTime = cooldownTime;
            _source.PlayOneShot(_attack);
        }
        
    }
    public void GiveDamage()
    {
        _player.GetComponent<HaronController>().GetDamage(damage);
    }

    private IEnumerator StartDeath()
    {
        yield return new WaitForSeconds(dalayDeath);
        Destroy(gameObject);
    }

    public void Reset()
    {
        isAlive = true;
        isAttack = false;
        health = maxHealth;
    }
}
