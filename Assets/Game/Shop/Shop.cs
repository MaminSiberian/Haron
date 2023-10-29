using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Item
{
    HP,
    Dash,
    Damage,
    Key
}

public static class Shop
{
    public static Dictionary<Item, int> shopItems {  get { return _shopItems; } }

    public static event Action<Item> OnItemPurchasedEvent;

    private static Dictionary<Item, int> _shopItems = new Dictionary<Item, int>()
    {
        { Item.HP, 3 },
        { Item.Dash, 2 },
        { Item.Damage, 2 },
        { Item.Key, 5 },
    };
    public static void BuyItem(Item item)
    {
        if (!Wallet.IsEnoughMoney(1)) return;
        if (!GotItem(item)) return;

        _shopItems = _shopItems.ToDictionary( i => i.Key, i => i.Key == item ? i.Value - 1 : i.Value);
        OnItemPurchasedEvent?.Invoke(item);
        Wallet.WasteCoin();
    }
    public static bool GotItem(Item item)
    {
        return _shopItems.FirstOrDefault(i => i.Key == item).Value > 0;
    }
}
