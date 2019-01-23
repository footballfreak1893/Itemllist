using System;

namespace ItemList
{
    public class Start
    {
        

        static void Main(string[] args)
        {
            StartProgramm();
        }

        public static void StartProgramm()
        {
            Item startitem = new Item();
            while (true)
            {

                Console.WriteLine("Checkliste");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Display entries [s]");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "n":
                        startitem.AddItem();
                        break;

                    case "s":
                        startitem.DisplayAllItems();
                        break;
                }
            }

        }














    }

}
