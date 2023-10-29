using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private HaronController hc;

    private void Start()
    {        
        hc = FindObjectOfType<HaronController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(1);
        if (collision.GetComponent<IDamagable>() != null)
        {
            Debug.Log(2);
            collision.GetComponent<IDamagable>().GetDamage(hc.damage);
        }
        
    }
    
}
