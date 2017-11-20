using System;

namespace Triangulation
{
    public static class Preconditions
    {
        public static bool CheckNotNull(object value)
        {
            return value != null;
        }

        public static void CheckNotNull(object value, string paramName)
        {
            if (value == null) throw new ArgumentNullException(paramName);
        }

        public static void CheckNotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(paramName);
        }

        public static void CheckOutOfRange(int value, int min, int max, string paramName)
        {
            if (value < min || value > max) throw new ArgumentOutOfRangeException(paramName);
        }
    }
}