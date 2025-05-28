


// using Scripts.GameScripts.CharacterModule;
using UnityEngine;

namespace Template
{
    public interface IInteractable
    {
        void OnInteractStart(Transform character);
        void OnInteractEnd(Transform character);
    }
}