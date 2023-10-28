using Haron;
using UnityEngine;

public class EnemyPoints : MonoBehaviour, IDamagable, IHPÑontroller
{
    private GameObject _player;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float _distanceVisible;
    [SerializeField] private float _cooldownTime;
    private float currentCooldownTime;
    [SerializeField] private Transform[] _points;
    [SerializeField, Range(0.5f, 5f)] private float distanceForDamage = 0.7f;
    [SerializeField] private GameObject _parentGameObject;
    
    [SerializeField] private Rigidbody2D _rb;
    private bool isAttack;
    private bool canMoveToPoints;
    private bool cooldown;
    private int currentPoint;
    [SerializeField]private Animator _anim;

    public int MaxHP { get => maxHealth; }
    public int CurrentHP { get => health; }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        canMoveToPoints = true;
        isAttack = false;
        cooldown = false;
        currentCooldownTime = _cooldownTime;
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(_player.transform.position, transform.position);
        if(distance < _distanceVisible)
        {
            isAttack = true;
        }
        else 
            isAttack = false;

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
                    //PlayAnimation with event
                    
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
        
        

    }

    private void Attack()
    {
        _player.GetComponent<HaronController>().GetDamage(damage);
        currentCooldownTime = _cooldownTime;

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
        Destroy(_parentGameObject);
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
}
