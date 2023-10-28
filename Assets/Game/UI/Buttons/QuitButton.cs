using UnityEngine;

namespace UI
{
    public class QuitButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            Application.Quit();
        }
    }
}
