using System;
using Template;
using UnityEngine;

namespace _GAME.Scripts.ItemTransferModule
{
    public static class TransferEvents
    {
        public static Action<BaseMono> OnTransferMoveDone;
        public static Action<BaseMono> OnTransferJumpDone;
    }
}