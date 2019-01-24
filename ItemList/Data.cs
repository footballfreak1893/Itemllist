using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList
{
    public class Data
    {
        List<Item> itemlist = new List<Item>();
        Dictionary<int, Item> dict = new Dictionary<int, Item>();

        public Data(List<Item> itemlist)
        {

        }
    }
}
