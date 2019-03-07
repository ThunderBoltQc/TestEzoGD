using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        private StringValidator validator;

        public Calculator()
        {
            validator = new StringValidator();
        }

        public string Calculate(string operationString)
        {
            return Substract(operationString);
        }

        private string Add(string addition)
        {
            string[] numbers = addition.Split('+');

            if (IsFloatNumber(ref numbers[0]) || IsFloatNumber(ref numbers[1]))
            {
                float sum = float.Parse(numbers[0]) + float.Parse(numbers[1]);
                return sum.ToString();
            }
            else
            {
                int sum = Int32.Parse(numbers[0]) + Int32.Parse(numbers[1]);
                return sum.ToString();
            }
        }

        private string Substract(string substraction)
        {
            // To put in another function

            int middleSignPos = 0;
            bool needToAddBack = false;

            middleSignPos = substraction.Find('-');

            if (middleSignPos == 0)
            {
                needToAddBack = true;
                substraction = substraction.Remove(0, 1);
            }

            middleSignPos = substraction.Find('-');

            string[] numbers = new string[2];

            numbers[0] = substraction.Substring(0, middleSignPos);
            numbers[1] = substraction.Substring(middleSignPos + 1, (substraction.Length - numbers[0].Length) - 1);

            if (needToAddBack)
            {
                numbers[0] = "-" + numbers[0];
            }

            // End

            float difference = float.Parse(numbers[0]) - float.Parse(numbers[1]);
            return difference.ToString();
        }

        private string Multiply(string multiplication)
        {
            string[] numbers = multiplication.Split('*');

            if (IsFloatNumber(ref numbers[0]) || IsFloatNumber(ref numbers[1]))
            {
                float product = float.Parse(numbers[0]) * float.Parse(numbers[1]);
                return product.ToString();
            }
            else
            {
                int product = Int32.Parse(numbers[0]) * Int32.Parse(numbers[1]);
                return product.ToString();
            }
        }

        private string Divide(string division)
        {
            string[] numbers = division.Split('/');

            if (numbers[1] == "0")
            {
                return "ERREUR !";
            }

            
            float sum = float.Parse(numbers[0]) / float.Parse(numbers[1]);
            return sum.ToString();
            
        }

        private bool IsFloatNumber(ref string number)
        {
            if (number.Contains(","))
            {
                number = number.Replace(",", ".");
                return true;
            }

            if (number.Contains("."))
            {
                return true;
            }

            return false;
        }
    }
}
