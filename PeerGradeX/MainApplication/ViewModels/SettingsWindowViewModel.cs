using MainApplication.Models;

namespace MainApplication.ViewModels
{
    /// <summary>
    /// MV of SettingsWindow.
    /// </summary>
    internal class SettingsWindowViewModel : Base.ViewModelBase
    {
        // This MV just updates settings' static class.
        private int catalogNumber;
        public int CatalogNumber
        {
            get => catalogNumber;
            set
            {
                Set(ref catalogNumber, value);
                Settings.CatalogNumber = catalogNumber;
            }
        }

        private int itemNumber;
        public int ItemNumber
        {
            get => itemNumber;
            set
            {
                Set(ref itemNumber, value);
                Settings.ItemNumber = itemNumber;
            }
        }

        private int folderNumber;
        public int FolderNumber
        {
            get => folderNumber;
            set
            {
                Set(ref folderNumber, value);
                Settings.FolderNumber = folderNumber;
            }
        }

        private int minPayed;
        public int MinPayed
        {
            get => minPayed;
            set
            {
                Set(ref minPayed, value);
                Settings.MinPayed = MinPayed;
            }
        }

        public SettingsWindowViewModel()
        {
            ItemNumber = Settings.ItemNumber;
            CatalogNumber = Settings.CatalogNumber;
            FolderNumber = Settings.FolderNumber;
            MinPayed = Settings.MinPayed;
        }
    }
}
