

using UnityEngine;
using TMPro;

namespace Template
{
    public class TextBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txt;

        public void SetText(string str)
        {
            txt.text = str;
        }
    }
}