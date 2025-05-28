using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class LoadingScreenHandler : MonoBehaviour
    {
        [Header("Slider UI")]
        public SliderCustom slider;

        [Header("Scenes To Check")]
        public List<SceneSo> scenesToCheck = new List<SceneSo>();

        [Header("Loading Screen Scene")]
        public SceneSo loadingScene;
    }
}
