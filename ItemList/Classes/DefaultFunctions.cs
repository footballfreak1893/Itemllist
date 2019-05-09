using ItemList.Classes;
using System;
using System.IO;

namespace ItemList
{
    public class DefaultFunctions
    {
        Data data = new Data();
        public static char SetPriority()
        {
            Console.WriteLine("Enter Priority");
            Console.WriteLine("Enter a: --> high");
            Console.WriteLine("Enter b: --> medium");
            Console.WriteLine("Enter c: --> low");
            var inputvalue = Console.ReadLine();
            char priority = 'x';

            if (inputvalue != "")
            {
                priority = CheckingNumbers.CheckingValuesChar(inputvalue);

                switch (priority)
                {
                    case 'a':
                        priority = 'a';
                        break;

                    case 'b':
                        priority = 'b';
                        break;

                    case 'c':
                        priority = 'c';
                        break;

                    default:
                        return 'x';
                }
                return priority;
            }
            else return 'x';
        }

        public static DateTime SetDateValue()
        { 
            Console.WriteLine("Enter Day");
            var inputDay = Console.ReadLine();
            int day = CheckingNumbers.CheckingValuesINT(inputDay);
            day = CheckingNumbers.CheckingRangeINT(1, 31, day);

            Console.WriteLine("Enter Month");
            var inputMonth = Console.ReadLine();
            int month = CheckingNumbers.CheckingValuesINT(inputMonth);
            month = CheckingNumbers.CheckingRangeINT(1, 12, month);

            Console.WriteLine("Enter Year");
            var inputYear = Console.ReadLine();
            int year = CheckingNumbers.CheckingValuesINT(inputYear);
            year = CheckingNumbers.CheckingRangeINT(2019, 3000, year);

            var date = new DateTime(year, month, day);
            return date;
        }

        
        public static void CreateFolder(string foldername)
        {
            DirectoryInfo di = new DirectoryInfo(foldername);
            di.Create();
            Console.WriteLine("Folder created");
        }
    }
}
