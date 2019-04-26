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
   public  class Sublist
    {
        List<string> longNamesList = new List<string>();
        List<string> shortNamesList = new List<string>();
        Password password = new Password();

        public Sublist(Data data)
        {

        }

        public Sublist()
        {

        }

        //paths
        string ListFullNamesPath = @"fullnames.txt";
        string ListShortNamesPath = @"shortnames.txt";

        public void SubOverview(Data data)
        {
            while (true)
            {
                longNamesList = data.ReadingStringLists(ListFullNamesPath);
                shortNamesList = data.ReadingStringLists(ListShortNamesPath);

                Console.WriteLine("Sub Overview");
                Console.WriteLine("Avaible lists");
                Console.WriteLine();
                data.DisplayListnames(longNamesList);
                Console.WriteLine();
                Console.WriteLine("Create new list [n]");
                Console.WriteLine("Open list [enter short of listname]");
                Console.WriteLine("Delete sublist [d]");
                Console.WriteLine("Back to main menu [b]");
                Console.WriteLine("Exit programm [e]");
                Console.WriteLine("Display Shortlist [d] ");
                Console.WriteLine();

                var userinput = Console.ReadLine();
                userinput.ToLower();

                switch (userinput)
                {
                    case "n":
                        Console.Clear();
                        AddSublist(data, longNamesList);
                        break;

                    case "d":
                        DeleteSublist(data);
                        break;

                    case "b":
                        Console.Clear();
                        return;

                    case "e":
                        Start.Exit(data);
                        return;

                    default:
                        if (!shortNamesList.Contains(userinput))
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
                ////Wenn methode ausgeführt wird soll neue Liste sichtbar sein, mit Bennenung und evtl. passwort schutz+ weitere Details (kategorie..)
                ////Listen sollen germerged werden können
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
                password.EnterPassword(data);
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                Console.WriteLine("Password settings [p]");
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

                    case "p":
                        Console.Clear();
                        password.PasswortSettings(data);
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

        public void OpenList(Data data, string shortNameInput)
        {

            data.CreatePath(shortNameInput);
            data.FolderExists();
            Console.WriteLine("Open List");
            Console.Clear();
            SubMenu(data, shortNameInput);
        }

        public void AddSublist(Data data, List<string> listCollection)
        {
            shortNamesList = data.ReadingStringLists(ListShortNamesPath);
            Console.WriteLine("Create new List");
            Console.WriteLine();
            Console.WriteLine("Enter Name");
            var listname = Console.ReadLine();

            while (listname.Length < 3)
            {
                Console.WriteLine("The listname is too short.");
                Console.WriteLine("Add a listname with at least a length of 3");
                listname = Console.ReadLine();
                Console.Clear();
            }
            //-->Bug liste kann nur geöffnet werden, wenn Großkleinschreibung stimmt

            var shortName = "";

            shortName = GenerateShortname(listname, shortNamesList, longNamesList);
            shortName = shortName.ToLower(); 

            while (listname == shortName)
            {
                Console.WriteLine("The listname is incorrect, enter a other name");
                listname = Console.ReadLine();
                shortName = GenerateShortname(listname, shortNamesList, longNamesList);
            }
            listname = listname + "[" + shortName + "]";

            longNamesList.Add(listname);
            shortNamesList.Add(shortName);
            data.SaveStringLists(longNamesList, ListFullNamesPath);
            data.SaveStringLists(shortNamesList, ListShortNamesPath);

            Console.WriteLine("Set a password? [y/n]");
            var setPassword = Console.ReadLine();

            data.CreatePath(shortName);
            data.FolderExists();
            Console.WriteLine();
            Console.WriteLine("List created");

            switch (setPassword)
            {
                case "y":
                    password.SetPassword(data);
                    break;

                default:
                    data.SaveString("", data.pathPassword);
                    break;
            }
            Console.Clear();
        }

        public void DeleteSublist(Data data)
        {
            //  var fullnameList = ReadingStringLists(ListFullNamesPath);
            var shortnameList = data.ReadingStringLists(ListShortNamesPath);
            Console.WriteLine("Enter short");
            var shortname = Console.ReadLine();

            if (!shortNamesList.Contains(shortname))
            {
                Console.Clear();
                Console.WriteLine("Error: " + shortname + " does not exists");

                return;
            }
            int indexOfname = shortnameList.IndexOf(shortname);

            SearchString(data, longNamesList, indexOfname);
            shortnameList.Remove(shortname);
            data.SaveStringLists(shortnameList, ListShortNamesPath);

            var folder = Path.Combine("Data\\Sub", shortname);
            Directory.Delete(folder, true);
            Console.WriteLine("Sublist has been removed");
        }

        public void SearchString(Data data, List<string> list, int index)
        {
            list.RemoveAt(index);
            data.SaveStringLists(list, ListFullNamesPath);
        }

        public string GenerateShortname( string listname, List<string> shortlist, List<string> longlist) //Nur erlaubte Zeichen für Ordner -->Googeln
        {
            int indexer = 2;
            string listnameShort = listname.Substring(0, indexer);
          
            while (shortlist.Contains(listnameShort) && indexer < listname.Length)
            {
                indexer++;
                listnameShort = listname.Substring(0, indexer);
            }

            return listnameShort;
        }

        //public void DisplayShortlist()
        //{
        //    var list = ReadingStringLists(ListShortNamesPath);
        //    foreach (var shorts in list)
        //    {
        //        Console.WriteLine(shorts);
        //    }
        //}

        //public string GenerateFolder(string input)
        //{

        //    var folder = Path.Combine("Data\\Sub", input);
        //    Directory.CreateDirectory(folder);
        //    Console.WriteLine("folder created");
        //    return folder;
        //}

        

    }
}

