using ItemList.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ItemList
{
    public class DefaultFunctions
    {
        Data data = new Data();
        
        public static void CreateFolder(string foldername)
        {
            DirectoryInfo di = new DirectoryInfo(foldername);
            di.Create();
            Console.WriteLine("Folder created");
        }

        public static void SaveString(string text, string path)
        {
            File.WriteAllText(path, text);
        }

        public static string ReadString(string path)
        {
            var text = File.ReadAllText(path);
            return text;
        }

        public static void SaveStringLists(List<string> list, string path)
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

        public static List<string> ReadingStringLists(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }
            var stringArr = File.ReadAllLines(path);
            var stringList = stringArr.ToList();
            return stringList;
        }

        public static void DisplayListnames(List<string> inputnames)
        {
            foreach (var item in inputnames)
            {
                Console.WriteLine(item);
            }
        }
    }
}
