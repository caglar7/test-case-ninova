

using System;
using System.Collections;
using _GAME.Scripts.ItemTransferModule;
using _Template_.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// main canvas and switching methods
/// 
/// any more new subcanvas added
/// update the enum below
/// </summary>

namespace Template
{
    public class CanvasController : Singleton<CanvasController>, IModuleInit
    {
        [Header("General")]
        public CanvasType startPanel;

        private SubCanvas[] subCanvases;
        
        
        public void Init()
        {
            subCanvases = GetComponentsInChildren<SubCanvas>(true);
            
            SwitchCanvas(startPanel);

            GameStateEvents.OnLevelStarted += GamePanel;
            GameStateEvents.OnLevelCompleted += LevelCompletedPanel;
            GameStateEvents.OnLevelFailed += LevelFailedPanel;
        }
        private void OnDisable()
        {
            GameStateEvents.OnLevelStarted -= GamePanel;
            GameStateEvents.OnLevelCompleted -= LevelCompletedPanel;
            GameStateEvents.OnLevelFailed -= LevelFailedPanel;  
        }
        
        public void GamePanel() => SwitchCanvas(CanvasType.GamePanel);
        public void LevelCompletedPanel() => SwitchCanvas(CanvasType.SuccessPanel);
        public void LevelFailedPanel() => SwitchCanvas(CanvasType.FailPanel);
        
        
        public void SwitchCanvas(CanvasType target)
        {
            StartCoroutine(SwitchCanvasCo(target, 0));
        }
        public void SwitchCanvas(CanvasType target, float initDelay = 0f)
        {
            StartCoroutine(SwitchCanvasCo(target, initDelay));
        }
        
        

        IEnumerator SwitchCanvasCo(CanvasType target, float initDelay)
        {
            yield return new WaitForSeconds(initDelay);

            foreach(SubCanvas sub in subCanvases)
            {
                if (sub.canvasType == target)
                {
                    sub.gameObject.SetActive(true);
                    UIEvents.OnSwitchedPanel?.Invoke(target);
                }
                else sub.gameObject.SetActive(false);
            }
        }
    }
}


