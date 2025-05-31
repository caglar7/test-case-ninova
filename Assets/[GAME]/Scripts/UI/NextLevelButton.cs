using _GAME_.Scripts.ComponentAccess;
using Template;

namespace _GAME_.Scripts.UI
{
    public class NextLevelButton : BaseClickButton
    {
        public override void OnClick()
        {
            BaseComponentFinder.instance.LevelManager.LoadNextLevel();
        }
    }
}