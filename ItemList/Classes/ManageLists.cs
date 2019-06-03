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
   public  class ManageLists
    {
         public List<string> longNamesList = new List<string>();
         public List<string> shortNamesList = new List<string>();

        UserSettings us = new UserSettings();

        public ManageLists(ManageListItems data)
        {
            FolderWorking.CreateFolder("Data");
            us.LoadUserSettings();
        }
        Password password = new Password();

        public ManageLists()
        {

        }

        public void ListOverview(ManageListItems data)
        {
            if (!File.Exists(data.ListShortNamesPath))
            {
                Console.WriteLine("Checklist");
                Console.WriteLine();
                Console.WriteLine("No lists avaible!");
                var input = AddList(data, longNamesList);
                data.GeneratePathsForeachList(input, true);
                File.WriteAllText(data.pathDefaultlist, "");
            }

            while (true)
            {
                longNamesList = StringWorking.ReadingStringLists(data.ListFullNamesPath);
                shortNamesList = StringWorking.ReadingStringLists(data.ListShortNamesPath);

                Console.WriteLine("--> Checklist <--");
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Sub Overview");
                Console.WriteLine("Avaible lists");
                Console.WriteLine();
                Display.DisplayListnames(longNamesList);
                Console.WriteLine();
                Console.WriteLine("Create new list [n]");
                Console.WriteLine("Open  default list [o]");
                Console.WriteLine("Open list [enter short of listname]");
                Console.WriteLine("Delete sublist [d]");

                Console.WriteLine();
                //Console.WriteLine("Back to main menu [b]");
                Console.WriteLine("Exit programm [e]");
                Console.WriteLine("User settings [u]");
                Console.WriteLine();

                var userinput = Console.ReadLine();
                userinput.ToLower();

                switch (userinput)
                {
                    case "n":
                        Console.Clear();
                        AddList(data, longNamesList);
                        break;

                    case "d":
                        DeleteList(data);
                        break;

                    case "o":
                        Console.Clear();
                        OpenDefaultList(data);
                        return;

                    case "e":
                        Start.Exit(data);
                        return;

                    case "u":
                        Console.Clear();
                        us.SettingsMenu();
                        return;

                    case "":
                        Console.Clear();
                        break;

                    default:
                        if (!shortNamesList.Contains(userinput))
                        {
                            Console.Clear();
                            Console.WriteLine("Error: " + userinput + " does not exists");
                            
                            break;
                        }
                        Console.Clear();
                        OpenList(data, userinput, false);
                        break;
                }
            }
        }

        public void OpenDefaultList(ManageListItems data)
        {
            var dirName = "";
            string[] defaultArr = Directory.GetFiles(data.pathSubFolder, "_default.txt", SearchOption.AllDirectories);
            var oldPath = defaultArr[0];
            var dirInfo = Directory.GetParent(oldPath);
            dirName = dirInfo.Name;

            OpenList(data, dirName, true);
        }

        public void SetAsDefaultList(ManageListItems data, string shortname)
        {
            var newPath = data.folder + "//_default.txt";
            string[] defaultArr = Directory.GetFiles(data.pathSubFolder, "_default.txt", SearchOption.AllDirectories);

            var oldPath = defaultArr[0];
            File.Move(oldPath, newPath);
        }

        public void OpenList(ManageListItems data, string shortNameInput, bool openEntrysDirectly)
        {
            data.GeneratePathsForeachList(shortNameInput, false);
            data.FolderExists();
            Console.WriteLine("Open List");
            Console.Clear();
            ListMenu(data, shortNameInput, openEntrysDirectly);
        }

        public void ListMenu(ManageListItems data, string listname, bool openEntrysDirectly)
        {
            Display display = new Display();
            Filter filter = new Filter();
            
            if (File.Exists(data.folder+ "\\_default.txt"))
            {
                listname = listname + " (Defaultlist)";
            }
            while (true)
            {
                if (openEntrysDirectly == true)
                {
                    Console.Clear();
                    Console.WriteLine("--> Checklist <--");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("--> " + listname + " <--");
                    Console.WriteLine("Entries:");
                    Console.WriteLine();
                    Console.WriteLine();
                    display.ShowEntryDetails(data, data.dict);
                    Console.Clear();
                }

                Console.WriteLine("Sublist Menu");
                Console.WriteLine();
                Console.WriteLine("--> " + listname + " <--");
                password.EnterPassword(data);
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                Console.WriteLine("Exit Programm [e]");
                Console.WriteLine("Show Deatils [x]");
                //Console.WriteLine("Password settings [p]");
                Console.WriteLine("Reset List [r]");
                Console.WriteLine("Filter [f]");
                Console.WriteLine("back to Main[b]");
                Console.WriteLine("Set as default [t]");
                //Console.WriteLine("Finished entries [fi]");

                string userinput = "x";
               userinput = Console.ReadLine();
                
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
                        display.ShowEntryDetails(data, data.dict);
                        break;

                    //case "p":
                    //    Console.Clear();
                    //    password.PasswortSettings(data);
                    //    break;

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

                    case "t":
                        SetAsDefaultList(data, listname);
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

        //Working with Lists
        public string AddList(ManageListItems data, List<string> listCollection)
        {
            shortNamesList = StringWorking.ReadingStringLists(data.ListShortNamesPath);
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

            shortName = GenerateShortname(listname, shortNamesList);

            while (listname == shortName)
            {
                Console.WriteLine("The listname is incorrect, enter a other name");
                listname = Console.ReadLine();
                shortName = GenerateShortname(listname, shortNamesList);
            }
            listname = listname + "[" + shortName + "]";

            longNamesList.Add(listname);
            shortNamesList.Add(shortName);
            StringWorking.SaveStringLists(longNamesList, data.ListFullNamesPath);
            StringWorking.SaveStringLists(shortNamesList, data.ListShortNamesPath);

            Console.WriteLine("Set a password? [y/n]");
            var setPassword = Console.ReadLine();

            data.GeneratePathsForeachList(shortName, true);
            data.FolderExists();
            Console.WriteLine();
            Console.WriteLine("List created");

            switch (setPassword)
            {
                case "y":
                    password.SetPassword(data);
                    break;

                default:
                    StringWorking.SaveString("", data.pathPassword);
                    break;
            }

            Console.Clear();
            return shortName;
        }

        public void DeleteList(ManageListItems data)
        {
            var shortnameList = StringWorking.ReadingStringLists(data.ListShortNamesPath);
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
            StringWorking.SaveStringLists(shortnameList, data.ListShortNamesPath);
        }


        //Methots for listnames
        public void SearchString(ManageListItems data, List<string> list, int index) // Benahmung
        {
            list.RemoveAt(index);
            StringWorking.SaveStringLists(list, data.ListFullNamesPath);
        }

        public string GenerateShortname( string listname, List<string> shortlist) //Nur erlaubte Zeichen für Ordner -->Googeln
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
    }
}

