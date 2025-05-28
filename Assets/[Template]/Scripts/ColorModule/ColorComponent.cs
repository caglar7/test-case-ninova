using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Template
{
    public class ColorComponent : BaseMono, IModuleInit
    {
        [Header("Fields")] 
        public ColorType currentColor;
        
        [Header("References")]
        public List<ColorUnit> units = new List<ColorUnit>();

        private ColorUnit _currentUnit;

        // edit mode call
        public ColorType CurrentColorEditMode
        {
            get
            {
                ColorType current = default(ColorType);
                foreach (ColorUnit unit in units)
                {
                    if (unit.obj.activeSelf)
                    {
                        current = unit.colorType;
                        break;
                    }
                }
                return current;
            }
        }
        
        public void Init()
        {
            foreach (ColorUnit unit in units)
            {
                if (unit.obj.activeSelf)
                {
                    currentColor = unit.colorType;
                    _currentUnit = unit;
                    break;
                }
            }
        }
        
        public void SetColor(ColorType colorType)
        {
            foreach (ColorUnit unit in units)
            {
                if (unit.colorType == colorType)
                {
                    unit.obj.SetActive(true);
                    _currentUnit = unit;
                }
                else
                {
                    unit.obj.SetActive(false);
                }
            }
            currentColor = colorType;
        }

        public T GetComponentOnUnit<T>() where T : MonoBehaviour
        {
            if (_currentUnit is null)
                return null;
            
            if (_currentUnit.obj.TryGetComponent(out MonoBehaviour mono))
            {
                return mono as T;
            }

            return null;
        }
    }
}