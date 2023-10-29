using NaughtyAttributes;
using UI;
using UnityEngine;

public class UITester : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float hpChange;

    private GameplayUI gameplayUI;

    private void Start()
    {
        gameplayUI = FindAnyObjectByType<GameplayUI>();
        LevelDirector.SendNewQuestTarget(target);
    }

    [Button]
    private void OnSoulDelivered()
    {
        LevelDirector.OnSoulDelivered();
        Debug.Log(LevelDirector.deliveredSoulsCounter);
    }
    [Button]
    private void WasteCoin()
    {
        Wallet.WasteCoin();
    }
    [Button]
    private void GetCoin()
    {
        Wallet.GetCoins();
    }
    [Button]
    private void ChangeHP()
    {
        gameplayUI.SetHPValue(hpChange);
    }
    [Button]
    private void SendDeliverSoul()
    {
        UIDirector.SendMessage(Messages.deliverSoul, 3f);
    }
    [Button]
    private void SendSoulIsWaiting()
    {
        UIDirector.SendMessage(Messages.sounIsWaiting, 3f);
    }
}
