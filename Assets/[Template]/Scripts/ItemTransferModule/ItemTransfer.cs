
using System;
using DG.Tweening;
using Template;
using UnityEngine;

namespace _GAME.Scripts.ItemTransferModule
{
    public static class ItemTransfer
    {
        public static void TransferMove(
        BaseMono obj, 
        Vector3 targetPos, 
        TweenMoveSettings moveSettings,
        Action onComplete = null)
        {
            obj.Transform.DOMove(
                targetPos,
                moveSettings.moveDuration)
                .SetEase(moveSettings.moveEase)
                .OnComplete((() =>
                {
                    onComplete?.Invoke();
                })
            );
        }
        
        public static void TransferMove(
            BaseMono obj, 
            Vector3 targetPos, 
            TweenMoveSpeedSettings moveSpeedSettings,
            Action onComplete = null)
        {
            float duration = Vector3.Distance(targetPos, obj.Transform.position) / moveSpeedSettings.moveSpeed;
            
            obj.Transform.DOMove(
                    targetPos,
                    duration)
                .SetEase(moveSpeedSettings.moveEase)
                .OnComplete((() =>
                    {
                        onComplete?.Invoke();
                    })
                );
        }
        public static void TransferMoveLocal(
            BaseMono obj, 
            Vector3 targetPos, 
            TweenMoveSettings moveSettings,
            Action onComplete = null)
        {
            obj.Transform.DOLocalMove(
                    targetPos,
                    moveSettings.moveDuration)
                .SetEase(moveSettings.moveEase)
                .OnComplete((() =>
                    {
                        onComplete?.Invoke();
                    })
                );
        }


        public static void TransferJump(
        BaseMono obj, 
        Vector3 targetPosition, 
        TweenJumpSettings jumpSettings,
        Action onComplete = null)
        {
            obj.Transform.DOJump(
                targetPosition,
                jumpSettings.jumpPower,
                jumpSettings.numOfJumps,
                jumpSettings.jumpDuration)
                .SetEase(jumpSettings.jumpEase)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                }
            );
        }

        public static void TransferBaseUI(
        BaseUI baseUI, 
        RectTransform targetRT, 
        RectTransform canvasRT, 
        TweenMoveSettings tweenSettings,
        Action onComplete = null)
        {
            baseUI.RectTransform.DOAnchorPos(
                UIUtility.GetRootCanvasPosition(targetRT, canvasRT),
                tweenSettings.moveDuration)
                    .SetEase(tweenSettings.moveEase)
                    .OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    }
            );
        }
    }
}