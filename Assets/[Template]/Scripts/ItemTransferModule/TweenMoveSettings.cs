using System;
using DG.Tweening;

namespace _GAME.Scripts.ItemTransferModule
{
    [Serializable]
    public class TweenMoveSettings
    {
        public float moveDuration = 1f;
        public Ease moveEase = Ease.Linear;
    }
}