using System.Collections.Generic;
using UI;
using UnityEngine;

public class EquipmentSouls : MonoBehaviour
{
    public int countSoul;
    public List<SoulsInfo> SoulsId = new List<SoulsInfo>();
    private Soul _soul;
    private Marina _marina;
    private bool _isPickUp = false;
    private bool _isMarina = false;
    private Marina[] marinas;

    private void Start()
    {
        
        _soul = null;
        marinas = GameObject.FindObjectsOfType<Marina>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            _isPickUp = true;
            _soul = collision.GetComponent<Soul>();
           
        }
        if(collision.gameObject.CompareTag("Marina"))
        {
            _marina = collision.GetComponent<Marina>();
            _isMarina = true;
            for (int i = 0; i < SoulsId.Count; i++)
            {
                SoulsInfo soul = SoulsId[i];
                if (soul.Marinaid == _marina.index)
                {
                    UIDirector.ActivePressF();
                }
            }
            
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
            UIDirector.DisablePressF();
        }

    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _isPickUp)
        {
            SoulsId.Add(_soul.GetSoulsInfo());
            
            foreach(var marina in marinas)
            {
                if(marina.index == _soul.GetMarinaId())
                {
                    LevelDirector.SendNewQuestTarget(marina.transform);
                }
            }
            LevelDirector.OnSoulDelivered();
            Destroy(_soul.gameObject);
            _soul = null;
            _isPickUp = false;
            
            

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
                    Wallet.GetCoins(2);
                }
            }
            LevelDirector.SendNewQuestTarget(null);
            UIDirector.DisablePressF();





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
                Debug.Log("x");
                _isPickUp = false;
            }
        }
    }
    public void AddSoul(SoulsInfo info)
    {
        SoulsId.Add(info);
    }
    public void RemoveSoul(SoulsInfo info)
    {
        SoulsId.Remove(info);
    }
    
}
