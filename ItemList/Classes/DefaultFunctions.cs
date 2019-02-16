using ItemList.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList
{
    public class DefaultFunctions
    {
        Data data = new Data();
        // --> Checking
        public static string SetCategory()
        {
            Console.WriteLine("Enter Category");
            Console.WriteLine("Enter 1: --> Business");
            Console.WriteLine("Enter 2: --> Private");
            Console.WriteLine("Enter 3: --> Future plan");
            var inputvalue = Console.ReadLine();
            string category = "";

            if (inputvalue == "1")
            {
                category = "Business";
            }
            else if (inputvalue == "2")
            {
                category = "Private";
            }
            else if (inputvalue == "3")
            {
                category = "Future Plan";
            }
            else
            {
                category = "";
            }
            return category;
        }

        // --> Checking
        public static char SetPriority()
        {
            Console.WriteLine("Enter Priority");
            Console.WriteLine("Enter a: --> high");
            Console.WriteLine("Enter b: --> medium");
            Console.WriteLine("Enter c: --> low");
            var inputvalue = Console.ReadLine();
            char priority ='x';

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
                        break;
                }
                return priority;
            }
            else return 'x';
        }

        public static string NotDefind()
        {
            string notDefind = "Not Defind";
            return notDefind;
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





        //public void displayList()
        //{
        //    foreach (Item i in itemlist)
        //    {
        //        Console.WriteLine("ID: " + i.id);
        //        Console.WriteLine("Titel: " + i.title);
        //        Console.WriteLine("Beschreibung: " + i.description);
        //        Console.WriteLine("Kategorie: " + i.category);
        //        Console.WriteLine("Priorität: " + i.priority);
        //        Console.WriteLine();
        //        Console.ReadKey();
        //    }
        //}

    }
}
