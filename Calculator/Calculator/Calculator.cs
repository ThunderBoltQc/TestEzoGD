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
        private char ADDITION_SIGN = '+';
        private char SUBSTRACTION_SIGN = '-';
        private char MULTIPLICATION_SIGN = '*';
        private char DIVISION_SIGN = '/';

        public Calculator()
        {
            validator = new StringValidator();
        }

        public string Calculate(string operationString)
        {
            // 1. Find parantheses
            // 2. Powers
            // 3. Multiplications / Divisions
            // 4. Additions / Substractions

            string answer = DoAdditionsSubstractions(operationString);

            return answer;
        }


        private string Add(string addition)
        {
            string[] numbers = addition.Split(ADDITION_SIGN);

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

            middleSignPos = substraction.Find(SUBSTRACTION_SIGN, false);

            if (middleSignPos == 0)
            {
                needToAddBack = true;
                substraction = substraction.Remove(0, 1);
            }

            middleSignPos = substraction.Find(SUBSTRACTION_SIGN, false);

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
            string[] numbers = multiplication.Split(MULTIPLICATION_SIGN);

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
            string[] numbers = division.Split(DIVISION_SIGN);

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

        private string DoAdditionsSubstractions(string operationString)
        {
            int termCount = CountTerms(operationString);

            while (termCount > 1)
            {
                int secondOperatorPos = FindSecondOperator(operationString);

                if (secondOperatorPos == -1)
                {
                    // It's a single addition / substraction
                    if (operationString.Contains(ADDITION_SIGN.ToString()))
                    {
                        operationString = operationString.Replace(operationString, Add(operationString));
                    }
                    else
                    {
                        operationString = operationString.Replace(operationString, Substract(operationString));
                    }
                }
                else
                {
                    // Multiple additions / substraction left
                    string operation = operationString.Substring(0, secondOperatorPos);

                    if (operation.Contains(ADDITION_SIGN.ToString()))
                    {
                        operationString = operationString.Replace(operation, Add(operation));
                    }
                    else
                    {
                        operationString = operationString.Replace(operation, Substract(operation));
                    }
                }
                
                termCount--;
            }

            return operationString;
        }

        private int CountTerms(string operationString)
        {
            int operatorCount = 0;

            for (int i = 0; i < operationString.Length; i++)
            {
                if (operationString[i] == ADDITION_SIGN)
                {
                    operatorCount++;
                }

                if (operationString[i] == SUBSTRACTION_SIGN && IsMinusAnOperator(operationString, i))
                {
                    operatorCount++;
                }
                
            }

            return operatorCount + 1;
        }

        private bool IsMinusAnOperator(string operationString, int index)
        {
            if (operationString[index] == SUBSTRACTION_SIGN && index != 0)
            {
                if (validator.IsNumber(operationString[index - 1]))
                {
                    return true;
                }
            }

            return false;
        }

        private int FindSecondOperator(string operationString)
        {
            bool isFirstOperatorFound = false;

            for (int i = 0; i < operationString.Length; i++)
            {
                if (operationString[i] == ADDITION_SIGN || 
                    (operationString[i] == SUBSTRACTION_SIGN && IsMinusAnOperator(operationString, i)))
                {
                    if (isFirstOperatorFound)
                    {
                        return i;
                    }
                    else
                    {
                        isFirstOperatorFound = true;
                    }
                }
            }

            return -1;
        }
    }
}
