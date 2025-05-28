namespace Template
{
    public static class ConversionUtility
    {
        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public static bool IntToBool(int value)
        {
            return (value == 1) ? true : false;
        }
        
        public static string SecondsToTimeString(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            return $"{minutes}:{remainingSeconds:D2}";
        }
    }
}