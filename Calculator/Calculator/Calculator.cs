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
        private const char ADDITION_SIGN = '+';
        private const char SUBSTRACTION_SIGN = '-';
        private const char MULTIPLICATION_SIGN = '*';
        private const char DIVISION_SIGN = '/';
        private const char OPENING_PARANTHESE = '(';
        private const char CLOSING_PARANTHESE = ')';

        private const string ERROR_MESSAGE = "ERREUR !";


        #region Public Methods
        public Calculator()
        {
            validator = new StringValidator();
        }


        /// <summary>
        /// Calcule des chaînes d'opérations
        /// </summary>
        /// <param name="operationString">la chaîne d'opérations à efectuer</param>
        /// <returns>le résultat de la chaîne</returns>
        public string Calculate(string operationString)
        {
            // 1. Find parantheses
            if (operationString.Contains(OPENING_PARANTHESE))
            {
                // Extract Paranthese content
                // Call this function with the content
                // Replace parenthese content with result then remove parantheses

                string parContent = ExtractParantheseContent(operationString);
                string parContentWithParantheses = OPENING_PARANTHESE + parContent + CLOSING_PARANTHESE;

                operationString = operationString.Replace(parContentWithParantheses, Calculate(parContent));
            }
            // 2. Powers
            // 3. Multiplications / Divisions
            operationString = DoAllMultiplcationsDivisions(operationString);

            if (operationString.Contains(ERROR_MESSAGE))
            {
                return ERROR_MESSAGE;
            }

            // 4. Additions / Substractions
            string answer = DoAdditionsSubstractions(operationString);

            return answer;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Additionne deux nombres
        /// </summary>
        /// <param name="addition">addition à effectuer</param>
        /// <returns>la somme sous forme de chaîne de caractères</returns>
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

        /// <summary>
        /// Fait une soustraction entre deux nombres
        /// </summary>
        /// <param name="substraction">la soustraction à effectuer</param>
        /// <returns>la différence sous forme de chaîne de caractères</returns>
        private string Substract(string substraction)
        {           
            string[] numbers = GetNumbers(substraction);
                  
            float difference = float.Parse(numbers[0]) - float.Parse(numbers[1]);
            return difference.ToString();
        }

        /// <summary>
        /// Fait une multiplication entre deux nombres
        /// </summary>
        /// <param name="multiplication">la multiplication à effectuer</param>
        /// <returns>le produit sous forme de châine de caractères</returns>
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

        /// <summary>
        /// Fait une division entre deux nombres
        /// </summary>
        /// <param name="division">la division à effectuer</param>
        /// <returns>le quotien sous forme de chaînes de caratères</returns>
        private string Divide(string division)
        {
            string[] numbers = division.Split(DIVISION_SIGN);

            if (numbers[1] == "0")
            {
                return ERROR_MESSAGE;
            }

            
            float sum = float.Parse(numbers[0]) / float.Parse(numbers[1]);
            return sum.ToString();
            
        }

        /// <summary>
        /// Puisque le - peut aussi être utilisé pour représenter un nombre négatif, cette fonction s'occupe de faire la 
        /// distinction et retourne les deux nombres utilisés pour la soustraction
        /// </summary>
        /// <param name="substraction">la soustraction à efectuer</param>
        /// <returns>les deux termes de la soustraction</returns>
        private string[] GetNumbers(string substraction)
        {
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

            return numbers;
        }

        /// <summary>
        /// Détermine si le nombre est un nombre à virgule flotante ou non
        /// </summary>
        /// <param name="number">le nombre à regarder</param>
        /// <returns>Vrai si nombre à virgule flottante, Faux sinon</returns>
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

        /// <summary>
        /// Fonction qui exécute toutes les additions / soustractions d'une chaîne d'opérations
        /// </summary>
        /// <param name="operationString">la chaîne d'opérations à effectuer</param>
        /// <returns>Le résultat de la chaîne</returns>
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

        /// <summary>
        /// Fonction qui détermine combien il reste de termes dans une châine d'additions ou de soustractions
        /// </summary>
        /// <param name="operationString">la chaîne d'opérations à analyser</param>
        /// <returns>le nombre de termes restants</returns>
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

        /// <summary>
        /// Fonction qui détermine si le - spécifié à la position en parramètre est un opérateur ou une marque pour 
        /// spécifier un nombre négatif
        /// </summary>
        /// <param name="operationString">la chaâine d'opérations à analyser</param>
        /// <param name="index">position du caractère</param>
        /// <returns>Vrai si le - est un opérateur de soustraction, Faux sinon</returns>
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

        /// <summary>
        /// Fonction qui sert de délimiteur pour la première addition / soustraction de la chaîne d'opérations
        /// </summary>
        /// <param name="operationString">la chaine d'opérations à analyser</param>
        /// <returns>la position du deuxième opérateur (délimiteur)</returns>
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

        /// <summary>
        /// Fonction qui détermine si le caractère passé en paramètre est un opérateur ou non 
        /// </summary>
        /// <param name="character">le caractère à analyser</param>
        /// <returns>Vrai si un caractère, Faux sinon</returns>
        private bool IsOperator(char character)
        {
            if (character == MULTIPLICATION_SIGN|| character == ADDITION_SIGN || character == DIVISION_SIGN)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fonction qui effectue toutes les multiplications / divisions d'une chaîne d'opérations
        /// </summary>
        /// <param name="operationString">la chaîne à effectuer</param>
        /// <returns>la chaîne résultante</returns>
        private string DoAllMultiplcationsDivisions(string operationString)
        {
            int firstIndex = FindFirstMulDivOperator(operationString);

            while (firstIndex != -1 && !operationString.Contains(ERROR_MESSAGE))
            {  
                int operationStart = FindNextOrPreviousOperatorIndex(operationString, firstIndex, true); // = GetOperationStart(operationString);
                int operationEnd = FindNextOrPreviousOperatorIndex(operationString, firstIndex, false); // = GetOperationEnd(operationString);

                string operation = operationString.Substring(operationStart, (operationEnd - operationStart) + 1);

                if (operation.Contains(MULTIPLICATION_SIGN.ToString()))
                {
                    operationString = operationString.Replace(operation, Multiply(operation));
                }
                else
                {
                    operationString = operationString.Replace(operation, Divide(operation));
                }

                firstIndex = FindFirstMulDivOperator(operationString);
            }

            return operationString;
        }

        /// <summary>
        /// Fonction qui retourne le nombre de multiplications / divisions à effectuer dans la chaîne
        /// </summary>
        /// <param name="operationString">la chaîne à analyser</param>
        /// <returns>le nombre de multiplications / divisions</returns>
        private int GetMultiplicationDivisionCount(string operationString)
        {
            int count = 0;

            for (int i = 0; i < operationString.Length; i++)
            {
                if (operationString[i] == MULTIPLICATION_SIGN || operationString[i] == DIVISION_SIGN)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Fonction qui retourne l'index du premier opérateur de multiplication / division
        /// </summary>
        /// <param name="operationString">la chaîne d'opérations à analyser</param>
        /// <returns>la position du premier opérateur de multiplication / division</returns>
        private int FindFirstMulDivOperator(string operationString)
        {
            for (int i = 0; i < operationString.Length; i++)
            {
                if (operationString[i] == MULTIPLICATION_SIGN || operationString[i] == DIVISION_SIGN)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Fonction qui retourne la position de l'opérateur précédent ou suivant de l'index donné. Utilisé pour délimiter l'opération lorsqu'il s'agit d'une
        /// multiplication / division puisqu'elles ont priorité
        /// </summary>
        /// <param name="operationString">la chaîne d'opérations à effectuer</param>
        /// <param name="startPoint">l'index de l'opérateur</param>
        /// <param name="goBackwards">parcoure la chaîne vers l'opérateur précédent si vrai, le suivant si faux</param>
        /// <returns>l'index de l'opérateur suivant ou précedant dans la chaîne</returns>
        private int FindNextOrPreviousOperatorIndex(string operationString, int startPoint, bool goBackwards)
        {
            if (goBackwards)
            {
                for (int i = startPoint - 1; i >= 0; i--)
                {
                    if (IsOperator(operationString[i]) || IsMinusAnOperator(operationString, i))
                    {
                        return i + 1;
                    }  
                }

                return 0;
            }
            else
            {
                for (int i = startPoint + 1; i < operationString.Length; i++)
                {
                    if (IsOperator(operationString[i]) || IsMinusAnOperator(operationString, i))
                    {
                        return i - 1;
                    } 
                }

                return operationString.Length - 1;
            }
        }

        /// <summary>
        /// Fonction qui extrait le contenu d'une paranthèse
        ///
        /// </summary>
        /// <param name="operationString">La châine d'opérations à analyser</param>
        /// <returns>le contenu de la paranthèse</returns>
        private string ExtractParantheseContent(string operationString)
        {
            int openingParPos = operationString.Find(OPENING_PARANTHESE, false);
            int closingParPos = operationString.Find(CLOSING_PARANTHESE, true);

            string content = operationString.Substring(openingParPos + 1, (closingParPos - openingParPos) - 1);

            return content;
        }
        #endregion
    }
}
