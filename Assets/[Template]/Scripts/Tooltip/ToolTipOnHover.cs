
using System.Collections;
using Template;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;

    public float stayDuration = 1.5f;

    private Coroutine coroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTip.instance.Show(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.instance.Hide();
    }
}