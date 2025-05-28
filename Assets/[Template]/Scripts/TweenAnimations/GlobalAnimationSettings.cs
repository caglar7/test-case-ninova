using _GAME.Scripts.ItemTransferModule;
using DG.Tweening;
using Template;
using UnityEngine;

[CreateAssetMenu(menuName = "Template/Settings/GlobalAnimationSettings", fileName = "GlobalAnimationSettings", order = 0)]
public class GlobalAnimationSettings : ScriptableObject
{
    private const string Path = "GameSettings/GlobalAnimationSettings";

    private static GlobalAnimationSettings _instance;
    public static GlobalAnimationSettings Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<GlobalAnimationSettings>(Path);

                if(_instance == null)
                    Debug.LogError("not found in resources");
            }
            return _instance;
        }
    }

    public PunchScaleSettings punchScale;
}