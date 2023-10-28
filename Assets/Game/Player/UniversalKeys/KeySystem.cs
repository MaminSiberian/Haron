using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    public List<int> keys = new List<int>();
    private bool canOpenDoor;
    private Door _door;
    private UniversalKey _key;

    private void Start()
    {
        canOpenDoor = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            canOpenDoor = true;
            _door = GetComponent<Door>();
            Debug.Log("Coll");
        }
        if(collision.CompareTag("Key"))
        {
            _key = collision.GetComponent<UniversalKey>();
            Debug.Log("Coll");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _door != null && keys.Count > 0)
        {
            _door.Open();
            
            keys.Remove(keys[Random.Range(0, keys.Count)]);
            Debug.Log(keys.Count);
        }

        if(Input.GetKeyDown(KeyCode.F) && _key != null)
        {
            keys.Add(keys.Count + 1);
            Destroy(_key.gameObject);
            _key = null;
            Debug.Log(keys.Count);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Key") && _key == null)
        {
            _key = collision.GetComponent<UniversalKey>();
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            canOpenDoor = false;
            _door = null;
        }

        if (collision.CompareTag("Key"))
        {
            _key = null;
        }
    }
    
    
}
