using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Scripts.DeveloperHelper
{
    public class CopyPasteTransform : EditorApplyReset
    {
        [Header("Transforms")]
        public List<Transform> copiedTransform = new List<Transform>();
        public List<Transform> pastedTransforms = new List<Transform>();

        protected override void _Apply()
        {
            for (int i = 0; i < pastedTransforms.Count; i++)
            {
                pastedTransforms[i].position = copiedTransform[i].position;
                pastedTransforms[i].eulerAngles = copiedTransform[i].eulerAngles;
                pastedTransforms[i].localScale = copiedTransform[i].localScale;
            }
        }

        protected override void _Reset()
        {
            copiedTransform.Clear();
            pastedTransforms.Clear();
        }
    }
}