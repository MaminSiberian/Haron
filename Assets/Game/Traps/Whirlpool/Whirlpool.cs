using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    [SerializeField] private float forceAttraction;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector3 direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            Debug.LogWarning(collision.gameObject + " run into whirpool");
            rb = collision.GetComponent<Rigidbody2D>();            
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rb != null)
        {
            Debug.Log("attraction");
            direction = (transform.position - collision.transform.position).normalized;
            //rb.velocity = direction * forceAttraction * Time.deltaTime;
            rb.AddForce(forceAttraction * direction);//, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rb = null;
    }
}
