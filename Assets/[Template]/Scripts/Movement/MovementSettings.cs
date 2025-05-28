





using System.Collections.Generic;
using DG.Tweening;
using Template;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Template/Settings/Movement Settings", order = 0)]
public class MovementSettings : ScriptableObject
{
    private static string path = "GameSettings/MovementSettings";

    private static MovementSettings _instance;
    public static MovementSettings Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<MovementSettings>(path);

                if(_instance == null)
                    Debug.LogError("not found in resources");
            }
            return _instance;
        }
    }

    [Range(.1f, 20f)] public float speedDefault = 5f;
    [Range(.1f, 20f)] public float speedHigh = 6f;
    [Range(.1f, 20f)] public float crouchSpeed = 3f;
    public float crouchTime = .5f;
    public Ease crouchSwitchEase = Ease.Linear;
}