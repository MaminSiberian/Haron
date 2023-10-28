using System;
using UnityEngine;

public static class Wallet
{
    public static int coinsValue { get; private set; }
    public static event Action OnCoinsValueChanged;

    public static void GetCoins(int value = 1)
    {
        coinsValue += value;
        OnCoinsValueChanged?.Invoke();
    }
    public static bool IsEnoughMoney(int value = 1)
    {
        return coinsValue >= value;
    }
    public static void WasteCoin()
    {
        if (coinsValue <= 0) return;
        coinsValue--;
        OnCoinsValueChanged?.Invoke();
    }
}
