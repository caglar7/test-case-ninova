

using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Template
{
    public class ToolTip : Singleton<ToolTip>
    {
        public CanvasGroup canvasGroup;
        public RectTransform rectThis;
        public RectTransform rectBack;
        public RectTransform rectText;
        public TextMeshProUGUI txt;

        private bool _isActive = false;
        private float WidthHalf => Screen.width / 2f;
        private float HeightHalf => Screen.height / 2f;

        private void Start() 
        {
            Hide();
        }
        private void Update() 
        {
            if(_isActive == false) return;    

            rectThis.anchoredPosition = Input.mousePosition - new Vector3(WidthHalf, HeightHalf, 0f);
        }




        public void Show(string text)
        {
            canvasGroup.alpha = 1f;
            SetText(text);
            _isActive = true;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0f;
            _isActive = false;
        }

        public void Hide(float duration)
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0f, duration);
        }

        public void SetText(string text)
        {
            txt.text = text;
            txt.ForceMeshUpdate();
            Vector2 textSize = txt.GetRenderedValues(false);
            Vector2 paddingSize = new Vector2(15f, 20f);
            rectBack.sizeDelta = textSize + paddingSize;
            rectText.sizeDelta = textSize;

            rectBack.transform.localPosition = rectBack.sizeDelta / 2f;
            txt.transform.localPosition = rectBack.sizeDelta / 2f;
        }
    }
}
