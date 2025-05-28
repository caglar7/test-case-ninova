using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template
{
    public class SmoothColorChanger : BaseMono
    {
        [Header("References")]
        public MeshRenderer meshRenderer;
        public Material fadedColorMaterial;
        public Material vibrantColorMaterial;
        
        [Header("Fields")]
        public List<int> materialIndexes = new List<int>();
        public bool fadeOnStart = false;

        private void Start()
        {
            if (fadeOnStart)
            {
                SetColor(fadedColorMaterial.color);
            }
        }

        public void SetFaded() => SetColorSmoothly(fadedColorMaterial.color);
        
        public void SetVibrant() => SetColorSmoothly(vibrantColorMaterial.color);
        
        
        private void SetColorSmoothly(Color targetColor, float duration = .5f)
        {
            foreach (var materialIndex in materialIndexes)
            {
                meshRenderer.materials[materialIndex].DOColor(targetColor, duration);
            }
        }

        private void SetColor(Color targetColor)
        {
            foreach (var materialIndex in materialIndexes)
            {
                meshRenderer.materials[materialIndex].color = targetColor;
            }
        }
        
        // private void SetColorForMaterial(Color newColor)
        // {
        //     if(_materialPropertyBlock == null)
        //         _materialPropertyBlock = new MaterialPropertyBlock();
        //     
        //     meshRenderer.GetPropertyBlock(_materialPropertyBlock, materialIndex);
        //     _materialPropertyBlock.SetColor("_Color", newColor);
        //     meshRenderer.SetPropertyBlock(_materialPropertyBlock, materialIndex);
        // }
    }
}