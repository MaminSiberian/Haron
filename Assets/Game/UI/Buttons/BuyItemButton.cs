using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuyItemButton : ButtonBase
    {
        [SerializeField] private Item _item;
        [SerializeField] private Image block;

        public Item item { get { return _item; } }

        protected override void OnEnable()
        {
            base.OnEnable();
            CheckButton();
            Wallet.OnCoinsValueChanged += CheckButton;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Wallet.OnCoinsValueChanged -= CheckButton;
        }
        public void BlockButton()
        {
            block.enabled = true;
            button.enabled = false;
        }
        public void UnblockButton()
        {
            block.enabled = false;
            button.enabled = true;
        }
        protected override void OnButtonClick()
        {
            Shop.BuyItem(item);
        }
        private void CheckButton()
        {
            if (!Wallet.IsEnoughMoney(1) || !Shop.GotItem(item))
            {
                BlockButton();
            }
            else
            {
                UnblockButton();
            }
        }
    }
}
