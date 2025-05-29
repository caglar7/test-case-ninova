using _GAME.Scripts.ItemTransferModule;
using DG.Tweening;
using Template;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game/Settings/StickmanSettings", fileName = "StickmanSettings", order = 0)]
public class StickmanSettings : ScriptableObject
{
    private const string Path = "GameSettings/StickmanSettings";

    private static StickmanSettings _instance;
    public static StickmanSettings Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<StickmanSettings>(Path);

                if(_instance == null)
                    Debug.LogError("not found in resources");
            }
            return _instance;
        }
    }

    public TweenRotationSettings rotationToFrontSettings;
    public TweenRotationSettings rotationToBackSettings;
}