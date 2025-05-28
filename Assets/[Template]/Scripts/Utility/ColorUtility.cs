

using UnityEngine;

namespace Template
{
    public static class ColorUtility
    {
        private static Color _color;
        public static Color GetTransparentColor(Color color)
        {
            _color = color;
            _color.a = 0f;
            return _color;
        }
    }
}