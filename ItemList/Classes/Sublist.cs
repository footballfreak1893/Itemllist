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

        //paths
        string ListFullNamesPath = @"fullnames.txt";
        string ListShortNamesPath = @"shortnames.txt";

        public void SubOverview(Data data)
        {
            while (true)
            {
                listnames = ReadingStringLists(ListFullNamesPath);
                shortNamesOfList = ReadingStringLists(ListShortNamesPath);

                Console.WriteLine("Sub Overview");
                Console.WriteLine("Avaible lists");
                Console.WriteLine();
                DisplayListnames(listnames);
                Console.WriteLine();
                Console.WriteLine("Create new list [n]");
                Console.WriteLine("Open list [enter short of listname]");
                Console.WriteLine("Back to main menu [b]");
                Console.WriteLine();

                var userinput = Console.ReadLine();
                userinput.ToLower();

                switch (userinput)
                {
                    case "n":
                        Console.Clear();
                        AddSublist(data, listnames);
                        break;

                    case "b":
                        Console.Clear();
                        return;

                    default:
                        if (!shortNamesOfList.Contains(userinput))
                        {
                            Console.WriteLine("Error: " + userinput + " does not exists");
                            break;
                        }
                        Console.Clear();
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
        }

        //Erweietrungen
        public void SubMenu(Data data, string listname)
        {
            ////Richtige Datei öfnnen

            string version = "v 2.0";
            Display display = new Display();
            Filter filter = new Filter();

            while (true)
            {
                Console.WriteLine("Sublist Menu");
                Console.WriteLine();
                Console.WriteLine("--> " + listname + " <--");
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Filter [f]");
                Console.WriteLine("back to Main[b]");
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

        public void SaveStringLists(List<string> list, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
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

        public List<string> ReadingStringLists(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "test");
            }
            var stringArr = File.ReadAllLines(path);
            var stringList = stringArr.ToList();
            return stringList;
        }

        public void DisplayListnames(List<string> inputnames)
        {
            foreach (var item in inputnames)
            {
                Console.WriteLine(item);
            }
        }

        public void AddSublist(Data data, List<string> listCollection)
        {
            ////ACHTUNG: Wenn List angelegt wird soll der Path der Datenen hinterlegt werden !!!!! -----> Soll bei Open abgefragt werden
            //Für später
            //In Shortliste überprüfen ob kürzel vorhanden, wenn ja dann erste 3 Ziffern verwenden, muss in Open Methode nachgezogen werden
            Console.WriteLine("Create new List");
            Console.WriteLine();
            Console.WriteLine("Enter Name");
            var listname = Console.ReadLine();

            while (listname.Length < 2)
            {
                Console.WriteLine("The listname is too short.");
                Console.WriteLine("Add a listname with at least a length of 2");
                listname = Console.ReadLine();
                Console.Clear();
            }
            string x  = listname + "[" + listname[0] + listname[1] + "]";
            listCollection.Add(x);
            //ACHTung: Daten haben redundante Daten
            SaveStringLists(listCollection, ListFullNamesPath);
            var inputnames = ReadingStringLists(ListFullNamesPath);
            var shortNameInput = "";
            //foreach (var item in inputnames)
            //{
                char nameShort1 = x[x.Length - 2];
                char nameShort2 = x[x.Length - 3];
                char nameShort3 = x[x.Length - 4];
            char[] nameShortArr = { nameShort3, nameShort2, nameShort1 };
                shortNameInput = new string(nameShortArr);

                if (shortNamesOfList.Contains(shortNameInput)) //Todo: Für mehrere Fälle machen, universell machen
                {
                    char[] test = new char[3];
                    test[0] = nameShortArr[0];
                    test[1] = nameShortArr[1];
                    test[2] = nameShortArr[2];


                    shortNameInput = new string(test);
                    shortNamesOfList.Add(shortNameInput);
                }
                else
                {
                    char[] test = new char[2];
                    test[0] = nameShortArr[0];
                    test[1] = nameShortArr[1];


                    shortNameInput = new string(test);
                    shortNamesOfList.Add(shortNameInput);
                }

            //}
            data.CreatePath(shortNameInput);
            SaveStringLists(shortNamesOfList, ListShortNamesPath);
            data.FolderExists();
            Console.WriteLine();
            Console.WriteLine("List greated");
            Console.Clear();

        }

        public void OpenList(Data data, string shortNameInput)
        {
            data.CreatePath(shortNameInput);
            data.FolderExists();
            Console.WriteLine("Open List");
            Console.Clear();
            SubMenu(data, shortNameInput);
        }

        //public void CreatePath(Data data, string input)
        //{
        //    data.folder = Path.Combine("Data\\Sub", input);
        //    data.path = data.folder + "_Data.txt"; //Wird noch nicht erstellt
        //    data.pathId = data.folder + "_IdFile.txt";
        //    data.currentidStr = "1";
        //    data.currentid = 1;
        //    //--> Bug daten werden außerhalb des Ordners erstellt!!
        //    Console.WriteLine("Data generated");
        //}

        public void ReadPath()
        {

        }
    }
}

