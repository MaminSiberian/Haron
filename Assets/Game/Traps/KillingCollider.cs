using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.GetComponent<HaronController>() != null)
        {
            Debug.Log("Killing " + collision.gameObject);
            collision.gameObject.GetComponent<HaronController>().SetBehaviorDeath();
        }

    }
}
