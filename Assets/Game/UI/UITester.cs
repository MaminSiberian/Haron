using NaughtyAttributes;
using UnityEngine;

public class UITester : MonoBehaviour
{
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
