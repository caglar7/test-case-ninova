using System;
using System.Collections.Generic;
using _GAME.Scripts.ItemTransferModule;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;


namespace Template
{
    [CreateAssetMenu(menuName = "Template/Settings/ColorSettings", fileName = "ColorSettings", order = 0)]
    public class ColorSettings : ScriptableObject
    {
        private const string Path = "GameSettings/ColorSettings";

        private static ColorSettings _instance;
        public static ColorSettings Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = Resources.Load<ColorSettings>(Path);

                    if(_instance == null)
                        Debug.LogError("not found in resources");
                }
                return _instance;
            }
        }

        [Header("Colors Defined")] 
        public List<ColorDefinition> colorDefinitions = new List<ColorDefinition>();

        public Color GetDefinedColor(ColorType colorType)
        {
            foreach (ColorDefinition colorDef in colorDefinitions)
            {
                if (colorType == colorDef.colorType)
                {
                    return colorDef.color;
                }
            }

            Debug.LogWarning("color definition not found");
            return Color.white;
        }
    }

    [Serializable]
    public class ColorDefinition
    {
        public ColorType colorType;
        public Color color;
    }
}

