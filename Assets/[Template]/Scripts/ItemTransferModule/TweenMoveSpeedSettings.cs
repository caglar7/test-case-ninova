using System;
using DG.Tweening;

namespace _GAME.Scripts.ItemTransferModule
{
    [Serializable]
    public class TweenMoveSpeedSettings 
    {
        public float moveSpeed = 1f;
        public Ease moveEase = Ease.Linear;
    }
}