using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private List<BuyItemButton> buttons = new List<BuyItemButton>();

        private void OnEnable()
        {
            CheckButtons();
        }

        private void CheckButtons()
        {
            foreach (var button in buttons)
            {
                Item item = button.item;
                
                if (!Wallet.IsEnoughMoney(1) || !Shop.GotItem(item))
                {
                    button.BlockButton();
                }
                else
                {
                    button.UnblockButton();
                }
            }
        }
    }
}
