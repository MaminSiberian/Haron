using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuyItemButton : ButtonBase
    {
        [SerializeField] private Item _item;
        [SerializeField] private Image block;

        public Item item { get { return _item; } }

        private void Start()
        {
            BlockButton();
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
            Debug.Log("click");
        }
    }
}
