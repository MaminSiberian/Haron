using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI souls;
    [SerializeField] private TextMeshProUGUI keys;

    private int maxHP = 100;

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

    public void SetHPValue(float value)
    {
        if (value > maxHP) maxHP = (int)value;

        healthBar.value = value / maxHP;
        health.text = value.ToString();
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
