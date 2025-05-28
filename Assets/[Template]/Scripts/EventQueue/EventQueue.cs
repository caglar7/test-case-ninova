using System;
using System.Collections.Generic;

namespace Template.EventQueue
{
    public class EventQueue : BaseMono, IModuleInit
    {
        public Timer timer;
        private List<Action> _events = new List<Action>();
        
        
        public void Init()
        {
            timer.RemoveListeners();
            timer.OnTimerDone += HandleOneEvent;
            timer.StartTimer();
        }

        public void AddEvent(Action action)
        {
            _events.Add(action);
        }
        
        private void HandleOneEvent()
        {
            if (_events.Count > 0)
            {
                _events[0]?.Invoke();
                _events.RemoveAt(0);
            }
        }
    }
}