using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList
{
    class Start
    {
        static void Main(string[] args)
        {
            Item item = new Item();
            item.FileExists();
            item.Start();
        }
    }
}
