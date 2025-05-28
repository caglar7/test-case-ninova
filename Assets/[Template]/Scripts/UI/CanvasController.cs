

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
        [Header("References")] 
        public Canvas canvas;
        public RectTransform canvasRT;
        
        [Header("General")]
        public CanvasType startPanel;

        [Header("Elements")] 
        public SourceUI goldSourceUI;
        
        private SubCanvas[] subCanvases;
        
        
        public void Init()
        {
            subCanvases = GetComponentsInChildren<SubCanvas>(true);
            
            SwitchCanvas(startPanel);
        }

        
        // public void AddCoin(Transform point) => AddCoin(point.position);
        // public void AddCoin(Vector3 spawnPosition)
        // {
        //     CoinUI coinUI = ComponentFinder.instance.ObjectCreator.CreateCoinUI();
        //     
        //     coinUI.transform.SetParent(canvas.transform);
        //     
        //     coinUI.RectTransform.anchoredPosition = UIUtility.WorldToUI(
        //                                                 spawnPosition, 
        //                                                 canvas,
        //                                                 canvasRT,
        //                                                 ComponentFinder.instance.MainCamera);
        //     
        //     DotweenExtensions.SafePunchScale(coinUI.transform, 1.3f, 1f, .3f, (() =>
        //     {
        //         ItemTransfer.TransferBaseUI(
        //             coinUI, 
        //             goldSourceUI.icon.rectTransform, 
        //             canvasRT,
        //             UISettings.Instance.addCoinSettings,
        //             () =>
        //             {
        //                 ComponentFinder.instance.SourceManager.AddSource(SourceType.Gold, coinUI.sourceAmount);
        //                 ComponentFinder.instance.ObjectCreator.RemoveCoinUI(coinUI);
        //             });
        //     }));
        // }
        
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


