using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GAME
{
    public class TextToolTip : MonoBehaviour
    {
        public SpriteRenderer backgroundSpriteRenderer;
        public TextMeshPro txt;
        public float paddingX = 0.2f;
        public float paddingY = 0.2f;

        public void SetText(string newText)
        {
            if (backgroundSpriteRenderer == null)
            {
                Debug.LogError("Background SpriteRenderer is not assigned in TextToolTip script!");
                return;
            }
            if (txt == null)
            {
                Debug.LogError("TextMesh is not assigned in TextToolTip script!");
                return;
            }

            Show();

            txt.text = newText;

            // Adjust the background size to fit the text
            Vector2 textSize = txt.GetPreferredValues(newText);
            backgroundSpriteRenderer.size = textSize + new Vector2(paddingX, paddingY);
        }

        public void Show()
        {
            if(backgroundSpriteRenderer.gameObject.activeInHierarchy) 
                backgroundSpriteRenderer.gameObject.SetActive(true);

            if(txt.gameObject.activeInHierarchy) 
                txt.gameObject.SetActive(true);
        }

        public void Hide() 
        {
            backgroundSpriteRenderer.gameObject.SetActive(false);

            txt.gameObject.SetActive(false);
        }
    }
}
