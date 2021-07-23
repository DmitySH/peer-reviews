using System.Collections.Generic;
using System.Text;
using MainApplication.Models;

namespace MainApplication.HelpClasses
{
    /// <summary>
    /// Finds items to buy and paths for them.
    /// </summary>
    internal class PathCreator
    {
        private readonly Catalog root;

        public PathCreator(Catalog root)
        {
            this.root = root;
            toBuy = new List<Item>(8);
            path = new StringBuilder(root.Title + "/");
        }

        private readonly List<Item> toBuy;
        public List<Item> ToBuy
        {
            get
            {
                IndexAll(root);
                return toBuy;
            }
        }

        private StringBuilder path;

        /// <summary>
        /// Indexes all catalogs and items to buy.
        /// </summary>
        /// <param name="catalog"> Catalog to start with. </param>
        public void IndexAll(Catalog catalog)
        {
            if (catalog.SectionType == SectionType.Folder)
            {
                foreach (var item in catalog.Items)
                {
                    if (item.Quantity < item.MinQuantity)
                    {
                        item.Path = path + item.Name;
                        toBuy.Add(item);
                    }
                }

                return;
            }

            if (catalog.SectionType == SectionType.Catalog)
            {
                foreach (var newCatalog in catalog.Catalogs)
                {
                    path.Append($"{newCatalog.Title}/");
                    int lastAddedLength = newCatalog.Title.Length + 1;
                    IndexAll(newCatalog);
                    path = path.Remove(path.Length - lastAddedLength, lastAddedLength);
                }
            }
        }
    }
}
