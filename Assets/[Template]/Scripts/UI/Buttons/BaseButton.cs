

using UnityEngine;
using UnityEngine.UI;

namespace Template
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : BaseMono
    {
        [SerializeField]
        public float initDelay;

        public virtual void EnableButton()
        {
            Button.enabled = true;
        }
        public virtual void DisableButton()
        {
            Button.enabled = false;
        }

        public virtual void FadeIn()
        {

        }
        public virtual void FadeOut()
        {

        }
    }
}