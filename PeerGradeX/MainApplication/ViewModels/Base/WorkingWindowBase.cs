using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;
using MainApplication.Views.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MainApplication.ViewModels.Base
{
    internal abstract class WorkingWindowBase : ViewModelBase
    {
        protected WorkingWindowBase(Client user)
        {
            this.user = user;

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

            CloseWindowCommand = new RelayCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            OpenFileCommand = new RelayCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            SaveFileCommand = new RelayCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);
            SelectedChanged += catalog => this.SelectedFolder = catalog;
            LogOutCommand = new RelayCommand(OnLogOutCommandExecuted, CanLogOutCommandExecute);
        }



        /// <summary>
        /// Finds catalog using ID.
        /// </summary>
        /// <param name="current"> Current catalog. </param>
        /// <param name="id"> ID to find. </param>
        /// <returns> Catalog with same id. </returns>
        protected static Catalog FindID(Catalog current, int id)
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
        protected void SaveData(string path)
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

        public ICommand SaveFileCommand { get; }
        protected bool CanSaveFileCommandExecute(object p) => true;

        /// <summary>
        /// Creates save for this warehouse with specific name.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        protected void OnSaveFileCommandExecuted(object p)
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

        public ICommand LogOutCommand { get; }
        protected bool CanLogOutCommandExecute(object p) => true;

        /// <summary>
        /// Creates new warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        protected void OnLogOutCommandExecuted(object p)
        {
            new StartUpWindow().Show();
            (p as Window)?.Close();
        }

        public ICommand OpenFileCommand { get; }
        protected bool CanOpenFileCommandExecute(object p) => true;

        /// <summary>
        /// Opens saved warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        protected void OnOpenFileCommandExecuted(object p)
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

                }
            }
        }

        // Occurs when window closes.
        public ICommand CloseWindowCommand { get; }
        protected bool CanCloseWindowCommandExecute(object p) => true;

        /// <summary>
        /// Saves current warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        protected void OnCloseWindowCommandExecuted(object p) =>
            SaveData($"data{Path.DirectorySeparatorChar}{RootsCollection[0].Title}.json");

        protected Catalog selectedFolder;
        public Catalog SelectedFolder
        {
            get => selectedFolder;
            set => Set(ref selectedFolder, value);
        }

        protected ObservableCollection<Catalog> rootsCollection = new ObservableCollection<Catalog>();
        public ObservableCollection<Catalog> RootsCollection
        {
            get => rootsCollection;
            set => Set(ref rootsCollection, value);
        }

        protected Item selectedItem;
        public Item SelectedItem
        {
            get => selectedItem;
            set => Set(ref selectedItem, value);
        }

        public string UserInfo => (this is BuyerWindowViewModel) ? $"Shopping for {user.Email}" : $"Managing for {user.Email}";
        protected Client user;

        protected static readonly Random rnd = new Random();
        public static event Action<Catalog> SelectedChanged;

        /// <summary>
        /// Sends that selected catalog was changed.
        /// </summary>
        /// <param name="catalog"> Catalog that become selected. </param>
        public static void OnSelectedChanged(Catalog catalog) => SelectedChanged?.Invoke(catalog);
    }
}
