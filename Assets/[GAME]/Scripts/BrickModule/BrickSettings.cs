using _GAME.Scripts.ItemTransferModule;
using DG.Tweening;
using Template;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Settings/BrickSettings", fileName = "BrickSettings", order = 0)]
public class BrickSettings : ScriptableObject
{
    private const string Path = "GameSettings/BrickSettings";

    private static BrickSettings _instance;
    public static BrickSettings Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<BrickSettings>(Path);

                if(_instance == null)
                    Debug.LogError("not found in resources");
            }
            return _instance;
        }
    }

    public TweenJumpSettings brickJumpToRoad;
    public TweenRotationSettings brickRotateToRoad;
}