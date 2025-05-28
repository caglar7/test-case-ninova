


using _Template_.Scripts.UI;
using Template;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseClickButton : BaseButton 
{
    public virtual void Start()
    {
        Button.onClick.AddListener(() => {

            GeneralUtils.Delay(initDelay, () =>
            {
                OnClick();
                UIEvents.OnButtonClicked?.Invoke();
            });
        });
    }

    public abstract void OnClick();
}
