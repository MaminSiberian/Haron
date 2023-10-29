using NaughtyAttributes;
using UI;
using UnityEngine;

public class UITester : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float hpChange;
    [SerializeField] private float dashChange;

    private GameplayUI gameplayUI;

    private void Start()
    {
        gameplayUI = FindAnyObjectByType<GameplayUI>();
        LevelDirector.SendNewQuestTarget(target);
    }
    private void Update()
    {
        gameplayUI.SetDashValue(dashChange);
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
    [Button]
    private void SetDash()
    {
        gameplayUI.SetDashValue(dashChange);
    }
}
