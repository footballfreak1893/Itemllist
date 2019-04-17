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
        List<string> listnames = new List<string>();
        List<string> shortNamesOfList = new List<string>();

        public Sublist(Data data)
        {
            
        }

        string listcollectionPath = @"collection.txt";
        //Erweietrungen
        public void SubMenu(Data data, string listname)
        {
            ////Richtige Datei öfnnen

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

            var listnames = ReadingListNames();

            Console.WriteLine("Sub Overview");
            Console.WriteLine("Avaible lists");
            Console.WriteLine();
            DisplayListnames(listnames);
            Console.WriteLine();
            Console.WriteLine("Create new list [n]");
            Console.WriteLine("Open list [enter short of listname]");
            Console.WriteLine();


            var userinput = Console.ReadLine();

            switch (userinput)
            {
                case "n":
                    AddSublist(data, this.listnames);
                    break;

                case "o":
                   
                    break;

                default:
                    OpenList(data, userinput);
                    break;
            }

            ////Notizen für weitere Features
            ////Listnamen in File speichern, inkl. Namensüberschreibung
            ////Passwort datei
            ////-->Neue List: Eigene Methode: 3-4 einstellen
            ////Wenn methode ausgeführt wird soll neue Liste sichtbar sein, mit Bennenung und evtl. passwort schutz+ weitere Details (kategorie..)
            ////Listen sollen gelöscht werden können
            ////Listen sollen germerged werden können
            ////Prüfen, dass Name nicht doppelt vorkommt, wenn user Liste erstellt
        }

        public void WritingListNames(List<string> list)
        {
            using (FileStream fs = new FileStream(listcollectionPath, FileMode.Append, FileAccess.Write))
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

        public string[] ReadingListNames()
        {
            if (!File.Exists(listcollectionPath))
            {
                File.Create(listcollectionPath);
            }
            var inputnames = File.ReadAllLines(listcollectionPath);
            return inputnames;
        }

        public void DisplayListnames(string[] inputnames)
        {
            foreach (var item in inputnames)
            {
                Console.WriteLine(item);
            }
        }

        public void AddSublist(Data data, List<string> listCollection)
        {
            //Für später
            //In Shortliste überprüfen ob kürzel vorhanden, wenn ja dann erste 3 Ziffern verwenden, muss in Open Methode nachgezogen werden
            Console.WriteLine("Enter Name");
            var listname = Console.ReadLine();
            listCollection.Add(listname + "[" + listname[0] + listname[1] + "]");
            WritingListNames(listCollection);
            var inputnames = ReadingListNames();

            foreach (var item in inputnames)
            {
                char x2 = item[item.Length - 2];
                char x3 = item[item.Length - 3];
                char[] XArr = new char[] { x3, x2 };
                 var shortNameInput = new string(XArr);
                shortNamesOfList.Add(shortNameInput);

                CreatePath(data, shortNameInput);
                data.FolderExists();


            }

        }

        public void OpenList(Data data, string shortNameInput)
        {
            Console.WriteLine("Open List");
            SubMenu(data, shortNameInput);
            //var inputnames = ReadingListNames();
            //DisplayListnames(inputnames);

            //foreach (var item in inputnames)
            //{
            //    char x2 = item[item.Length - 2];
            //    char x3 = item[item.Length - 3];
            //    char[] XArr = new char[] { x3, x2 };
            //    shortNameInput = new string(XArr);
            //    shortNamesOfList.Add(shortNameInput);



            //}
            //Console.WriteLine();
            //Console.WriteLine("Input short");
            //var input = shortNameInput;
            ////var input = Console.ReadLine();
            //if (shortNamesOfList.Contains(input))
            //{
            //    //Hier soll das ListMenu angezeigt werden
            //    Console.WriteLine("Continue");
            //    CreatePath(data, input);
            //    data.FolderExists();
            //}
            //else
            //{
            //    Console.WriteLine("Err");
            //}
        }

        public void CreatePath(Data data, string input)
        {
            data.folder = Path.Combine("Data\\Sub", input);
            data.path = data.folder + "_Data.txt"; //Wird noch nicht erstellt
            data.pathId = data.folder + "_IdFile.txt";
            data.currentidStr = "1";
            data.currentid = 1;
            //--> Bug daten werden außerhalb des Ordners erstellt!!
            Console.WriteLine("Data generated");
        }
    }
}

