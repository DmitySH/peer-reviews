using System.Collections.ObjectModel;
using MainApplication.Models;

namespace MainApplication.Interfaces
{
    /// <summary>
    /// Interface for Catalog.
    /// </summary>
    interface ICatalog
    {
        ObservableCollection<Catalog> Catalogs { get; }
    }
}
