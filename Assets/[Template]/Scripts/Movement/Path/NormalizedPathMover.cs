using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template.Movement.Path
{
    public class NormalizedPathMover : BaseMovement
    {
        private float _speedNormalized = .1f;
        private float _currentNormalized;
        
        private Vector3 _targetRotation;
        private PathPoints _path;
        private bool _isMoving;

        public Action EventMoveDone;
        
        
        public override void OnUpdate()
        {
            if (_isMoving == false)
                return;
            
            base.OnUpdate();
            
            _currentNormalized += (Time.deltaTime * _speedNormalized);
            _currentNormalized = Mathf.Clamp01(_currentNormalized);
            SetPositionAlongPath(_currentNormalized);

            if (_currentNormalized >= 1f)
            {
                StopMoving();
                EventMoveDone?.Invoke();
            }
        }

        public void StartMoving(PathPoints path, float speed, float startNormalized, Action onMoveDone)
        {
            _path = path;
            
            _isMoving = true;
            
            _speedNormalized = speed;
            
            _currentNormalized = startNormalized;
            
            SetPositionAlongPath(_currentNormalized);

            SetRotationSnap(_currentNormalized);
            
            EventMoveDone = onMoveDone;
        }

        public void StopMoving()
        {
            Transform.DOKill();
            _isMoving = false;
        }

        public void ResumeMoving()
        {
            _isMoving = true;
        }

        private void SetPositionAlongPath(float normalized)
        {
            if (_path == null)
            {
                Debug.LogWarning("Path is null");
                return;
            }
            
            Transform.position = _path.GetPointOnPath(normalized);
            SetRotation(normalized);
        }

        private void SetRotation(float normalized)
        {
            float currentRotation = Transform.eulerAngles.y;
            float targetRotation = _path.GetPathAngle(normalized);

            if (!Mathf.Approximately(targetRotation, currentRotation))
            {
                Transform.DOKill();
                Transform.DORotate(Vector3.up * targetRotation, .2f).SetEase(Ease.Linear);
            }
        }

        private void SetRotationSnap(float normalized)
        {
            Transform.eulerAngles = Vector3.up * _path.GetPathAngle(normalized);
        }
    }
}