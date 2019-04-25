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
        List<string> longNamesOfList = new List<string>();
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
                longNamesOfList = ReadingStringLists(ListFullNamesPath);
                shortNamesOfList = ReadingStringLists(ListShortNamesPath);

                Console.WriteLine("Sub Overview");
                Console.WriteLine("Avaible lists");
                Console.WriteLine();
                DisplayListnames(longNamesOfList);
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
                        AddSublist(data, longNamesOfList);
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
                EnterPassword(data);
                Console.WriteLine();
                Console.WriteLine("New Entry [n]");
                //Console.WriteLine("Delete sublist [d]");
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
                        PasswortSettings(data);
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

        //public void AddSublist(Data data, List<string> listCollection)
        //{
        //    shortNamesOfList = ReadingStringLists(ListShortNamesPath);
        //    Console.WriteLine("Create new List");
        //    Console.WriteLine();
        //    Console.WriteLine("Enter Name");
        //    var listname = Console.ReadLine();

        //    while (listname.Length < 5)
        //    {
        //        Console.WriteLine("The listname is too short.");
        //        Console.WriteLine("Add a listname with at least a length of 3");
        //        listname = Console.ReadLine();
        //        Console.Clear();
        //    }
        //    string fulnameString = listname + "[" + listname[0] + listname[1] + "]"; //-->Bug liste kann nur geöffnet werden, wenn Großkleinschreibung stimmt
        //    string fulnameStringLong = listname + "[" + listname[0] + listname[1] + listname[2] + "]";
        //    var shortName = "";

        //    char nameShort1 = fulnameString[fulnameString.Length - 2];
        //    char nameShort2 = fulnameString[fulnameString.Length - 3];
        //    char[] nameShortArr = { nameShort2, nameShort1 };
        //    shortName = new string(nameShortArr);

        //    Console.WriteLine();
        //    Console.WriteLine(shortName);
        //    Console.WriteLine();
        //    Console.ReadLine();

        //    if (shortNamesOfList.Contains(shortName)) //Todo: Für mehrere Fälle machen, universell machen --> Rekursive Methode
        //    {
        //        char nameShort3 = listname[2];
        //        char[] shortnameArr = new char[3];
        //        shortnameArr[0] = nameShortArr[0];
        //        shortnameArr[1] = nameShortArr[1];
        //        shortnameArr[2] = nameShort3;


        //        shortName = new string(shortnameArr);
        //        shortNamesOfList.Add(shortName);
        //        SaveStringLists(shortNamesOfList, ListShortNamesPath);
        //        listCollection.Add(fulnameStringLong);
        //        SaveStringLists(listCollection, ListFullNamesPath);
        //    }
        //    else
        //    {
        //        char[] shortnameArr = new char[2];
        //        shortnameArr[0] = nameShortArr[0];
        //        shortnameArr[1] = nameShortArr[1];

        //        shortName = new string(shortnameArr);
        //        shortNamesOfList.Add(shortName);
        //        SaveStringLists(shortNamesOfList, ListShortNamesPath);
        //        listCollection.Add(fulnameString);
        //        SaveStringLists(listCollection, ListFullNamesPath);
        //    }
        //    Console.WriteLine("Set a password? [y/n]");
        //    var setPassword = Console.ReadLine();

           

        //    data.CreatePath(shortName);
        //    data.FolderExists();
        //    Console.WriteLine();
        //    Console.WriteLine("List created");
        //    //Hier weiter --> Passwort wird generiert
        //    switch (setPassword)
        //    {
        //        case "y":
        //            SetPassword(data);
        //            break;

        //        default:
        //            SaveString("", data.pathPassword);
        //            break;
        //    }
        //    Console.Clear();

        //}

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
                Console.WriteLine("Add a listname with at least a length of 3");
                listname = Console.ReadLine();
                Console.Clear();
            }
             //-->Bug liste kann nur geöffnet werden, wenn Großkleinschreibung stimmt
            
            var shortName = "";



            shortName = GenerateShortname(listname, shortNamesOfList, longNamesOfList);

            listname = listname + "[" +shortName + "]";

            longNamesOfList.Add(listname);
            shortNamesOfList.Add(shortName);
            SaveStringLists(longNamesOfList, ListFullNamesPath);
            SaveStringLists(shortNamesOfList, ListShortNamesPath);

            Console.WriteLine("Set a password? [y/n]");
            var setPassword = Console.ReadLine();



            data.CreatePath(shortName);
            data.FolderExists();
            Console.WriteLine();
            Console.WriteLine("List created");
            //Hier weiter --> Passwort wird generiert
            switch (setPassword)
            {
                case "y":
                    SetPassword(data);
                    break;

                default:
                    SaveString("", data.pathPassword);
                    break;
            }
            Console.Clear();

        }

        public string GenerateShortname( string listname, List<string> shortlist, List<string> longlist) //Nur kürzel für Ordner -->Googeln
        {
            int indexer = 2;
            string listnameShort = listname.Substring(0, indexer);
          
            while (shortlist.Contains(listnameShort) && indexer < listname.Length)
            {
                indexer++;
                listnameShort = listname.Substring(0, indexer);
                //char additionalCharOfLongname = listname[1 + indexer];
                //string removeLastCharOflongname = listname.Remove(listname.Length - 1);

                //listname = removeLastCharOflongname + additionalCharOfLongname + "]";
                //listnameShort = listnameShort + listname[indexer + 1];
                //Console.WriteLine("Index: "+indexer);
                //Console.WriteLine("Shortname"+listnameShort.Length);
                //Console.ReadLine();

                //indexer++;
                //if(indexer == listname.Length-2 )
                //{
                //    listnameShort = listnameShort + listname[listname.Length - 1];
                //    Console.WriteLine("ERREICHT");
                //}
            }
            

            return listnameShort;
        }


        public void DeleteSublist(Data data)
        {
          //  var fullnameList = ReadingStringLists(ListFullNamesPath);
            var shortnameList = ReadingStringLists(ListShortNamesPath);
            Console.WriteLine("Enter short");
               var shortname = Console.ReadLine();

            if (!shortNamesOfList.Contains(shortname))
            {
                Console.Clear();
                Console.WriteLine("Error: " + shortname + " does not exists");

                return;
            }
            int indexOfname = shortnameList.IndexOf(shortname);

            SearchString(data, longNamesOfList, indexOfname);
            shortnameList.Remove(shortname);
            SaveStringLists(shortnameList, ListShortNamesPath);

            var folder = Path.Combine("Data\\Sub", shortname);
            Directory.Delete(folder, true);
            Console.WriteLine("Sublist has been removed");
        }

        public void SearchString(Data data, List<string> list, int index) 
        {
            list.RemoveAt(index);
            SaveStringLists(list, ListFullNamesPath);
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



        //Passwort

        //--> Hier könnte man noch Passwortrichtlinien erstellen
        //Bug: wenn passwort eingegen wird, das passwort kopiert ist, wird es sichtbar!!!!!
        
        public void SetPassword(Data data)
        {
            Console.WriteLine("Enter password");
            Console.ForegroundColor = ConsoleColor.Black;
            var password = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Password set");

            password = AES.CryptMenu(password, 'e');
            SaveString(password, data.pathPassword);

           //SaveString(password, data.pathPassword);
        }

        public void EnterPassword(Data data)
        {
            var password = ReadString(data.pathPassword);
            password = AES.CryptMenu(password, 'd');

            if (password == null || password == "")
            {
                return;
            }

            Console.WriteLine("This list requires a password, please enter it");
            Console.ForegroundColor = ConsoleColor.Black;
            var inputPassword = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            while (password != inputPassword)
            {
                Console.WriteLine("Password is incorrect, try again");
                Console.ForegroundColor = ConsoleColor.Black;
                inputPassword = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

       
        public void Change_Or_SetNull_Password(Data data, char deleteOrChange)
        {
            if (deleteOrChange == 'd')
            {
                var password = "";
                SaveString(password, data.pathPassword);
            }

            else
            {
                Console.WriteLine("Change password");
                Console.WriteLine("enter current password");
                Console.ForegroundColor = ConsoleColor.Black;
                var inputPasswordCurrent = Console.ReadLine();
                var password = ReadString(data.pathPassword);
                password = AES.CryptMenu(password, 'd');
                Console.ForegroundColor = ConsoleColor.White;

                while (password != inputPasswordCurrent)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Current password is incorrect, try again");
                    Console.ForegroundColor = ConsoleColor.Black;
                    inputPasswordCurrent = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("enter new password");
                Console.ForegroundColor = ConsoleColor.Black;
                password = Console.ReadLine();
                password = AES.CryptMenu(password, 'e');
                SaveString(password, data.pathPassword);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Password has successfully changed");
            }
        }

        public void PasswortSettings(Data data)
        {
            //--> BUG: Wenn Feature Farbe ändern eingebaut wird muss andere Lösung her als vordergrundfarbe zu ändern (Idee -->..)
            Console.WriteLine("Password settings");
            Console.WriteLine();

             var password = ReadString(data.pathPassword);
             password = AES.CryptMenu(password, 'd');

            if (password == null || password == "")
            {
                Console.WriteLine("add password [a]");
            }

            Console.WriteLine("Change password [c]");
            Console.WriteLine("Deactivate password [d]");
            var userinput = Console.ReadLine();
           userinput = userinput.ToLower();

            switch (userinput)
            {
                case "a":
                    SetPassword(data);
                    break;

                case "c":
                    Change_Or_SetNull_Password(data, 'c');
                    break;

                case "d":
                    Change_Or_SetNull_Password(data, 'd');
                    break;

                case "p":
                   
                    break;

                default:
                    return;
            }
            Console.Clear();
        }

        public void SaveString(string text, string path)
        {
            File.WriteAllText(path, text);
        }

        public string ReadString(string path)
        {
           var text = File.ReadAllText(path);
            return text;
        }
    }
}

