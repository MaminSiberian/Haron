using System.Collections.Generic;
using UnityEngine;

public class EquipmentSouls : MonoBehaviour
{
    public Dictionary<int, int> Souls = new Dictionary<int, int>();
    public List<SoulsInfo> SoulsId = new List<SoulsInfo>();
    public UIManager UIManager;
    private Soul _soul;
    private Marina _marina;
    private bool _isPickUp = false;
    private bool _isMarina = false;

    private void Start()
    {
        UIManager.UpdateSoulsCount(SoulsId.Count.ToString());
        _soul = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            _isPickUp = true;
            _soul = collision.GetComponent<Soul>();
            Debug.Log("x");
        }
        if(collision.gameObject.CompareTag("Marina"))
        {
            _marina = collision.GetComponent<Marina>();
            _isMarina = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            _isPickUp = false;
            _soul = null;
        }
        if (collision.gameObject.CompareTag("Marina"))
        {
            _isMarina = false;
            _marina = null;
        }

    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _isPickUp)
        {
            SoulsId.Add(_soul.GetSoulsInfo());
            Destroy(_soul.gameObject);
            _soul = null;
            _isPickUp = false;
            UIManager.UpdateSoulsCount(SoulsId.Count.ToString());

        }
        if(Input.GetKeyDown(KeyCode.F) && _isMarina && !_isPickUp && SoulsId.Count > 0)
        {
            
            for (int i = 0; i < SoulsId.Count; i++)
            {
                SoulsInfo soul = SoulsId[i];
                if (soul.Marinaid == _marina.index)
                {
                   _marina.countSouls++;
                   SoulsId.Remove(soul);
                   UIManager.UpdateSoulsCount(SoulsId.Count.ToString());
                }
            }
            
            UIManager.UpdateSoulsCount(SoulsId.Count.ToString());


        }
    }

    
    
    public void test(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            _isPickUp = true;
            if (Input.GetKeyDown(KeyCode.F) && _isPickUp)
            {
                //Souls.Add(Souls.Count + 1, collision.GetComponent<Soul>().GetMarinaId());
                //Destroy(collision.gameObject);
                //UIManager.UpdateSoulsCount(Souls.Count.ToString());

                SoulsId.Add(collision.GetComponent<Soul>().GetSoulsInfo());
                Destroy(collision.gameObject);
                UIManager.UpdateSoulsCount(SoulsId.Count.ToString());
                Debug.Log("x");
                _isPickUp = false;
            }
        }
    }
}
