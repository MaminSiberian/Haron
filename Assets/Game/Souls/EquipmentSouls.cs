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
    [SerializeField] private Transform _soulPos;
    private bool isSoul;
    private Marina[] marinas;

    private void Start()
    {

        _soul = null;
        _isPickUp = false;
        isSoul = false;
        marinas = GameObject.FindObjectsOfType<Marina>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            if (!isSoul)
            {
                _isPickUp = true;
                _soul = collision.GetComponent<Soul>();
            }
        }
        if(collision.gameObject.CompareTag("Marina"))
        {
            _marina = collision.GetComponent<Marina>();
            _isMarina = true;
            if(_soul != null)
            {
                if(_soul.GetMarinaId() == _marina.index)
                {
                    
                }
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            if (!isSoul)
            {
                
                _soul = null;
            }
            _isPickUp = false;
        }
        if (collision.gameObject.CompareTag("Marina"))
        {
            _isMarina = false;
            _marina = null;
            
        }

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPickUp && !isSoul)
        {

            foreach (var marina in marinas)
            {
                if (marina.index == _soul.GetMarinaId())
                {
                    LevelDirector.SendNewQuestTarget(marina.transform);
                }
            }
            LevelDirector.OnSoulDelivered();
            _soul.gameObject.transform.position = _soulPos.position;

            _isPickUp = false;
            isSoul = true;



        }
        if (Input.GetKeyDown(KeyCode.E) && _isMarina && !_isPickUp && isSoul)
        {

            if (_soul.GetMarinaId() == _marina.index)
            {
                Wallet.GetCoins(2);
                LevelDirector.SendNewQuestTarget(null);
                Destroy(_soul.gameObject);
                _soul = null;
                isSoul = false;
            }
            
        }
        if (_soul != null && isSoul)
        {
            _soul.gameObject.transform.position = _soulPos.position;
        }
    }

    
    
    public void test(Collider2D collision)
    {
        for (int i = 0; i < SoulsId.Count; i++)
        {
            SoulsInfo soul = SoulsId[i];
            if (soul.Marinaid == _marina.index)
            {
                
            }
        }

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
