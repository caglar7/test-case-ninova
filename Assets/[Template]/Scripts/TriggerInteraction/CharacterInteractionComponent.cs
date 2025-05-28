using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class CharacterInteractionComponent : MonoBehaviour
    {
        public Action<Transform, bool> onTriggerStateModified;
        public Transform LastInteractedObj => ItemsTriggered[^1];
        
        protected readonly List<Transform> ItemsTriggered = new List<Transform>();
        private BaseCharacter _character;

        private void Awake()
        {
            _character = GetComponentInParent<BaseCharacter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var newTransform = other.transform;
            if (!ItemsTriggered.Contains(newTransform))
            {
                onTriggerStateModified?.Invoke(newTransform, true);
                OnInteractStart(newTransform);
                ItemsTriggered.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var newTransform = other.transform;
            if (ItemsTriggered.Contains(newTransform))
            {
                onTriggerStateModified?.Invoke(newTransform, false);
                OnInteractEnd(newTransform);
                ItemsTriggered.Remove(other.transform);
            }
        }
        
        protected virtual void OnInteractStart(Transform interact)
        {
            if (interact.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnInteractStart(transform);
            }
        }

        protected virtual void OnInteractEnd(Transform interact)
        {
            if (interact.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnInteractEnd(transform);
            }
        }
    }
}