using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemList.Classes
{
    class UserSettings
    {
        public void AddToAutoStart()
        {
            string pathExe = @"C:\_P\Privat\List\Itemllist\ItemList\bin\Debug";

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        }

        public void SelectAttributes()
        {
            
        }
    
    }
}
