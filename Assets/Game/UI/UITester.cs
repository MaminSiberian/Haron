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
}
