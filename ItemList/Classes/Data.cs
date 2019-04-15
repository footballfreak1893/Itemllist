using ItemList.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ItemList
{
    public class Data
    {
        // General class variables
        public string folderDefault = @"Data";
        public string pathDefault = @"Data\list.txt";
        public string pathIdDefault = @"Data\idfile.txt";
        public string StartIdDefault = "1";
        public int currentidDefault = 1;

        public string folderSub = @"Data\Sub";
        public string pathSub = @"Data\Sub\list.txt";
        public string pathIdSub = @"Data\Sub\idfile.txt";
        public string StartIdSub = "1";
        public int currentidDefaultSub = 1;

        CheckingNumbers checkingNumbers = new CheckingNumbers();
       
        public Data()
        {

        }
        public Dictionary<int, Item> dict = new Dictionary<int, Item>();
        public Dictionary<int, Item> sub = new Dictionary<int, Item>();



        //!!!! Vorerst: Eiegne File Exits Methode für Sub
        public void FolderExists(string foldername, string filePath, string filePathId, string listtype)
        {

            if (Directory.Exists(foldername))
            {
                //FileExists(filePath, filePathId, listtype);

                FileExistsDefault();
            }
            else
            {
                DefaultFunctions.CreateFolder(foldername);
                //FileExists(pathDefault, filePathId, listtype);
                FileExistsDefault();
            }
        }

        //!!!! Vorerst: Eiegne File Exits Methode für Sub
        public void FileExists(string filePath, string filePathId, string listtype)
        {
            if (File.Exists(filePath))
            {
                if (listtype == "sub")
                {
                    sub = LoadList(filePath, sub);
                    SaveId(pathIdSub, currentidDefaultSub);
                }
                else
                {
                    dict = LoadList(filePath, dict);
                }
               //Überprüfen, das Sub ID File erstellt wird, siehe Branch v2
               
            }

            else
            {
                currentidDefault = 0;
                SaveId(filePathId, currentidDefault);
            }

            if (File.Exists(pathIdDefault))
            {
                currentidDefault = ReadId(pathIdDefault);
            }

            else
            {
                currentidDefault = 0;
                SaveId(pathIdDefault, currentidDefault);
                ClearList();
            }

        }

        public void FileExistsDefault()
        {
            //Zuerst überprüfen, ob ID existiert
            if (File.Exists(pathIdDefault))
            {
                  currentidDefault = ReadId(pathIdDefault);
                if (File.Exists(pathDefault))
                {
                   dict = LoadList(pathDefault, dict);
                }
                else
                {
                    Console.WriteLine("List File can't be found");
                }
            }
            else
            {
                Console.WriteLine("ID File can't be found");
                currentidDefault = 0;
                SaveId(pathIdDefault, currentidDefault);
                Console.WriteLine("ID File was created");

            }

        }

        public void IDFileExists(string pathId)
        {
            if (!File.Exists(pathId))
            {
                //ID File erstellen
            }
        }

        public void SaveList(string path, Dictionary <int, Item> list)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            IFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, list);

            fs.Close();
        }

        public Dictionary<int, Item> LoadList(string path, Dictionary<int, Item> list)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            IFormatter bf = new BinaryFormatter();
            list = (Dictionary<int, Item>)bf.Deserialize(fs);
            fs.Close();

            return list;
        }

        public int ReadId(string fileIdPath)
        {
            //Anwendung Sub
            StartIdDefault = File.ReadAllText(fileIdPath);
            int currentid = Convert.ToInt32(StartIdDefault);
            return currentid;
        }

        public void SaveId(string fileIdPath, int currentid)
        {
            StartIdDefault = Convert.ToString(currentid);
            File.WriteAllText(fileIdPath, StartIdDefault);
        }

        public int CountId(int idToCount)
        {
            idToCount = idToCount + 1;
            return idToCount;
        }

        public int proccessingID(int currentId, string fileIdPath)
        {
            //Liest aktuelle ID aus und erhöht um 1
            currentId = ReadId(fileIdPath);
            currentId = CountId(currentId);
            SaveId(fileIdPath, currentId);
            return currentId;
        }

        public void DowngradeID(int currentId, string fileIdPath)
        {
            currentId--;
            SaveId(fileIdPath, currentId);
            Console.Clear();
            return;
        }
        public void AddItem(Dictionary<int, Item> list, int currentId, string fileIdPath, string filePath)
        {
            //Aktuell Sub
            currentId = proccessingID(currentidDefaultSub, pathIdDefault);
            //currentidDefault = ReadId(pathDefault);

            Console.WriteLine("Add Entry");
            Console.WriteLine("Enter Title:");
            string userTitle = Console.ReadLine();
            while (userTitle == "")
            {
                Console.WriteLine("Title must have a value");
                userTitle = Console.ReadLine();

            }
            Console.WriteLine();
            Console.WriteLine("Enter Description:");
            string userDescription = Console.ReadLine();

            Console.WriteLine();

            Item item = new Item(userTitle, currentId);

            char priority = DefaultFunctions.SetPriority();
            if (priority != 'x')
            {
                item.priority = priority;
            }

            Console.WriteLine();
            Console.WriteLine(" Want to enter a Enddate? [y/n]");
            string userEndddate = Console.ReadLine();

            if (userEndddate == "y")
            {
                var userEnddate = DefaultFunctions.SetDateValue();
                item.enddate = userEnddate;
            }

            Console.WriteLine();
            item.description = userDescription;

            item.createdate = DateTime.Now;

            Console.WriteLine("Save this entry [y/n]");
            string inputuser = Console.ReadLine();

            if (inputuser == "n")
            {
                DowngradeID(currentId, fileIdPath);
                Console.Clear();
                return;
            }
            else
            {
                Console.Clear();
                list.Add(item.id, item);
                SaveList(filePath, list);
            }
        }

        public void DeleteItem(int id, Item item, Dictionary <int,Item> list)
        {
            list.Remove(id);
            SaveList(pathDefault, list);
        }

        public void UpdateItem(int index, Item item, Dictionary<int, Item> list)
        {
            Console.WriteLine("Update entry: ");
            Console.WriteLine();
            Console.WriteLine("Update Title");
            var inputtitle = Console.ReadLine();
            if (inputtitle != "")
            {
                item.title = inputtitle;
            }

            Console.WriteLine("Update Description");
            var inputdescription = Console.ReadLine();
            if (inputdescription != "")
            {
                item.description = inputdescription;
            }

            Console.WriteLine("Update Priority");
            char priority = DefaultFunctions.SetPriority();
            if (priority != 'x')
            {
                item.priority = priority;
            }

            Console.WriteLine();

            SaveList(pathDefault, list);
            //Console.WriteLine("entry: " + item.id + ".)");
        }

        public int CountItems(Dictionary<int, Item> list)
        {
            var itemscount = list.Count();
            Console.WriteLine("Number of items " + itemscount);
            return itemscount;
        }
        
        //Nicht universell
        public void ClearList()
        {
            dict.Clear();
            currentidDefault = 0;
            SaveId(pathIdDefault, currentidDefault);
            SaveList(pathDefault, dict);
            Console.WriteLine("List has been reseted");
        }

        //Testing

        public void CountItems()
        {
            Console.WriteLine("DICT " + dict.Count);
            Console.WriteLine("SUB " + sub.Count);
        }
    }
}
