using System;
using System.Collections.Generic;
using _GAME_.Scripts.BrickModule;
using _GAME.Scripts.ItemTransferModule;
using Sirenix.OdinInspector;
using Template;
using TMPro;
using UnityEngine;

namespace _GAME_.Scripts.BridgeModule
{
    public class Bridge : BaseMono, IModuleInit
    {
        [Header("Settings")] 
        public float xDistance = .2f;
        public float zDistance = .4f;
        
        [Header("References")] 
        public TextMeshPro txtCount;
        public Transform blueprintHolder;
        public Transform brickHolder;
        public List<Transform> pathPoints = new();
        
        [Header("Resources")] 
        public GameObject blueprintPrefab;
        
        private List<BrickBlueprint> _blueprints = new();
        private List<Brick> _bricks = new();

        public bool IsBridgeComplete => (_bricks.Count == _blueprints.Count) ? true : false;
        public int NextBlueprintIndex => _bricks.Count;
        public List<BrickBlueprint> BrickBlueprints => _blueprints;
        public int BrickCount => _bricks.Count;

        public Action OnBridgeCompleted;

        private int _visualBrickCount;
            
        public void Init()
        {
            _blueprints = new();
            foreach (BrickBlueprint blueprint in GetComponentsInChildren<BrickBlueprint>())
            {
                _blueprints.Add(blueprint);
            }
            SetText();
            OnBridgeCompleted = null;
            _visualBrickCount = 0;
        }
        public void AddBrick(Brick brick, float jumpDelay, Action onDropDone)
        {
            if (_bricks.Count >= _blueprints.Count)
            {
                Destroy(brick.gameObject);
                return;
            }
            
            _bricks.Add(brick);

            brick.Transform.SetParent(brickHolder);
            
            GeneralUtils.Delay(jumpDelay, () =>
            {
                JumpToBlueprintIndex(
                    brick, 
                    _bricks.IndexOf(brick),
                    () =>
                    {
                        onDropDone?.Invoke();
                        SetText();
                        
                        HandleLastBrick();
                    }
                );
            });
        }
        public int GetNextColorCount()
        {
            ColorType color = _blueprints[NextBlueprintIndex].colorComponent.currentColor;
            int count = 0;
            for (int i = NextBlueprintIndex; i < _blueprints.Count; i++)
            {
                if (_blueprints[i].colorComponent.currentColor == color)
                    count++;
                else
                    break;
            }
            return count;
        }
        private void HandleLastBrick()
        {
            _visualBrickCount++;
            
            if (_visualBrickCount >= _blueprints.Count)
            {
                OnBridgeCompleted?.Invoke();
            }
        }
        private void SetText()
        {
            txtCount.text = (_blueprints.Count - _bricks.Count).ToString();
        }
        private void JumpToBlueprintIndex(Brick brick, int index, Action onJumpDone = null)
        {
            ItemTransfer.TransferJump(
                brick, 
                _blueprints[index].Transform.position,
                BrickSettings.Instance.brickJumpToRoad,
                () =>
                {
                    _blueprints[index].gameObject.SetActive(false);
                    onJumpDone?.Invoke();
                }
            );
            
            TweenRotation.RotateGlobal(
                brick, 
                Vector3.zero,
                BrickSettings.Instance.brickRotateToRoad);
        }
        

        [Button]
        private void AddBlueprint(ColorType colorType)
        {
            _blueprints.Add(Instantiate(blueprintPrefab).GetComponent<BrickBlueprint>());
            _blueprints.Add(Instantiate(blueprintPrefab).GetComponent<BrickBlueprint>());

            _blueprints[^2].Transform.SetParent(blueprintHolder);
            _blueprints[^1].Transform.SetParent(blueprintHolder);

            _blueprints[^2].Transform.localPosition =
                new Vector3(-xDistance, 0f, (_blueprints.Count / 2 - 1) * xDistance);
            
            _blueprints[^1].Transform.localPosition =
                new Vector3(xDistance, 0f, (_blueprints.Count / 2 - 1) * xDistance);
            
            _blueprints[^2].colorComponent.SetColor(colorType);
            _blueprints[^1].colorComponent.SetColor(colorType);
        }
        [Button]
        private void ClearBlueprints()
        {
            int count = _blueprints.Count;

            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(_blueprints[0].gameObject);
                _blueprints.RemoveAt(0);
            }
        }
    }
}