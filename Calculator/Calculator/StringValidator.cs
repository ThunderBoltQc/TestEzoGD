using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class StringValidator
    {
        public StringValidator()
        {

        }

        public bool Validate(string input)
        {
            if (!ValidateFirstCaracter(input))
            {
                return false;
            }

            return true;
        }

        private bool ValidateFirstCaracter(string input)
        {
            // First caracter must be a '(', a number or '-' to be valid
            // If the string is one caracter long, the only caracters allowed are numbers
            if (input.Length == 1)
            {
                if (!IsNumber(input[0]))
                {
                    return false;
                }

            }

            // Make sure that if the string starts with '-' then it must be followed by a number
            if (input[0] == '-')
            { 
                string negNumber = input.Substring(0, 2);

                if (!IsNumber(negNumber))
                {
                    return false;
                }
            }

            //

            return true;
        }

        private bool IsNumber(char caracter)
        {
            if (caracter < 48 || caracter > 57)
            {
                return false;
            }

            return true;
        }

        private bool IsNumber(string input)
        {
            try
            {
                Int32.Parse(input);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
}
