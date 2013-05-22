using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Paths
{
    public class PathTreeLoader
    {
        private Dictionary<string, FolderItem> items;
        private Dictionary<string, FolderItem> roots;

        public IEnumerable<FolderItem> Load(IEnumerable<string> paths)
        {
            this.roots = new Dictionary<string, FolderItem>();

            this.items = new Dictionary<string, FolderItem>();

            foreach (var item in paths)
            {
                this.FindNodeFor(item);
            }

            return roots.Values;
        }

        private FolderItem FindNodeFor(string path)
        {
            if (this.items.ContainsKey(path))
            {
                return this.items[path];
            }

            var node = new FolderItem { Name = GetName(path) };

            this.items[path] = node;

            if (path.Contains('/'))
            {
                var basePath = GetBasePath(path);

                var baseNode = FindNodeFor(basePath);

                baseNode.Children.Add(node);
            }
            else
            {
                this.roots[path] = node;
            }

            return node;
        }

        private string GetName(string item)
        {
            return item.Substring(item.LastIndexOf('/') + 1);
        }

        private string GetBasePath(string item)
        {
            return item.Substring(0, item.LastIndexOf('/'));
        }

        

        
    }
}
