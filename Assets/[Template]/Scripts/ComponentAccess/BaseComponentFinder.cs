using Template;
using UnityEngine;

namespace Template
{
    public class BaseComponentFinder : Singleton<BaseComponentFinder>
    {
        private RectTransform _canvasRT;
        private Camera _cameraMain;
        private Canvas _canvas;
        private BaseLevelManager _levelManager;
        private InputComponentManager _inputComponentManager;
        private TimeScalingManager _timeScalingManager;
        
        
 
        public TimeScalingManager TimeScalingManager
        {
            get
            {
                if (_timeScalingManager == null)
                    _timeScalingManager = FindObjectOfType<TimeScalingManager>();

                return _timeScalingManager;
            }
        }
        public InputComponentManager InputComponentManager
        {
            get
            {
                if (_inputComponentManager == null)
                    _inputComponentManager = FindObjectOfType<InputComponentManager>();

                return _inputComponentManager;
            }
        }
        public BaseLevelManager LevelManager
        {
            get
            {
                if (_levelManager == null)
                    _levelManager = FindObjectOfType<BaseLevelManager>();

                return _levelManager;
            }
        }
        public Camera CameraMain
        {
            get
            {
                if (_cameraMain == null)
                    _cameraMain = Camera.main;

                return _cameraMain;
            }
        }
        public RectTransform CanvasRT
        {
            get
            {
                if (_canvasRT == null)
                    _canvasRT = FindObjectOfType<Canvas>().GetComponent<RectTransform>();

                return _canvasRT;
            }
        }
        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                    _canvas = FindObjectOfType<Canvas>();

                return _canvas;
            }
        }
 
    }
}