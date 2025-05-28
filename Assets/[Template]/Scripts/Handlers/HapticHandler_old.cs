// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace Template
// {
//     public class HapticHandler_old : MonoBehaviour
//     {
//         #region Variables
//
//         private const float HAPTIC_MINIMUM_DELAY = 0.05f;
//         private static float Timer;
//
//         private delegate void TapticDelegate();
//         private static TapticDelegate TapticMethod;
//
//         #endregion // Variables
//
//         #region Start
//
//         private void Start()
//         {
//             ResetTimer();
//         }
//
//         #endregion // Start
//
//         #region Update
//
//         private void Update()
//         {
//             if (Timer > 0)
//             {
//                 Timer -= Time.deltaTime;
//             }
//         }
//
//         #endregion // Update
//
//         #region HapticMethods
//
//     public static void MultipleMediumHaptic(int numHaptic, float delayPerHaptic)
//     {
//         MediumHaptic();
//
//         // numHaptic--;
//         // if (numHaptic > 0) GeneralUtility.Delay(delayPerHaptic, () => { MultipleMediumHaptic(numHaptic, delayPerHaptic); });
//     }
//
//     public static void HeavyHaptic()
//     {
//         //TapticMethod = Taptic.Heavy;
//         ApplyTaptic();
//     }
//
//     public static void MediumHaptic()
//     {
//         //TapticMethod = Taptic.Medium;
//         ApplyTaptic();
//     }
//
//     public static void SuccessHaptic()
//     {
//         //TapticMethod = Taptic.Success;
//         ApplyTaptic();
//     }
//
//     public static void FailureHaptic()
//     {
//         //TapticMethod = Taptic.Failure;
//         ApplyTaptic();
//     }
//
//     private static void ApplyTaptic()
//     {
//         if(Timer <= 0)
//         {
//             TapticMethod();
//             ResetTimer();
//         }
//     }
//
//     private static void ResetTimer() => Timer = HAPTIC_MINIMUM_DELAY;
//
//     #endregion // HapticMethods
//
//     }
// }
