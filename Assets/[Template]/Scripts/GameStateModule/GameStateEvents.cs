using System;

namespace Template
{
    public static class GameStateEvents
    {
        public static Action OnLevelStarted;
        public static Action OnLevelCompleted;
        public static Action OnLevelFailed;
    }
}