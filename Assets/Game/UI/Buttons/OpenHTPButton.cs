using UnityEngine;

namespace UI
{
    public class OpenHTPButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            UIDirector.OpenHTP();
        }
    }
}
