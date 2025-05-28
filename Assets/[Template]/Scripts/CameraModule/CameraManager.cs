

using UnityEngine;
using Cinemachine;
using Template;
using System.Collections.Generic;
using System;
using System.Collections;
using Sirenix.OdinInspector;

public class CameraManager : BaseMono, IModuleInit
{
    [Header("References")]
    public CinemachineBrain cinemachineBrain;
    public List<CameraUnit> cameraUnits = new List<CameraUnit>();
    
    [Header("Settings")]
    public CameraAngleType defaultAngle = CameraAngleType.Default;
    
    private const int PRIORITY_HIGH = 10;
    private const int PRIORITY_LOW = 0; 

    private CameraAngleType _currentAngleType;
    public CameraAngleType CurrentAngleType => _currentAngleType;


    public void Init()
    {
        ChangeAngle(defaultAngle);
    }

    public void ChangeAngle(int index)
    {
        ChangeAngle(cameraUnits[index].angleType);
    }
    public void ChangeAngle(int index, float duration)
    {
        cameraUnits[index].easeDuration = duration;
        ChangeAngle(cameraUnits[index].angleType);
    }
    public void ChangeAngle(CameraAngleType targetAngleType)
    {
        foreach (CameraUnit unit in cameraUnits)
        {
            if(unit.angleType == targetAngleType)
            {
                cinemachineBrain.m_DefaultBlend.m_Style = unit.ease;

                cinemachineBrain.m_DefaultBlend.m_Time = unit.easeDuration;

                unit.cam.Priority = PRIORITY_HIGH;

                _currentAngleType = targetAngleType;
            }
            else unit.cam.Priority = PRIORITY_LOW;
        }

        CameraEvents.OnAngleChanged?.Invoke(targetAngleType);
    }


    

    IEnumerator FindCameraUnits()
    {
        cameraUnits.AddRange(FindObjectsOfType<CameraUnit>());
        yield return 0;
    }
}