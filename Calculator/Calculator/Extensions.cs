using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Extensions
    {
        public static int Find(this string stringInput, char charToFind)
        {
            int pos = 0;

            for (int i = 0; i < stringInput.Length; i++)
            {
                if (stringInput[i] == charToFind)
                {
                    pos = i;
                    return pos;
                }
            }

            return -1;
        }
    }
}
