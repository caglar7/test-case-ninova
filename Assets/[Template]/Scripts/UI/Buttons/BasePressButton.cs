


using Template;
using UnityEngine.EventSystems;

public abstract class BasePressButton : BaseButton, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GeneralUtils.Delay(initDelay, () => { OnPressed(); });
    }

    public abstract void OnPressed();
}