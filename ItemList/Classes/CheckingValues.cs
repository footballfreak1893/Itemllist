using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList.Classes
{
    public class CheckingValues
    {
        public static int CheckingRangeINT(int rangeStart, int rangeEnd, int inputvalINT)
        {
            while (inputvalINT < rangeStart || inputvalINT > rangeEnd)
            {
                Console.WriteLine("Invalid Range");
                Console.WriteLine("Input a value between " + rangeStart + " and " + rangeEnd);
                string inputvalue = Console.ReadLine();
                inputvalINT = CheckingValuesINT(inputvalue);
            }
            return inputvalINT;
        }

        public static int CheckingValuesINT(string input)
        {
            bool canconvert = false;
            Int16 integer = 0;

            while (canconvert == false)
            {
                canconvert = Int16.TryParse(input, out integer);
                if (canconvert == false)
                {
                    Console.WriteLine("Incorrect data type, enter a whole number");
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            return integer;
        }

        public static double CheckingValuesDouble(string input)
        {
            bool canconvert = false;
            double doublevalue = 0;

            while (canconvert == false)
            {
                canconvert = double.TryParse(input, out doublevalue);
                if (canconvert == false)
                {
                    Console.WriteLine("Incorrect data type, enter a numeric value");
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            return doublevalue;
        }

        public static char CheckingValuesChar( string input)
        {
            bool canconvert = false;
            char charvalue = '0';

            while (canconvert == false)
            {
                canconvert = char.TryParse(input, out charvalue);
                if (canconvert == false)
                {
                    Console.WriteLine("Incorrect data type, enter a char value [example a or 1]");
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            return charvalue;
        }
    }
}
