using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Paths
{
    public class FolderItem : Item
    {
        public List<Item> Children { get; set; }

        public FolderItem()
        {
            this.Children = new List<Item>();
        }
    }
}
