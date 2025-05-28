using UnityEngine;

namespace Template
{
    public class FloatFormatter
    {
        private static readonly TextProperties BILLION = new TextProperties("B", "0.#", 1000000000);
        private static readonly TextProperties MILLION = new TextProperties("M", "0.#", 1000000);
        private static readonly TextProperties THOUSAND = new TextProperties("K", "0.#", 1000);
        private static readonly TextProperties NORMAL = new TextProperties("", "0.#", 1);

        private static TextProperties CACHE_PROPERTIES;

        public static string Format(float value, string postFix = "")
        {
            CalculateTargetTextProperties(value);
            return $"{(value / CACHE_PROPERTIES.Divider).ToString(CACHE_PROPERTIES.FormatText)}{CACHE_PROPERTIES.PostFix + postFix}";
        }

        private static void CalculateTargetTextProperties(float value)
        {
            value = Mathf.Abs(value);
            if (value >= BILLION.Divider)
                CACHE_PROPERTIES = BILLION;
            else if (value >= MILLION.Divider)
                CACHE_PROPERTIES = MILLION;
            else if (value >= THOUSAND.Divider)
                CACHE_PROPERTIES = THOUSAND;
            else
                CACHE_PROPERTIES = NORMAL;
        }

        private class TextProperties
        {
            public string PostFix { get; private set; }
            public string FormatText { get; private set; }
            public int Divider { get; private set; }

            public TextProperties(string postFix, string formatText, int divider)
            {
                PostFix = postFix;
                FormatText = formatText;
                Divider = divider;
            }
        }
    }

}

