using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI souls;
    [SerializeField] private TextMeshProUGUI keys;

    private void Awake()
    {
        SetHPValue(100);
        SetCoinsValue();
        SetSoulsValue();
        SetKeysValue();
    }
    private void OnEnable()
    {
        Wallet.OnCoinsValueChanged += SetCoinsValue;
        LevelDirector.OnSoulDeliveredEvent += SetSoulsValue;
        LevelDirector.OnKeysValueChangedEvent += SetKeysValue;
    }
    private void OnDisable()
    {
        Wallet.OnCoinsValueChanged -= SetCoinsValue;
        LevelDirector.OnSoulDeliveredEvent -= SetSoulsValue;
        LevelDirector.OnKeysValueChangedEvent -= SetKeysValue;
    }

    public void SetHPValue(int value)
    {
        health.text = "HP: " + value.ToString();
    }    
    public void SetCoinsValue()
    {
        coins.text = "Coins: " + Wallet.coinsValue.ToString();
    }    
    public void SetSoulsValue()
    {
        souls.text = "Souls: " + LevelDirector.deliveredSoulsCounter.ToString();
    }
    public void SetKeysValue()
    {
        keys.text = "Keys: " + LevelDirector.keysCounter.ToString();
    }
}
