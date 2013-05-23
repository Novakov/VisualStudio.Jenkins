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

                    if (folderItem.Children.Count == 1 && folderItem.Children[0] is FolderItem)
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

        public static IEnumerable<T> FixUpParents<T>(this IEnumerable<T> @this)
            where T : Item
        {
            foreach (var item in @this.OfType<FolderItem>())
            {
                item.Children.ForEach(x => x.Parent = item);
                item.Children.FixUpParents();
            }

            return @this;
        }

        public static IEnumerable<Item> Ancestors(this Item @this)
        {
            var item = @this.Parent;

            while (item != null)
            {
                yield return item;
                item = item.Parent;
            }
        }
    }
}
