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
    private void OnEnable()
    {
        LevelDirector.OnKeysValueChangedEvent += ChangeKey;
    }
    private void OnDisable()
    {
        LevelDirector.OnKeysValueChangedEvent -= ChangeKey;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            canOpenDoor = true;
            _door = collision.GetComponent<Door>();
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
        if(Input.GetKeyDown(KeyCode.F) && keys.Count > 0 && _door != null)
        {
            _door.Open();
            
            keys.Remove(keys[Random.Range(0, keys.Count)]);
            LevelDirector.WasteKey();
            Debug.Log("Open");
        }

        if(Input.GetKeyDown(KeyCode.F) && _key != null)
        {
            ChangeKey();
            LevelDirector.GetKey();
            
            Destroy(_key.gameObject);
            _key = null;
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

    public void ChangeKey()
    {
        keys.Add(keys.Count + 1);
    }
    
    
}
