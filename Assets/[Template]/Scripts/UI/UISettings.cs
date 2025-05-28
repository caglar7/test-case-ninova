using _GAME.Scripts.ItemTransferModule;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "Template/Settings/UISettings", fileName = "UISettings", order = 0)]
public class UISettings : ScriptableObject
{
    private const string Path = "GameSettings/UISettings";

    private static UISettings _instance;
    public static UISettings Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<UISettings>(Path);

                if(_instance == null)
                    Debug.LogError("not found in resources");
            }
            return _instance;
        }
    }

    [Header("Adding Coins")] 
    public TweenMoveSettings addCoinSettings;
}