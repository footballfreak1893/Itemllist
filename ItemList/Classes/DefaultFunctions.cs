using ItemList.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList
{
    public class DefaultFunctions
    {
        Data data = new Data();

        // --> Checking
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
        { //Checking here
            Console.WriteLine("Enter Day");
            var pday = Console.ReadLine();
            int day = CheckingNumbers.CheckingValuesINT(pday);
            day = CheckingNumbers.CheckingRangeINT(1, 31, day);

            Console.WriteLine("Enter Month");
            var pmonth = Console.ReadLine();
            int month = CheckingNumbers.CheckingValuesINT(pmonth);
            month = CheckingNumbers.CheckingRangeINT(1, 12, month);

            Console.WriteLine("Enter Year");
            var pyear = Console.ReadLine();
            int year = CheckingNumbers.CheckingValuesINT(pyear);
            year = CheckingNumbers.CheckingRangeINT(2019, 3000, year);

            var date = new DateTime(year, month, day);
            return date;
        }

        public static string NotDefind()
        {
            string notDefind = "Not Defind";
            return notDefind;
        }

        public static string RenameList(string listname)
        {
            Console.WriteLine("Enter new Name");
            var newName = Console.ReadLine();
            listname = newName;
            Console.WriteLine("New Name " +newName);
            return newName;
        }

        //Not used
        public static void CreateFolder(string foldername)
        {
            DirectoryInfo di = new DirectoryInfo(foldername);
            di.Create();
            Console.WriteLine("Folder created");
        }
    }
}
