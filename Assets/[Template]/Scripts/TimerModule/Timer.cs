

using System;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;

public class Timer : MonoBehaviour 
{
    public TimerData timerData;
    public Action OnTimerDone;
    public Action OneSecondPassed;

    private bool _isRunning;
    public bool IsRunning => _isRunning;
    
    private float _durationSecond;

    public void RemoveListeners()
    {
        OnTimerDone = null;
        OneSecondPassed = null;
    }
    public void StartTimer()
    {
        if(TimerManager.instance.Timers.Contains(this) == false)
        {
            TimerManager.instance.Timers.Add(this);
        }

        timerData.currentValue = 0f;

        ResumeTimer();
    }

    public void PauseTimer()
    {
        _isRunning = false;
    }
    public void ResumeTimer()
    {
        _isRunning = true;
    }

    public void AddDeltaTime()
    {
        if (_isRunning == false) return;

        IncrementForSeconds();

        IncrementForPeriod();
    }

    private void IncrementForPeriod()
    {
        timerData.currentValue += Time.deltaTime;

        if (timerData.currentValue >= timerData.period)
        {
            timerData.currentValue = 0f;

            OnTimerDone?.Invoke();
        }
    }

    private void IncrementForSeconds()
    {
        _durationSecond += Time.deltaTime;

        if (_durationSecond >= 1f)
        {
            _durationSecond -= 1f;

            OneSecondPassed?.Invoke();
        }
    }
}