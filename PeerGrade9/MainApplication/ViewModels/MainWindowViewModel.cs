using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CsvHelper;
using MainApplication.HelpClasses;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;
using MainApplication.ViewModels.Base;
using MainApplication.Views.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MainApplication.ViewModels
{
    /// <summary>
    /// VM for MainWindow.
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {

        private static readonly Random rnd = new Random();
        public static event Action<Catalog> SelectedChanged;
        /// <summary>
        /// Sends that selected catalog was changed.
        /// </summary>
        /// <param name="catalog"> Catalog that become selected. </param>
        public static void OnSelectedChanged(Catalog catalog) => SelectedChanged?.Invoke(catalog);

        #region Properties
        // Properties from MVVM pattern.
        // Their names shows what are they doing.

        private Catalog selectedFolder;
        public Catalog SelectedFolder
        {
            get => selectedFolder;
            set => Set(ref selectedFolder, value);
        }

        private ObservableCollection<Catalog> rootsCollection = new ObservableCollection<Catalog>();
        public ObservableCollection<Catalog> RootsCollection
        {
            get => rootsCollection;
            set => Set(ref rootsCollection, value);
        }

        private Item selectedItem;
        public Item SelectedItem
        {
            get => selectedItem;
            set => Set(ref selectedItem, value);
        }

        #endregion

        #region ContextCommands
        // Commands for context menu.

        public ICommand EditSectionCommand { get; }
        private bool CanEditSectionCommandExecute(object p) => true;

        /// <summary>
        /// Renames selected section.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnEditSectionCommandExecuted(object p)
        {
            var selected = Catalog.FindSelectedAndParent(RootsCollection[0], null);
            if (selected.Child == null || selected.Parent == null)
            {
                return;
            }

            GetTitleWindow getTitleWindow = new GetTitleWindow() { DataContext = new GetTitleWindowViewModel() { Code = selected.Child.SortCode, Title = selected.Child.Title } };
            bool? ok = getTitleWindow.ShowDialog();

            if (ok == null || (bool)!ok)
            {
                return;
            }

            if (selected.Parent.Catalogs.Any(x => x.Title.Equals(GetTitleWindowViewModel.toSendTitle) && x != selected.Child))
            {
                MessageBox.Show("Already have section with same title in this catalog");
                return;
            }

            if (GetTitleWindowViewModel.toSendTitle.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Title can't be empty");
                return;
            }

            selected.Child.SortCode = GetTitleWindowViewModel.toSendCode;
            selected.Child.Title = GetTitleWindowViewModel.toSendTitle;
        }

        public ICommand RemoveSectionCommand { get; }
        private bool CanRemoveSectionCommandExecute(object p) => true;

        /// <summary>
        /// Removes selected section.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnRemoveSectionCommandExecuted(object p)
        {
            var selected = Catalog.FindSelectedAndParent(RootsCollection[0], null);

            if (selected.Child == null || selected.Parent == null)
            {
                return;
            }

            if (selected.Child.SectionType == SectionType.Catalog && selected.Child.Catalogs.Count > 0 ||
                selected.Child.SectionType == SectionType.Folder && selected.Child.Items.Count > 0)
            {
                MessageBox.Show("Section has to be empty to remove");
                return;
            }

            SelectedFolder = null;
            selected.Parent.Catalogs.Remove(selected.Child);
        }

        public ICommand AddFolderCommand { get; }
        private bool CanAddFolderCommandExecute(object p) => true;

        /// <summary>
        /// Adds folder to selected section.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnAddFolderCommandExecuted(object p)
        {
            Catalog selected = Catalog.FindSelected(RootsCollection[0]);
            if (selected == null)
            {
                return;
            }

            if (selected.SectionType == SectionType.Folder)
            {
                MessageBox.Show("Can't add section to folder");
                return;
            }

            GetTitleWindow getTitleWindow = new GetTitleWindow();
            bool? ok = getTitleWindow.ShowDialog();

            if (ok == null || (bool)!ok)
            {
                return;
            }

            if (selected.Catalogs.Any(x => x.Title.Equals(GetTitleWindowViewModel.toSendTitle)))
            {
                MessageBox.Show("Already have section with same title in this catalog");
                return;
            }

            if (GetTitleWindowViewModel.toSendTitle.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Title can't be empty");
                return;
            }

            selected.Catalogs.Add(new Catalog(GetTitleWindowViewModel.toSendTitle, SectionType.Folder));
            selected.IsExpanded = true;
        }

        public ICommand AddCatalogCommand { get; }
        private bool CanAddCatalogCommandExecute(object p) => true;

        /// <summary>
        /// Adds catalog to selected section.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnAddCatalogExecuted(object p)
        {
            Catalog selected = Catalog.FindSelected(RootsCollection[0]);
            if (selected == null)
            {
                return;
            }

            if (selected.SectionType == SectionType.Folder)
            {
                MessageBox.Show("Can't add section to folder");
                return;
            }

            GetTitleWindow getTitleWindow = new GetTitleWindow();
            bool? ok = getTitleWindow.ShowDialog();

            if (ok == null || (bool)!ok)
            {
                return;
            }

            if (selected.Catalogs.Any(x => x.Title.Equals(GetTitleWindowViewModel.toSendTitle)))
            {
                MessageBox.Show("Already have section with same title in this catalog");
                return;
            }

            if (GetTitleWindowViewModel.toSendTitle.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Title can't be empty");
                return;
            }

            selected.Catalogs.Add(new Catalog(GetTitleWindowViewModel.toSendTitle, SectionType.Catalog) { SortCode = GetTitleWindowViewModel.toSendCode });
            selected.IsExpanded = true;
        }
        #endregion

        #region CloseWindowCommand

        // Occurs when window closes.
        public ICommand CloseWindowCommand { get; }
        private bool CanCloseWindowCommandExecute(object p) => true;

        /// <summary>
        /// Saves current warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnCloseWindowCommandExecuted(object p) => SaveData($"data{Path.DirectorySeparatorChar}{RootsCollection[0].Title}.json");

        #endregion

        #region ItemsCommands

        public ICommand AddItemCommand { get; }
        private bool CanAddItemCommandExecute(object p) => SelectedFolder?.Items != null;

        /// <summary>
        /// Adds item to selected folder.
        /// </summary>
        /// <param name="p"> Extra parameter </param>
        private void OnAddItemCommandExecuted(object p)
        {
            IList kk = new ArrayList();
            Item item = new Item();
            SelectedFolder.Items.Add(item);
            SelectedItem = item;
            OnEditItemCommandExecuted(null);
        }

        public ICommand EditItemCommand { get; }
        private bool CanEditItemCommandExecute(object p) => SelectedItem != null;

        /// <summary>
        /// Opens item's card of selected item.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnEditItemCommandExecuted(object p)
        {
            new ItemCardWindow() { DataContext = new ItemCardWindowViewModel() { SelectedItem = this.SelectedItem } }
                .ShowDialog();

            if (SelectedItem.Name == null || SelectedItem.Name.Trim() == string.Empty)
            {
                SelectedItem.Name = 1.ToString();
                MessageBox.Show("Title can't be empty");
            }

            string name = selectedItem.Name;
            int i = 1;
            while (SelectedFolder.Items.Count(x => x.Name.Equals(SelectedItem.Name)) > 1)
            {
                SelectedItem.Name = name + i++;
            }

            if (i != 1)
            {
                MessageBox.Show("Already have section with same title in this catalog");
            }
        }

        public ICommand RemoveItemCommand { get; }
        private bool CanRemoveItemCommandExecute(object p) => SelectedItem != null && SelectedFolder?.Items != null;

        /// <summary>
        /// Removes selected item.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnRemoveItemCommandExecuted(object p) => selectedFolder.Items.Remove(SelectedItem);

        #endregion

        #region MenuCommands
        public ICommand NewWarehouseCommand { get; }
        private bool CanNewWarehouseCommandExecute(object p) => true;

        /// <summary>
        /// Creates new warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnNewWarehouseCommandExecuted(object p)
        {
            GetTitleWindow getTitleWindow = new GetTitleWindow();
            bool? ok = getTitleWindow.ShowDialog();

            if (ok == null || (bool)!ok)
            {
                return;
            }
            if (GetTitleWindowViewModel.toSendTitle.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Title can't be empty");
                return;
            }
            OnCloseWindowCommandExecuted(null);
            RootsCollection[0] = new Catalog(GetTitleWindowViewModel.toSendTitle, SectionType.Catalog);
        }

        public ICommand OpenFileCommand { get; }
        private bool CanOpenFileCommandExecute(object p) => true;

        /// <summary>
        /// Opens saved warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnOpenFileCommandExecuted(object p)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json";
            try
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "data";
            }
            catch (Exception)
            {
            }

            bool? ok = openFileDialog.ShowDialog();
            if (ok != null && ok == true)
            {
                try
                {
                    OnCloseWindowCommandExecuted(null);
                    RootsCollection[0] = LoadData(openFileDialog.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't open this file");
                }
            }
        }

        public ICommand CreateReportCommand { get; }
        private bool CanCreateReportCommandExecute(object p) => true;

        /// <summary>
        /// Creates CSV report for current warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnCreateReportCommandExecuted(object p)
        {
            try
            {
                PathCreator creator = new PathCreator(RootsCollection[0]);
                using (StreamWriter tw = new StreamWriter(File.Create($"reports{Path.DirectorySeparatorChar}{RootsCollection[0].Title}.csv"), Encoding.UTF8))
                {
                    using (CsvWriter csvReader = new CsvWriter(tw, CultureInfo.InvariantCulture))
                    {
                        csvReader.WriteRecords(creator.ToBuy);
                    }
                }

                MessageBox.Show("Report created in reports folder in current app path");
            }
            catch (Exception)
            {
                MessageBox.Show("Can't create report");
            }
        }

        public ICommand OpenSettingsCommand { get; }
        private bool CanOpenSettingsCommandExecute(object p) => true;

        /// <summary>
        /// Opens setting's window.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnOpenSettingsCommandExecuted(object p) => new SettingsWindow().ShowDialog();

        public ICommand CreateRandomCommand { get; }
        private bool CanCreateRandomCommandExecute(object p) => true;

        /// <summary>
        /// Creates new random warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnCreateRandomCommandExecuted(object p)
        {
            OnNewWarehouseCommandExecuted(null);
            Catalog current = RootsCollection[0];
            if (current.Catalogs.Count > 0)
            {
                return;
            }

            int CNumber = Settings.CatalogNumber;
            int INumber = Settings.ItemNumber;
            int FNumber = Settings.FolderNumber;
            RandomWarehouseCreation(CNumber, INumber, FNumber, current);
        }

        public ICommand SortCommand { get; }
        private bool CanSortCommandExecute(object p) => true;

        /// <summary>
        /// Sorts using sort code of catalog.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnSortCommandExecuted(object p) => SortCatalogs(RootsCollection[0]);

        /// <summary>
        /// Sorts.
        /// </summary>
        /// <param name="catalog"> Root catalog. </param>
        public void SortCatalogs(Catalog catalog)
        {
            if (catalog.SectionType == SectionType.Catalog)
            {
                List<Catalog> catalogsList = new List<Catalog>(catalog.Catalogs);
                catalogsList.Sort();
                catalog.Catalogs = new ObservableCollection<Catalog>(catalogsList);
                foreach (var newCatalog in catalog.Catalogs)
                {
                    SortCatalogs(newCatalog);
                }
            }
        }

        public ICommand SaveFileCommand { get; }
        private bool CanSaveFileCommandExecute(object p) => true;

        /// <summary>
        /// Creates save for this warehouse with specific name.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnSaveFileCommandExecuted(object p)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON Files (*.json)|*.json";

                try
                {
                    saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "data";
                }
                catch (Exception)
                {
                }

                bool? ok = saveFileDialog.ShowDialog();

                if (ok != null && ok == true)
                {
                    SaveData(saveFileDialog.FileName);
                    MessageBox.Show("Warehouse was saved");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't save warehouse");
            }
        }

        public ICommand ShowInfoCommand { get; }
        private bool CanShowInfoCommandExecute(object p) => true;

        /// <summary>
        /// Shows info.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnShowInfoCommandExecuted(object p) =>
            MessageBox.Show($"Hello and welcome to my warehouse manager =)" +
                            $"{Environment.NewLine}" +
                            $"Hope you will enjoy" +
                            $"{Environment.NewLine}" +
                            $"To add catalog or folder select section and use context menu (left part)" +
                            $"{Environment.NewLine}" +
                            $"To add goods to folder select folder and use context menu (right part)" +
                            $"{Environment.NewLine}" +
                            $"To open good's card make double click on it" +
                            $"{Environment.NewLine}" +
                            $"To add photo use context menu" +
                            $"{Environment.NewLine}" +
                            $"To use sort code enter it in section window" +
                            $"{Environment.NewLine}" +
                            $"The bigger code, the lower this section is" +
                            $"{Environment.NewLine}" +
                            $"You can configure random creation using settings" +
                            $"{Environment.NewLine}{Environment.NewLine}" +
                            $"Good luck and have a nice day", "About program");

        #endregion

        public MainWindowViewModel()
        {
            // Finds last opened warehouse.
            FileInfo lastWrited = null;
            try
            {
                var directory = new DirectoryInfo(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "data");
                lastWrited = directory.GetFiles()[0];
                foreach (var file in directory.GetFiles())
                {
                    if (file.LastWriteTime > lastWrited.LastWriteTime)
                    {
                        lastWrited = file;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not load last warehouse");
            }

            Catalog root = LoadData(lastWrited?.FullName);
            RootsCollection = new ObservableCollection<Catalog> { root };

            // Create all the commands.
            AddFolderCommand = new RelayCommand(OnAddFolderCommandExecuted, CanAddFolderCommandExecute);
            AddCatalogCommand = new RelayCommand(OnAddCatalogExecuted, CanAddCatalogCommandExecute);
            RemoveSectionCommand = new RelayCommand(OnRemoveSectionCommandExecuted, CanRemoveSectionCommandExecute);
            CloseWindowCommand = new RelayCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            AddItemCommand = new RelayCommand(OnAddItemCommandExecuted, CanAddItemCommandExecute);
            EditItemCommand = new RelayCommand(OnEditItemCommandExecuted, CanEditItemCommandExecute);
            RemoveItemCommand = new RelayCommand(OnRemoveItemCommandExecuted, CanRemoveItemCommandExecute);
            EditSectionCommand = new RelayCommand(OnEditSectionCommandExecuted, CanEditSectionCommandExecute);
            OpenFileCommand = new RelayCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            NewWarehouseCommand = new RelayCommand(OnNewWarehouseCommandExecuted, CanNewWarehouseCommandExecute);
            CreateReportCommand = new RelayCommand(OnCreateReportCommandExecuted, CanCreateReportCommandExecute);
            OpenSettingsCommand = new RelayCommand(OnOpenSettingsCommandExecuted, CanOpenSettingsCommandExecute);
            CreateRandomCommand = new RelayCommand(OnCreateRandomCommandExecuted, CanCreateRandomCommandExecute);
            SortCommand = new RelayCommand(OnSortCommandExecuted, CanSortCommandExecute);
            ShowInfoCommand = new RelayCommand(OnShowInfoCommandExecuted, CanShowInfoCommandExecute);
            SaveFileCommand = new RelayCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);
            SelectedChanged += catalog => this.SelectedFolder = catalog;
        }

        /// <summary>
        /// Loads warehouse from path.
        /// </summary>
        /// <param name="path"> Path to execute. </param>
        /// <returns> Root catalog of loaded warehouse. </returns>
        public Catalog LoadData(string path)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sw = new StreamReader(path))
                {
                    using (JsonReader reader = new JsonTextReader(sw))
                    {
                        return (Catalog)serializer.Deserialize(reader, typeof(Catalog));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not load data");
                return new Catalog("Root", SectionType.Catalog);
            }
        }

        /// <summary>
        /// Saves warehouse to path.
        /// </summary>
        /// <param name="path"> Path to save. </param>
        private void SaveData(string path)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                using (StreamWriter sw = new StreamWriter(path))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, RootsCollection[0]);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not save data");
            }
        }

        /// <summary>
        /// Generates random warehouse.
        /// </summary>
        /// <param name="cNumber"> Number of catalogs. </param>
        /// <param name="iNumber"> Number of items. </param>
        /// <param name="fNumber"> Number of folders. </param>
        /// <param name="root"> Root catalog. </param>
        private static void RandomWarehouseCreation(int cNumber, int iNumber, int fNumber, Catalog root)
        {
            int createdCatalogs = 0;
            int createdFolders = 0;
            int createdItems = 0;
            cNumber -= 1;

            for (int i = 0; i < cNumber; i++)
            {
                FindID(root, rnd.Next(createdCatalogs + 1))
                    .Catalogs.Add(new Catalog($"{createdCatalogs + 1}c", SectionType.Catalog)
                    { ID = createdCatalogs + 1 });
                createdCatalogs++;
            }

            for (int i = 0; i < fNumber; i++)
            {
                FindID(root, rnd.Next(cNumber)).Catalogs.Add(new Catalog($"{createdFolders}f", SectionType.Folder)
                { ID = cNumber + createdFolders + 1 });
                createdFolders++;
            }

            for (int i = 0; i < iNumber; i++)
            {
                FindID(root, rnd.Next(fNumber) + cNumber + 1).Items.Add(new Item()
                {
                    Name = $"{createdItems}i",
                    Article = rnd.Next(1000),
                    MinQuantity = rnd.Next(1000),
                    Price = rnd.Next(10000),
                    Quantity = rnd.Next(1000)
                });
                createdItems++;
            }
        }

        /// <summary>
        /// Finds catalog using ID.
        /// </summary>
        /// <param name="current"> Current catalog. </param>
        /// <param name="id"> ID to find. </param>
        /// <returns> Catalog with same id. </returns>
        private static Catalog FindID(Catalog current, int id)
        {
            if (current.ID == id)
            {
                return current;
            }

            if (current.Catalogs == null)
            {
                return null;
            }

            foreach (var currentCatalog in current.Catalogs)
            {
                Catalog newCurrent = FindID(currentCatalog, id);
                if (newCurrent != null)
                {
                    return newCurrent;
                }
            }

            return null;
        }
    }
}