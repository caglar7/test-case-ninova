using UnityEngine;

namespace _GAME_.Scripts.StickmanModule
{
    public class StickmanAnimation : BaseMono
    {
        private static readonly int CarryIdle = Animator.StringToHash("CarryIdle");
        private static readonly int CarryRunning = Animator.StringToHash("CarryRunning");
        private static readonly int Running = Animator.StringToHash("Running");

        public void HandleAnimation(StickmanState state)
        {
            switch (state)
            {
                case StickmanState.CarryIdle:
                    Animator.SetTrigger(CarryIdle);
                    break;
                
                case StickmanState.CarryRunning:
                    Animator.SetTrigger(CarryRunning);
                    break;
                
                case StickmanState.Running:
                    Animator.SetTrigger(Running);
                    break;
            }
        }
    }
}