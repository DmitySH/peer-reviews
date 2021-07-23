using System;
using System.Collections.ObjectModel;
using System.IO;
using MainApplication.Interfaces;
using MainApplication.ViewModels;
using Newtonsoft.Json;

namespace MainApplication.Models
{
    /// <summary>
    /// Sections.
    /// </summary>
    enum SectionType
    {
        Catalog,
        Folder
    }

    /// <summary>
    /// Model of catalog.
    /// </summary>
    internal class Catalog : ViewModels.Base.ViewModelBase, ICatalog, IFolder, IComparable<Catalog>
    {
        private bool isExpanded;

        /// <summary>
        /// Shows if this section is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get => isExpanded;
            set => Set(ref isExpanded, value);
        }

        private bool isSelected;

        /// <summary>
        /// Shows if this section is selected.
        /// </summary>
        [JsonIgnore]
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                Set(ref isSelected, value);
                if (value && this.SectionType == SectionType.Folder)
                {
                    MainWindowViewModel.OnSelectedChanged(this);
                }
                else if (value && this.SectionType == SectionType.Catalog)
                {
                    MainWindowViewModel.OnSelectedChanged(null);
                }
            }
        }

        private uint sortCode;
        /// <summary>
        /// Code for sort.
        /// </summary>
        public uint SortCode
        {
            get => sortCode;
            set => Set(ref sortCode, value);
        }

        /// <summary>
        /// Path to image.
        /// </summary>
        [JsonIgnore]
        public string Src
        {
            get
            {
                try
                {
                    if (SectionType == SectionType.Folder)
                    {
                        return $@"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}pictures{Path.DirectorySeparatorChar}folder.png";
                    }

                    return $@"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}pictures{Path.DirectorySeparatorChar}catalog.png";
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Type of section.
        /// </summary>
        public SectionType SectionType { get; }

        private string title;

        /// <summary>
        /// Title of section.
        /// </summary>
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        // Catalog can be to types: first is catalog: storage for catalogs and folders, second is folder: storage for items.
        private ObservableCollection<Catalog> catalogs;

        /// <summary>
        /// Collection of catalogs.
        /// </summary>
        public ObservableCollection<Catalog> Catalogs
        {
            get => catalogs;
            set => Set(ref catalogs, value);
        }

        /// <summary>
        /// Collection of items.
        /// </summary>
        public ObservableCollection<Item> Items { get; }

        /// <summary>
        /// Constructor with title and section type.
        /// </summary>
        /// <param name="title"> Title. </param>
        /// <param name="sectionType"> Type of section. </param>
        public Catalog(string title, SectionType sectionType)
        {

            SectionType = sectionType;
            Title = title;
            if (SectionType == SectionType.Folder)
            {
                Items = new ObservableCollection<Item>();
            }
            else
            {
                Catalogs = new ObservableCollection<Catalog>();
            }
        }

        /// <summary>
        /// DFS algorithm to find selected catalog. 
        /// </summary>
        /// <param name="catalog"> Start catalog. </param>
        /// <returns> Catalog which is is selected. </returns>
        public static Catalog FindSelected(Catalog catalog)
        {
            if (catalog.isSelected)
            {
                return catalog;
            }

            if (catalog.Catalogs == null)
            {
                return null;
            }

            foreach (var newCatalog in catalog.Catalogs)
            {
                Catalog nextNode = FindSelected(newCatalog);
                if (nextNode != null)
                {
                    return nextNode;
                }
            }

            return null;
        }

        /// <summary>
        /// ID of section.
        /// </summary>
        [JsonIgnore]
        public int ID { get; set; }

        /// <summary>
        /// DFS algorithm to find selected catalog and his parent. 
        /// </summary>
        /// <param name="catalog"> Start catalog tuple and his parent. </param>
        /// <returns> Tuple catalog which is is selected and his parent. </returns>
        public static (Catalog Child, Catalog Parent) FindSelectedAndParent(Catalog catalog, Catalog prev)
        {
            if (catalog.isSelected)
            {
                return (catalog, prev);
            }

            if (catalog.Catalogs == null)
            {
                return (null, null);
            }

            foreach (var newCatalog in catalog.Catalogs)
            {
                var nextNode = FindSelectedAndParent(newCatalog, catalog);
                if (nextNode.Child != null)
                {
                    return nextNode;
                }
            }

            return (null, null);
        }

        public int CompareTo(Catalog other) => this.SortCode.CompareTo(other.SortCode) == 0 ?
                string.Compare(this.Title, other.Title, StringComparison.Ordinal) : this.SortCode.CompareTo(other.SortCode);
    }
}
