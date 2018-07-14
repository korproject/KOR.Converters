using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOR.Converters.CustomParsers
{
    public static class CustomParsers
    {
        /// <summary>
        /// Int custom tryParser
        /// </summary>
        /// <param name="value">string numeric value</param>
        /// <param name="defaultValue"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int IntParse(this string value, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int output = int.TryParse(value, out output) ? output : defaultValue;

            output = output < minValue ? defaultValue : output;
            output = output > maxValue ? defaultValue : output;

            return output;
        }

        public static double Double(this string value, double defaultValue = 0, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            double output = double.TryParse(value, out output) ? output : defaultValue;

            output = output < minValue ? defaultValue : output;
            output = output > maxValue ? defaultValue : output;

            return output;
        }

        public static bool Int2Bool(this int value, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            if (value > 0 && value <= int.MaxValue)
            {
                return true;
            }

            return false;
        }
    }
}
