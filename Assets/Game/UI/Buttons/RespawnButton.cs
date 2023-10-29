
namespace UI
{
    public class RespawnButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            LevelDirector.Respawn();
        }
    }
}
