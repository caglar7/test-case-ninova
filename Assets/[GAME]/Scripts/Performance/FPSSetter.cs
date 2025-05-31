using Template;
using UnityEngine;

namespace _GAME.Scripts.Utility
{
    public class FPSSetter : BaseMono, IModuleInit
    {
        public int targetFPS = 60;
        
        public void Init()
        {
            Application.targetFrameRate = targetFPS;
        }
    }
}