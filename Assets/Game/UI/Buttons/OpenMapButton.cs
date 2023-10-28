using UnityEngine;

namespace UI
{
    public class OpenMapButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            UIDirector.OpenMap();
        }
    }
}
