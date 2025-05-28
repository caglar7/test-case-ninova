

using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class TimeScalingManager : Singleton<TimeScalingManager>
    {
        private int _index;

        [SerializeField]
        private List<float> timeScales = new List<float>();

        public void ChangeTimeScale()   
        {
            IncreaseIndex();
            Time.timeScale = timeScales[_index];
            
            print("Time Scale: " + timeScales[_index] + "x");
        }

        private void IncreaseIndex()
        {
            _index++;
            if (_index >= timeScales.Count)
                _index = 0;
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }
}