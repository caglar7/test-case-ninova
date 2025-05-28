using System.Collections.Generic;
using _GAME_.Scripts.BrickModule;
using _GAME.Scripts.ItemTransferModule;
using Sirenix.OdinInspector;
using Template;
using TMPro;
using UnityEngine;

namespace _GAME_.Scripts.BrickRoadModule
{
    public class BrickRoad : BaseMono, IModuleInit
    {
        [Header("Settings")] 
        public float xDistance = .2f;
        public float zDistance = .4f;

        
        [Header("References")] 
        public TextMeshPro txtCount;
        public Transform blueprintHolder;
        public Transform brickHolder;
        
        [Header("Resources")] 
        public GameObject blueprintPrefab;
        
        
        private List<BrickBlueprint> _blueprints = new();
        private List<Brick> _bricks = new();

        
        public void Init()
        {
            _blueprints = new();
            foreach (BrickBlueprint blueprint in GetComponentsInChildren<BrickBlueprint>())
            {
                _blueprints.Add(blueprint);
            }
        }
        
        
        public void AddBrick(Brick brick)
        {
            _bricks.Add(brick);

            brick.Transform.SetParent(brickHolder);
            
            JumpToBlueprintIndex(brick, _bricks.Count-1);
        }

        private void JumpToBlueprintIndex(Brick brick, int index)
        {
            ItemTransfer.TransferJump(
                brick, 
                _blueprints[index].Transform.position,
                BrickSettings.Instance.brickJumpToRoad,
                () =>
                {
                    _blueprints[index].gameObject.SetActive(false);
                }
            );
            
            ItemRotation.RotateGlobal(
                brick, 
                Vector3.zero,
                BrickSettings.Instance.brickRotateToRoad);
        }


        [Button]
        public void AddBlueprint()
        {
            _blueprints.Add(Instantiate(blueprintPrefab).GetComponent<BrickBlueprint>());
            _blueprints.Add(Instantiate(blueprintPrefab).GetComponent<BrickBlueprint>());

            _blueprints[^2].Transform.SetParent(blueprintHolder);
            _blueprints[^1].Transform.SetParent(blueprintHolder);

            _blueprints[^2].Transform.localPosition =
                new Vector3(-xDistance, 0f, (_blueprints.Count / 2 - 1) * xDistance);
            
            _blueprints[^1].Transform.localPosition =
                new Vector3(xDistance, 0f, (_blueprints.Count / 2 - 1) * xDistance);
        }
        [Button]
        public void ClearBlueprints()
        {
            int count = _blueprints.Count;

            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(_blueprints[0].gameObject);
                _blueprints.RemoveAt(0);
            }
        }
        
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            _OnValidate();
        }

        private void _OnValidate()
        {
        }

#endif
  
    }
}