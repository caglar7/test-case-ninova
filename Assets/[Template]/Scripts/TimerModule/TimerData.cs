

using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class TimerData  
{
    public float period;
    
    [ReadOnly]
    public float currentValue;
}