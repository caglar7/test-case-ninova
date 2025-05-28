using System.Collections.Generic;
using UnityEngine;

namespace Template.Scripts.DeveloperHelper
{
    public class SetAveragePosition : EditorApplyReset
    {
        [Header("Transforms")]
        public List<Transform> transforms = new List<Transform>();
        
        [Header("Target Transform")]
        public Transform targetTransform;

        private Vector3 _total;
        
        protected override void _Apply()
        {
            _total = Vector3.zero;
            foreach (var t in transforms)
            {
                _total += t.position;
            }
            targetTransform.position = _total / transforms.Count;
        }

        protected override void _Reset()
        {
            transforms.Clear();
            targetTransform = null;
        }
    }
}