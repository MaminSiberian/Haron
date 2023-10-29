using Haron;
using UnityEngine;

public class PointEnemyAnimatorController : MonoBehaviour
{
    [SerializeField] private EnemyPoints _parent;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }
    public void Attack()
    {
        _player.GetComponent<HaronController>().GetDamage(_parent.damage);
        _parent.currentCooldownTime = _parent._cooldownTime;
    }
}
