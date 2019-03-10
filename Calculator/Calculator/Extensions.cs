using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Extensions
    {
        public static int Find(this string stringInput, char charToFind, bool startFromEnd)
        {
            int pos = 0;

            if (startFromEnd)
            {
                for (int i = stringInput.Length - 1; i >= 0; i--)
                {
                    if (stringInput[i] == charToFind)
                    {
                        pos = i;
                        return pos;
                    }
                }
            }
            else
            {
                for (int i = 0; i < stringInput.Length; i++)
                {
                    if (stringInput[i] == charToFind)
                    {
                        pos = i;
                        return pos;
                    }
                }
            }

            return -1;
        }
    }
}
