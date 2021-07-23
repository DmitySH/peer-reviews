using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;
using Microsoft.Win32;

namespace MainApplication.ViewModels
{
    /// <summary>
    /// MV of ItemCardWindow.
    /// </summary>
    internal class ItemCardWindowViewModel : Base.ViewModelBase
    {
        private static readonly Random rnd = new Random();

        private Item selectedItem;
        public Item SelectedItem
        {
            get => selectedItem;
            set => Set(ref selectedItem, value);
        }

        public ICommand LoadPictureCommand { get; }
        private bool CanLoadPictureCommandExecute(object p) => true;

        /// <summary>
        /// Opens picture.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnLoadPictureCommandExecuted(object p)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png)|*.png;";

                if (openFileDialog.ShowDialog() == true)
                {
                    string name = rnd.Next(10000) + ".png";
                    DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar +
                                                         "savedPictures");
                    int attempt = 0;
                    while (di.GetFiles().Any(x => x.Name.Equals(name)) && attempt < 10000)
                    {
                        name = rnd.Next(10000) + ".png";
                        attempt++;
                    }

                    if (attempt >= 10000)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    File.Copy(openFileDialog.FileName,
                        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar +
                        "savedPictures" + Path.DirectorySeparatorChar + name);
                    SelectedItem.PictureName = Path.DirectorySeparatorChar + name;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Can't load picture");
            }
        }

        public ItemCardWindowViewModel()
        {
            LoadPictureCommand = new RelayCommand(OnLoadPictureCommandExecuted, CanLoadPictureCommandExecute);
        }
    }
}