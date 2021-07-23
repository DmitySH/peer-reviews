using System.Collections.ObjectModel;
using MainApplication.Models;

namespace MainApplication.Interfaces
{
    /// <summary>
    /// Interface for folder.
    /// </summary>
    interface IFolder
    {
        ObservableCollection<Item> Items { get; }
    }
}
