using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Paths
{
    public static class ItemExtensions
    {
        public static IEnumerable<Item> ConvertToLeaves(this IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                var folderItem = item as FolderItem;

                if (folderItem != null && folderItem.Children.Any())
                {
                    yield return new FolderItem
                    {
                        Name = item.Name,
                        Children = folderItem.Children.ConvertToLeaves().ToList()
                    };
                }
                else
                {
                    yield return new FileItem
                    {
                        Name = item.Name
                    };
                }
            }
        }

        public static IEnumerable<Item> JoinPassthroughNodes(this IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                if (item is FileItem)
                {
                    yield return item;
                }
                else
                {
                    var folderItem = (FolderItem)item;

                    if (folderItem.Children.Count == 1)
                    {
                        foreach (var child in folderItem.Children.JoinPassthroughNodes())
                        {
                            child.Name = folderItem.Name + '/' + child.Name;

                            yield return child;
                        }
                    }
                    else
                    {
                        folderItem.Children = folderItem.Children.JoinPassthroughNodes().ToList();
                        yield return folderItem;
                    }
                }
            }
        }
    }
}
