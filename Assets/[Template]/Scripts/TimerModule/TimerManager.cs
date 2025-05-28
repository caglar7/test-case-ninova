

using System.Collections.Generic;
using Template;
using UnityEngine;

public class TimerManager : Singleton<TimerManager> 
{
    private List<Timer> _timers = new List<Timer>();
    public List<Timer> Timers => _timers;

    private void Update() 
    {
        for (int i = 0; i < _timers.Count; i++)
        {
            _timers[i].AddDeltaTime();
        }   
    }
}
