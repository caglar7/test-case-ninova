


using UnityEngine;


namespace Template
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ButtonSwitchPanel : BaseClickButton
    {
        public CanvasType targetPanel;

        public override void OnClick()
        {
            CanvasController.instance.SwitchCanvas(targetPanel);
        }
    }
}