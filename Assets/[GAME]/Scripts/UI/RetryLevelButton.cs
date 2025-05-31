using Template;

namespace _GAME_.Scripts.UI
{
    public class RetryLevelButton : BaseClickButton
    {
        public override void OnClick()
        {
            BaseComponentFinder.instance.LevelManager.ReLoadCurrentLevel();
        }
    }
}