using Haron;
using UnityEngine;

public class BirdEnemy : MonoBehaviour, IDamagable, IHPÑontroller
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField, Range(0.5f, 10)] private float moveSpeed = 1.5f;
    [SerializeField, Range(1, 7)] private float distanceVisible = 4f;
    [SerializeField, Range(1, 10)] private int damage = 2;
    [SerializeField] private float cooldownTime = 1f;
    [SerializeField, Range(0.5f, 2f)] private float distanceForDamage = 0.7f;
    private float currentcdTime;
    private bool isAttack;
    private GameObject _player;

    public int MaxHP { get => maxHealth;  }
    public int CurrentHP { get => health; }


    public void GetDamage(int damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            Death();
        }
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        health = maxHealth;
        currentcdTime = 0f;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance < distanceVisible)
        {
            if (distance > distanceForDamage)
            {

                transform.position = Vector2.Lerp(transform.position,
                    _player.transform.position, moveSpeed * Time.deltaTime);
                
            }
            else if(distance <= distanceForDamage)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceVisible);
        Gizmos.DrawWireSphere(transform.position, distanceForDamage);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        if(currentcdTime < 0f)
        {
            //Play Animation with event method GiveDamage()
            Debug.Log("Attack");
            currentcdTime = cooldownTime;
        }
        
    }
    public void GiveDamage()
    {
        _player.GetComponent<HaronController>().GetDamage(damage);
    }
}
