using System;
using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.StageModule;
using _GAME_.Scripts.StickmanModule;
using _Template_.Scripts.UI;
using Template;

namespace _GAME_.Scripts.EventListeners
{
    public class AudioEventListener : BaseMono, IModuleInit
    {
        public AudioController controller;
        
        public void Init()
        {
            controller.Init();

            UIEvents.OnButtonClicked += HandleButtonClicked;
            BridgeEvents.OnBrickDropped += HandleBrickDropped;
            StickmanEvents.OnMadeMove += HandleMadeMove;
            StageEvents.OnStageCompleted += HandleStageCompleted;
            GameStateEvents.OnLevelCompleted += HandleLevelCompleted;
            GameStateEvents.OnLevelFailed += HandleLevelFailed;
        }
        private void OnDisable()
        {
            UIEvents.OnButtonClicked += HandleButtonClicked;
            BridgeEvents.OnBrickDropped -= HandleBrickDropped;
            StickmanEvents.OnMadeMove -= HandleMadeMove;
            StageEvents.OnStageCompleted -= HandleStageCompleted;
            GameStateEvents.OnLevelCompleted += HandleLevelCompleted;
            GameStateEvents.OnLevelFailed += HandleLevelFailed;
        }

        private void HandleLevelFailed()
        {
            controller.PlayOneShot(SoundType.LevelFailed);
        }

        private void HandleLevelCompleted()
        {
            controller.PlayOneShot(SoundType.LevelCompleted);
        }

        private void HandleStageCompleted(Stage obj)
        {
            controller.PlayOneShot(SoundType.StagePassed);
        }

        private void HandleMadeMove()
        {
            controller.PlayOneShot(SoundType.Tap);
        }

        private void HandleBrickDropped()
        {
            controller.PlayOneShot(SoundType.BrickDropped);
        }

        private void HandleButtonClicked()
        {
            controller.PlayOneShot(SoundType.Button);
        }
    }
}