using NaughtyAttributes;
using UnityEngine;

public class UITester : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Awake()
    {
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
}
