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
        public static string SetPriority()
        {
            Console.WriteLine("Enter Priority");
            Console.WriteLine("Enter 1: --> high");
            Console.WriteLine("Enter 2: --> medium");
            Console.WriteLine("Enter 3: --> low");
            var inputvalue = Console.ReadLine();
            string priority = "";

            if (inputvalue == "1")
            {
                priority = "high";
            }
            else if (inputvalue == "2")
            {
                priority = "medium";
            }
            else if (inputvalue == "3")
            {
                priority = "low";
            }
            else
            {
                priority = "";
            }
            return priority;
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
            int day = Start.CheckingValuesINT(pday);
           day = CheckingRangeINT(1, 31, day);
            
            Console.WriteLine("Enter Month");
            var pmonth = Console.ReadLine();
            int month = Start.CheckingValuesINT(pmonth);
            month = CheckingRangeINT(1, 12, month);

            Console.WriteLine("Enter Year");
            var pyear = Console.ReadLine();
            int year = Start.CheckingValuesINT(pyear);
            year = CheckingRangeINT(2019, 3000, year);

            var date = new DateTime(year, month, day);
            return date;
        }

        public static int CheckingRangeINT(int x, int y, int inputvalINT)
        {
            while(inputvalINT < x || inputvalINT > y)
            {
                Console.WriteLine("Invalid Range");
                Console.WriteLine("Input a value between " + x + " and " + y);
                string inputvalue = Console.ReadLine();
                inputvalINT = Start.CheckingValuesINT(inputvalue);
            }
            return inputvalINT;
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
