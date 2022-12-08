using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal static class IntExtensions
    {
        public static bool InRange(this int number, int bottom, int top)
        {
            return number >= bottom && number <= top;
        }
    }
}
