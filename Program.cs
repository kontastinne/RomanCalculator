using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RomanCalculator
{

    class RomanCalculator
    {
        /// <summary>
        /// Calculates Roman Values
        /// </summary>
        public void RomanCalculate()
        {
            Console.Write("Enter string: ");
            string enteredRomanValue = Console.ReadLine();

            string[] romanArrayNumbers = enteredRomanValue.Split(new char[] { '+', '-', '*', '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] arabicArrayNumbers = new int[romanArrayNumbers.Length];
            int index = 0;

            foreach (string romanValue in romanArrayNumbers)
            {
                arabicArrayNumbers[index] = RomanToInt(romanValue);
                index++;
            }

            string[] mathOperators = enteredRomanValue.Split(new char[] { 'I', 'V', 'X', 'L', 'C', 'D', 'M' }, StringSplitOptions.RemoveEmptyEntries);
            int result = arabicArrayNumbers[0];

            for (int i = 0, j = 1; i < mathOperators.Length; i++, j++)
            {
                switch (mathOperators[i])
                {
                    case "+": result += arabicArrayNumbers[j]; break;
                    case "-": result -= arabicArrayNumbers[j]; break;
                    case "*": result *= arabicArrayNumbers[j]; break;
                    case "/": result /= arabicArrayNumbers[j]; break;
                }
            }
            Console.WriteLine("\nYour result is " + IntToRoman(result));
        }

        /// <summary>
        /// Converting Roman number to int
        /// </summary>
        /// <param name="romanValue"></param>
        /// <returns></returns>
        public int RomanToInt(string romanValue)
        {
            int arabicNumber = 0;
            Dictionary<char, int> romanNumbersDictionary = new()
            {
                {'I', 1  },
                {'V', 5  },
                {'X', 10  },
                {'L', 50  },
                {'C', 100 },
                {'D', 500 },
                {'M', 1000}
            };
            for (int i = 0; i < romanValue.Length; i++)
            {
                char currentRomanChar = romanValue[i];
                romanNumbersDictionary.TryGetValue(currentRomanChar, out int currentNumber);

                if (i + 1 < romanValue.Length && romanNumbersDictionary[romanValue[i + 1]] > romanNumbersDictionary[currentRomanChar])
                {
                    arabicNumber -= currentNumber;
                }
                else
                {
                    arabicNumber += currentNumber;
                }
            }
            return arabicNumber;
        }

        /// <summary>
        /// Converting int to Roman number
        /// </summary>
        /// <param name="value"></param>
        static string IntToRoman(int arabicNumber)
        {
            string romanResult = string.Empty;
            Dictionary<string, int> romanNumbersDictionary = new()
            {
                {"I", 1  },
                {"IV",4  },
                {"V", 5  },
                {"IX",9  },
                {"X", 10  },
                {"XL",40  },
                {"L", 50  },
                {"XC",90  },
                {"C", 100 },
                {"CD",400 },
                {"D", 500 },
                {"CM",900 },
                {"M", 1000}
            };
            if (arabicNumber == 0)
            {
                romanResult = "0";
            }
            foreach (var item in romanNumbersDictionary.Reverse())
            {
                while (arabicNumber > 0 && arabicNumber >= item.Value)
                {
                    arabicNumber -= item.Value;
                    romanResult += item.Key;
                }
            }
            return romanResult;
        }
    }
    internal class Program
    {
        static void Main()
        {
            RomanCalculator romanCalculator = new RomanCalculator();
            romanCalculator.RomanCalculate();
        }
    }
}