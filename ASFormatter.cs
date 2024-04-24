using System;
using System.Collections.Generic;
using System.Text;

namespace AccountSystem
{
    internal static class ASFormatter
    {
        internal static bool IsDouble(string value)
        {
            double doubleNumber = 0;
            return double.TryParse(value, out doubleNumber);
        }
        internal static bool IsInteger(string value)
        {
            int i = 0;
            return Int32.TryParse(value, out i);

        }
        internal static bool IsNumeric(string value)
        {
            int i = 0; double d = 0;
            return (Int32.TryParse(value, out i) || double.TryParse(value, out d));

        }
        internal static bool IsNumeric(object value)
        {
            int i = 0; double d = 0;
            return (Int32.TryParse(value.ToString(), out i) || double.TryParse(value.ToString(), out d));

        }
    }
}
