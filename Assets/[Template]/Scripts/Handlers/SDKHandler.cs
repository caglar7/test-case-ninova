//
//
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Analytics;
//
// namespace Template
// {
//     public class SDKHandler : MonoBehaviour, IEvents
//     {
//         #region Variables
//
//         [SerializeField] private bool _isSDKOpen;
//
//         #endregion // Variables
//
//
//         #region Events
//         private void OnEnable()
//         {
//             SubscribeToEvents();
//         }
//
//         private void OnDisable()
//         {
//             UnsubscribeToEvents();
//         }
//
//         public void SubscribeToEvents()
//         {
//             // unity analytics
//             BasicEvent.LevelStartSDK += UpdateLevelLoadUnityAnalytics;
//             BasicEvent.LevelCompleteSDK += UpdateLevelCompletedUnityAnalytics;
//             BasicEvent.LevelFailSDK += UpdateLevelFailedUnityAnalytics;
//         }
//
//         public void UnsubscribeToEvents()
//         {
//             // unity analytics
//             BasicEvent.LevelStartSDK -= UpdateLevelLoadUnityAnalytics;
//             BasicEvent.LevelCompleteSDK -= UpdateLevelCompletedUnityAnalytics;
//             BasicEvent.LevelFailSDK -= UpdateLevelFailedUnityAnalytics;
//         }
//
// #endregion // Events
//
// #region Unity Analytics Methods
//
//         public void UpdateLevelLoadUnityAnalytics()
//         {
//             if (_isSDKOpen)
//             {
//                 AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelLoad", new Dictionary<string, object> {
//                 { "Level", LevelManager.instance.fakeLevel }});
//             }
//             else
//             {
//                 Debug.Log("Load UA");
//             }
//         }
//
//         private void UpdateLevelCompletedUnityAnalytics()
//         {
//             if (_isSDKOpen)
//             {
//                 AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelCompleted", new Dictionary<string, object> {
//                     { "Level", LevelManager.instance.fakeLevel },
//                     { LevelManager.instance.fakeLevel.ToString() +" Level_Orj:", LevelManager.instance.originalLevel }});
//             }
//             else
//             {
//                 Debug.Log("Success UA");
//             }
//         }
//
//         private void UpdateLevelFailedUnityAnalytics()
//         {
//             if (_isSDKOpen)
//             {
//                 AnalyticsResult analyticsResult = Analytics.CustomEvent("LevelFailed", new Dictionary<string, object> {
//                     { "Level", LevelManager.instance.fakeLevel },
//                     { LevelManager.instance.fakeLevel.ToString() +" Level_Orj:", LevelManager.instance.originalLevel}});
//             }
//             else
//             {
//                 Debug.Log("Failed UA");
//             }
//         }
// #endregion
//     }
// }
