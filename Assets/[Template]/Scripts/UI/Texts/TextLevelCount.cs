using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class TextLevelCount : TextBase
    {
        private void OnEnable()
        {
            // SetLevelText(ComponentFinder.instance.LevelManager.fakeLevel);
        }

        public void SetLevelText(int level)
        {
            SetText("Level " + level.ToString());
        }
    }
}