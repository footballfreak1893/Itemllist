﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList
{
    public static class DefaultFunctions
    {
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
            return category;
        }

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
            return priority;
        }

        public static string NotDefind()
        {
            string notDefind = "Not Defind";
            return notDefind;
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