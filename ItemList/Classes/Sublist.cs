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
                Console.WriteLine("Exit programm [e]");
                Console.WriteLine("DisplayShortlist d ");
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

                    case "d":
                        DisplayShortlist();
                        return;

                    case "e":
                        Start.Exit(data);
                        return;

                    default:
                        if (!shortNamesOfList.Contains(userinput))
                        {
                            Console.Clear();
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
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (var item in list)
                {
                    sw.WriteLine(item);
                }
            }
        }

        public List<string> ReadingStringLists(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
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
            shortNamesOfList = ReadingStringLists(ListShortNamesPath);
            Console.WriteLine("Create new List");
            Console.WriteLine();
            Console.WriteLine("Enter Name");
            var listname = Console.ReadLine();

            while (listname.Length < 3)
            {
                Console.WriteLine("The listname is too short.");
                Console.WriteLine("Add a listname with at least a length of 2");
                listname = Console.ReadLine();
                Console.Clear();
            }
            string fulnameString = listname + "[" + listname[0] + listname[1] + "]"; //-->Bug liste kann nur geöffnet werden, wenn Großkleinschreibung stimmt
            string fulnameStringLong = listname + "[" + listname[0] + listname[1] + listname[2] + "]";
            var shortName = "";

            char nameShort1 = fulnameString[fulnameString.Length - 2];
            char nameShort2 = fulnameString[fulnameString.Length - 3];
            char[] nameShortArr = { nameShort2, nameShort1 };
            shortName = new string(nameShortArr);

            Console.WriteLine();
            Console.WriteLine(shortName);
            Console.WriteLine();
            Console.ReadLine();

            if (shortNamesOfList.Contains(shortName)) //Todo: Für mehrere Fälle machen, universell machen --> Rekursive Methode
            {
                char nameShort3 = listname[2];
                char[] shortnameArr = new char[3];
                shortnameArr[0] = nameShortArr[0];
                shortnameArr[1] = nameShortArr[1];
                shortnameArr[2] = nameShort3;
                

                shortName = new string(shortnameArr);
                shortNamesOfList.Add(shortName);
                SaveStringLists(shortNamesOfList, ListShortNamesPath);
                listCollection.Add(fulnameStringLong);
                SaveStringLists(listCollection, ListFullNamesPath);
            }
            else
            {
                char[] shortnameArr = new char[2];
                shortnameArr[0] = nameShortArr[0];
                shortnameArr[1] = nameShortArr[1];

                shortName = new string(shortnameArr);
                shortNamesOfList.Add(shortName);
                SaveStringLists(shortNamesOfList, ListShortNamesPath);
                listCollection.Add(fulnameString);
                SaveStringLists(listCollection, ListFullNamesPath);
            }
            
            data.CreatePath(shortName);
            data.FolderExists();
            Console.WriteLine();
            Console.WriteLine("List created");
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


        public void DisplayShortlist()
        {
            var list = ReadingStringLists(ListShortNamesPath);
            foreach (var shorts in list)
            {
                Console.WriteLine(shorts);
            }
        }

        public string GenerateFolder(string input)
        {

           var folder = Path.Combine("Data\\Sub", input);
            Directory.CreateDirectory(folder);
            Console.WriteLine("folder created");
            return folder;
        }
    }
}

