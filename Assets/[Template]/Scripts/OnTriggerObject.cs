
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// tramboline, key inherits from this,
/// anything that is triggered by player inherits this
/// </summary>

namespace Template
{
    public class OnTriggerObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        [HideInInspector] public bool triggerOnce = true;

        private bool _isUsed = false;

        private void OnTriggerEnter(Collider other) {
            
            if(!_isUsed && other.TryGetComponent<T>(out T hitComponent))
            {
                _isUsed = (triggerOnce) ? true : false;
                OnTrigger(other.transform);
            }
        }

        public virtual void OnTrigger(Transform obj)
        {
            print("Not impletemented");
        }

    }
}
