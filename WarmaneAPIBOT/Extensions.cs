using System;
using System.Collections.Generic;
using System.Text;

namespace WarmaneAPIBOT
{
    public static class Extensions
    {
        public static string FirstCharacterToUpper(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
