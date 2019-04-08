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

        public void FolderExists(string foldername, string filePath, string filePathId)
        {
            if (Directory.Exists(foldername))
            {
                FileExists(filePath, filePathId);
            }
            else
            {
                DefaultFunctions.CreateFolder(foldername);
                FileExists(pathDefault, filePathId);
            }
        }

        public void FileExists(string filePath, string filePathId)
        {
            if (File.Exists(pathDefault))
            {
                dict = LoadList(filePath, dict);
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

        public void AddItem()
        {
            currentidDefault = ReadId(pathIdDefault);
            currentidDefault = CountId(currentidDefault);
            SaveId(pathIdDefault, currentidDefault);

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

            Item item = new Item(userTitle, currentidDefault);

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
                currentidDefault--;
                SaveId(pathIdDefault, currentidDefault);
                Console.Clear();
                return;
            }
            else
            {
                Console.Clear();
                dict.Add(item.id, item);
                SaveList(pathDefault, dict);
            }
        }

        public void DeleteItem(int id, Item item)
        {
            dict.Remove(id);
            SaveList(pathDefault, dict);
        }

        public void UpdateItem(int index, Item item)
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

            SaveList(pathDefault, dict);
            //Console.WriteLine("entry: " + item.id + ".)");
        }

        public int CountItems()
        {
            var itemscount = dict.Count();
            Console.WriteLine("Number of items " + itemscount);
            return itemscount;
        }
        
        public void ClearList()
        {
            dict.Clear();
            currentidDefault = 0;
            SaveId(pathIdDefault, currentidDefault);
            SaveList(pathDefault, dict);
            Console.WriteLine("List has been reseted");
        }
    }
}
