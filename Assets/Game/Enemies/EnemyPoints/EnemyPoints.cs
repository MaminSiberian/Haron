using Haron;
using NaughtyAttributes.Test;
using System.Collections;
using UnityEngine;

public class EnemyPoints : MonoBehaviour, IDamagable, IHPController
{
    private GameObject _player;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] internal int damage;
    [SerializeField] private float _distanceVisible;
    [SerializeField] internal float _cooldownTime;
    internal float currentCooldownTime;
    [SerializeField] private Transform[] _points;
    [SerializeField, Range(0.5f, 5f)] private float distanceForDamage = 0.7f;
    [SerializeField] private GameObject _parentGameObject;
    [SerializeField] private AudioClip[] _soundsIdle;
    [SerializeField] private AudioClip _soundDeath;
    [SerializeField] private AudioClip _soundAttack;
    [SerializeField] private float cooldownSound;
    

    [SerializeField] private Rigidbody2D _rb;
    private bool isAttack;
    private bool canMoveToPoints;
    private float currentsoundCooldown;
    private int currentPoint;
    private AudioSource _source;
    private Transform _startpos;
    [SerializeField] private Animator _anim;
    [SerializeField] private float animOffsetTakeDamage;

    public int MaxHP { get => maxHealth; }
    public int CurrentHP { get => health; }


    private void Awake()
    {
        
    }

    private void OnDisable()
    {
        LevelDirector.OnRespawn -= OnReset;
    }
    private void OnEnable()
    {
        LevelDirector.OnRespawn += OnReset;
    }
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _source = GetComponent<AudioSource>();
        canMoveToPoints = true;
        isAttack = false;
        currentCooldownTime = _cooldownTime;
        _startpos = transform;
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(_player.transform.position, transform.position);
        if (distance < _distanceVisible)
        {
            isAttack = true;

        }
        else
        {
            isAttack = false;
        }
        if (!isAttack && canMoveToPoints)
        {
            Move();
        }
        if (isAttack)
        {
            if (distance < distanceForDamage)
            {
                if(currentCooldownTime < 0)
                {
                    _anim.SetTrigger("Attack");
                    StartCoroutine(Attack());
                    currentCooldownTime = _cooldownTime;
                }
            }
            else if(distance > distanceForDamage)
            {
                transform.position = Vector2.Lerp(transform.position,
                    _player.transform.position, _moveSpeed * Time.deltaTime * 0.2f);
            }
            canMoveToPoints = false;
        }
        if(!canMoveToPoints)
        {
            currentCooldownTime -= Time.deltaTime;
        }
        
        if(currentsoundCooldown < 0)
        {
            _source.PlayOneShot(_soundsIdle[Random.Range(0, _soundsIdle.Length - 1)]);
            currentsoundCooldown = cooldownSound;
        }
        else
        {
            currentsoundCooldown -= Time.deltaTime;
        }

    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(animOffsetTakeDamage);
        _source.PlayOneShot(_soundAttack);
        _player.GetComponent<HaronController>().GetDamage(damage);

    }
    
    

    private void Move()
    {
        if (canMoveToPoints)
        {
            transform.position = Vector2.MoveTowards(transform.position, _points[currentPoint].position, _moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _points[currentPoint].position) < 0.1f)
            {
                if (currentCooldownTime <= 0)
                {

                    currentPoint++;
                    currentCooldownTime = _cooldownTime;
                    if (currentPoint > _points.Length - 1)
                    {
                        currentPoint = 0;
                    }
                }
                else
                {
                    currentCooldownTime -= Time.deltaTime;
                }
            }
        }
    }

    public void Death()
    {
        StartCoroutine(StartDeath());
    }

    private IEnumerator StartDeath()
    {
        _source.PlayOneShot(_soundDeath);
        _anim.SetTrigger("Death");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceVisible);
        Gizmos.DrawWireSphere(transform.position, distanceForDamage);
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Death();
        }
        
    }
    public void OnReset()
    {
        transform.position = _startpos.position;
        health = maxHealth;
        canMoveToPoints = true;
        isAttack = false;
    }
}
