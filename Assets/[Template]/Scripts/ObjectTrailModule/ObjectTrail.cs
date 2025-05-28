using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.Scripts.ItemModule;
using _GAME.Scripts.ItemTransferModule;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


// colored object creation should be done on inherited objects of this trail


namespace Template
{
    public abstract class ObjectTrail : BaseMono, IModuleInit
    {
        [Header("Base")]
        public PathPoints pathPoints;
        public Transform objectHolder;
        public ObjectCounter objectCounter;

        protected ObjectTrailData objectTrailData;
        
        private List<BaseMono> _objects = new List<BaseMono>();
        private BaseMono _currentObject;
        private int _generatedCount;
        
        
        public virtual void Init()
        {
            SetData();
            pathPoints.Init();
            _generatedCount = 0;
        }

        public virtual void OnDisable()
        {
            // timer.OnTimerDone = null;
        }

    
        public void TryTransferObject()
        {
            if (IsThereAnyObject() == false)
                return;
            
            if (CanTransfer(PeakFirstObject()))
            {
                BaseMono objToTransfer = PopObjectAtIndex(0);
                TransferObject(objToTransfer);
            }
        }

        public BaseMono PeakFirstObject()
        {
            return _objects[0];
        }

        public bool IsThereAnyObject()
        {
            return _objects.Count > 0;
        }
        
        public void GenerateObject()
        {
            _currentObject = CreateObject();

            _currentObject.Transform.name = (_generatedCount++).ToString();

            _currentObject.Transform.localScale = Vector3.one * objectTrailData.objScale;
            
            _currentObject.Transform.SetParent(objectHolder);
            
            _objects.Add(_currentObject);

            SetPositionSnap(_objects.Count - 1);

            // SetRotationSnap(_objects.Count - 1);
            
            // objectCounter.UpdateCounter(_objects.Count + _generationList.Count);
        }
        public void AddObjectSmooth()
        {
            // start from last index and move to target index
        }
        
        protected BaseMono PopObjectAtIndex(int index)
        {
            BaseMono obj = _objects[index];

            _objects.RemoveAt(index);

            // objectCounter.UpdateCounter(_objects.Count);
            
            return obj;
        }
        public void RemoveObject(BaseMono obj)
        {
            _objects.Remove(obj);
        }
        public void RemoveObjectAtIndex(int index)
        {
            _objects.RemoveAt(index);
        }
        
        
        protected IEnumerator PositionRemainingObjects()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                SetPositionSmooth(i);
            }
            yield return 0;
        }
        protected IEnumerator RotateRemainingObjects()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                SetRotationSmooth(i);
            }
            yield return 0;
        }


        protected int GetObjectIndexOnTrail(BaseMono obj)
        {
            return _objects.IndexOf(obj);
        }
        private void SetPositionSnap(int i)
        {
            _objects[i].Transform.position = GetPositionAtIndex(i);
        }
        private void SetPositionSmooth(int i)
        {
            _objects[i].Transform.DOMove(GetPositionAtIndex(i), objectTrailData.tweenDuration);
        }

        private Vector3 GetPositionAtIndex(int index)
        {
            return pathPoints.GetPointOnPath(index * (1f / objectTrailData.maxVisualCount));
        }

        private void SetRotationSnap(int index)
        {
            _objects[index].Transform.eulerAngles = GetRotationAtIndex(index);
        }
        private void SetRotationSmooth(int index)
        {
            _objects[index].Transform.DORotate(GetRotationAtIndex(index), objectTrailData.tweenDuration);
        }
        private Vector3 GetRotationAtIndex(int index)
        {
            return new Vector3(0f, pathPoints.GetPathAngle(GetNormalizedFromIndex(index)), 0f);
        }
        
        private float GetNormalizedFromIndex(int index)
        {
            return index * (1f / objectTrailData.maxVisualCount);
        }
    
        
        public abstract void SetData();
        public abstract BaseMono CreateObject();
        public abstract bool CanTransfer(BaseMono obj);
        public abstract void TransferObject(BaseMono box);
    }
}