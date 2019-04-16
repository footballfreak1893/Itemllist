using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
 

namespace ItemList.Classes
{
    class Sublist
    {
       // Data data = new Data();

        public Sublist(Data data)
        {
            
        }

        string listcollectionPath = @"ListCollection\collection.txt";
        //Erweietrungen
        public void SubMenu(Data data, string listname)
        {
            string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();
            //Kann evtl. weg
            //Data data = new Data();
            //data.CheckListtype("s");
            //data.FolderExists();
            

            while (true)
            {
                Console.WriteLine("Sublist Menu:");
                Console.WriteLine();
                Console.WriteLine(listname);
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Filter [f]");
                Console.WriteLine("back to Main[b]");
                Console.WriteLine("[st]");
                //Console.WriteLine("Finished entries [fi]");

                string userinput = Console.ReadLine();

                switch (userinput.ToLower())
                {
                    case "n":
                        Console.Clear();
                        data.AddItem();
                        break;

                    case "e":
                        Console.Clear();
                        Start.Exit(data);
                        break;

                    case "x":
                        Console.Clear();
                        display.ShowDetails(data, false, data.dict);
                        break;

                    case "r":
                        Console.Clear();
                        data.ClearList();
                        break;

                    case "f":
                        Console.Clear();
                        filter.FilterMenu(data);
                        break;

                    case "b":
                        Console.Clear();
                        return;

                    case "rn":
                        Console.Clear();
                        DefaultFunctions.RenameList(listname);
                        return;

                    case "st":
                        Console.Clear();
                        return;

                    //Speicherung von Listnamen

                    //case "fi":
                    //    Console.Clear();
                    //    display.EntriesSetFinished(data);
                    //    break;

                    default:
                        Console.Clear();
                        return;
                }

            }
        }

        public void SubOverview(Data data)
        {
            Console.WriteLine("Avaible lists");
            ReadingListNames();
            Console.WriteLine();
            Console.WriteLine("Coose a list");
            Console.WriteLine("New sublist");
            Console.WriteLine("Enter Name");
           

          
            List<string> listCollection = new List<string>();
            //Notizen für weitere Features
            //Listnamen in File speichern, inkl. Namensüberschreibung
            //Passwort datei
            //-->Neue List: Eigene Methode: 3-4 einstellen
            //Wenn methode ausgeführt wird soll neue Liste sichtbar sein, mit Bennenung und evtl. passwort schutz+ weitere Details (kategorie..)
            //Listen sollen gelöscht werden können
            //Listen sollen germerged werden können


            
            //listCollection.Add("Sub1");
            //listCollection.Add("Sub2");
            //listCollection.Add("Sub3");
            
            AddSublist(listCollection);

            // File.WriteAllLines(listcollectionPath, listCollection);
            foreach (var item in listCollection)
            {
                Console.WriteLine(item + "  [ "+item[0]+item[1]+"]");
            }
            var userinput = Console.ReadLine();

            switch (userinput)
            {
                case "s1":
                    data.CheckListtype("s1");
                    data.FolderExists();
                    SubMenu(data, "Test" );
                    break;

                case "s2":
                    data.CheckListtype("s2");
                    data.FolderExists();
                    SubMenu(data, "Sub2");
                    break;

                case "s3":
                    data.CheckListtype("s3");
                    data.FolderExists();
                    SubMenu(data, "Sub3");
                    break;

                default:
                    break;
            }

            //data.CheckListtype(userinput);
            //data.FolderExists();

            
        }

        public void WritingListNames(List<string> list)
        {
            using (FileStream fs = new FileStream("text.txt", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (var item in list)
                {
                    sw.WriteLine(item);
                }
                //File Append Methode einbauen
                //ACHTUNG FEHLER
            }
        }

        public void ReadingListNames()
        {
            string line = "";
            using (StreamReader sr = new StreamReader("test.txt"))
            {
                while((line = sr.ReadLine())!=null)
                {
                    Console.WriteLine(line);
                }
                //Append Methode beachten
            }
        }

        public void AddSublist(List<string>listCollection)
        {
            Console.WriteLine("Enter Name");
            var listname = Console.ReadLine();
            listCollection.Add(listname +"["+listname[0]+listname[1]+"]");
            WritingListNames(listCollection);

        }
    }
}

