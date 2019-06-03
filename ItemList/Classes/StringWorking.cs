using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ItemList.Classes
{
    class StringWorking
    {
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
    }
}
