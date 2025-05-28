using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template.Movement.Tween
{
    public class TransformPathMover : BaseMovement
    {
        [Header("Settings")]
        [ReadOnly]
        public float originalSpeed = 1.45f;
        
        public int tweenSegmentCount = 10;

        private float _segmentNormalized;
        private float _segmentMoveDuration;
        private float _targetNormalized;
        private float _currentSpeed;
        private Vector3 _targetSegmentRotation;
        
        private PathPoints _path;
        
        
        
        public void MoveAlongPath(PathPoints path, float speedMult = 1f, Action onMoveDone = null)
        {
            _path = path;

            _targetNormalized = 0f;
            
            _segmentNormalized = 1f / tweenSegmentCount;
            
            _currentSpeed = originalSpeed * speedMult;
            
            float totalMoveDuration = _path.GetPathLength() / _currentSpeed;
            
            _segmentMoveDuration = totalMoveDuration / tweenSegmentCount;
            
            MoveAndRotateToNextSegment(onMoveDone);
        }

        private void MoveAndRotateToNextSegment(Action onMoveDone)
        {
            if (_targetNormalized > 1f)
            {
                onMoveDone?.Invoke();
                return;
            }

            _targetNormalized += _segmentNormalized;
            Transform.DOMove(_path.GetPointOnPath(_targetNormalized), _segmentMoveDuration).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    MoveAndRotateToNextSegment(onMoveDone);
                });

            _targetSegmentRotation = Transform.eulerAngles;
            _targetSegmentRotation.y = _path.GetPathAngle(_targetNormalized);
            Transform.DORotate(_targetSegmentRotation, _segmentMoveDuration);
        }
    }
}