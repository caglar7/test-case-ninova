

using UnityEngine;
using UnityEngine.UI;

namespace Template
{
    public class SliderCustom : MonoBehaviour
    {
        public Image fillImage;

        public void SetFillAmount(float normalizedValue)
        {
            fillImage.fillAmount = normalizedValue;
        }
    }
}