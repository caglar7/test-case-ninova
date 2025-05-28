using System;
using DG.Tweening;

namespace _GAME.Scripts.ItemTransferModule
{
    [Serializable]
    public class TweenJumpSettings
    {
        public float jumpPower = 1f;
        public int numOfJumps = 1;
        public float jumpDuration = .5f;
        public Ease jumpEase = Ease.Linear;
    }
}