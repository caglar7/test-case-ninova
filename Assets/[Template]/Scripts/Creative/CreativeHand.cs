using UnityEngine;

namespace Template
{
    public class CreativeHand : BaseMono
    {
        public RectTransform rectTransform;
        public GameObject free;
        public GameObject holdDown;
        
        private void Update()
        {
            rectTransform.anchoredPosition = Input.mousePosition - (new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Input.GetMouseButtonDown(0))
            {
                free.gameObject.SetActive(false);
                holdDown.gameObject.SetActive(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                free.gameObject.SetActive(true);
                holdDown.gameObject.SetActive(false);
            }
        }
    }
}