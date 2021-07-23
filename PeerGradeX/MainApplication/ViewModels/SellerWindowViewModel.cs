using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CsvHelper;
using MainApplication.HelpClasses;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;
using MainApplication.ViewModels.Base;
using MainApplication.Views.Windows;

namespace MainApplication.ViewModels
{
    /// <summary>
    /// VM for SellerWindow.
    /// </summary>
    internal class SellerWindowViewModel : WorkingWindowBase
    {
        #region Properties
        // Properties from MVVM pattern.
        // Their names shows what are they doing.

        private List<Client> buyers;
        public List<Client> Buyers
        {
            get => buyers;
            set => Set(ref buyers, value);
        }

        private Client selectedClient;
        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                Set(ref selectedClient, value);
                OnPropertyChanged(nameof(SelectedClientOrders));
            }
        }

        public List<Order> SelectedClientOrders => (from order in Order.Orders
                                                    where order.Buyer.Equals(SelectedClient)
                                                    select order).ToList();

        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get => orders;
            set => Set(ref orders, value);
        }

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                Set(ref selectedOrder, value);
                OnPropertyChanged(nameof(ListBoxEnabled));
                OnPropertyChanged(nameof(IsProcessed));
                OnPropertyChanged(nameof(IsShipped));
                OnPropertyChanged(nameof(IsClosed));
            }
        }

        public bool IsProcessed
        {
            get => SelectedOrder != null && (SelectedOrder.Status & Status.Processed) == Status.Processed;
            set
            {
                if (SelectedOrder == null)
                {
                    return;
                }

                if (IsProcessed)
                {
                    SelectedOrder.Status ^= Status.Processed;
                }
                else
                {
                    SelectedOrder.Status |= Status.Processed;
                }
                OnPropertyChanged(nameof(IsProcessed));
            }
        }

        public bool IsShipped
        {
            get => SelectedOrder != null && (SelectedOrder.Status & Status.Shipped) == Status.Shipped;
            set
            {
                if (SelectedOrder == null)
                {
                    return;
                }

                if (IsShipped)
                {
                    SelectedOrder.Status ^= Status.Shipped;
                }
                else
                {
                    SelectedOrder.Status |= Status.Shipped;
                }
                OnPropertyChanged(nameof(IsShipped));
            }
        }

        public bool IsClosed
        {
            get => SelectedOrder != null && (SelectedOrder.Status & Status.Closed) == Status.Closed;
            set
            {
                if (SelectedOrder == null)
                {
                    return;
                }

                if (IsClosed)
                {
                    SelectedOrder.Status ^= Status.Closed;
                }
                else
                {
                    SelectedOrder.Status |= Status.Closed;
                    if (IsHided)
                    {
                        OnHideClosedCommandExecuted(null);
                    }
                }
                OnPropertyChanged(nameof(IsClosed));
            }
        }

        public bool ListBoxEnabled => SelectedOrder != null;

        private bool isHided;
        public bool IsHided
        {
            get => isHided;
            set => Set(ref isHided, value);
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

        #region ItemsCommands

        public ICommand HideClosedCommand { get; }
        private bool CanHideClosedCommandExecute(object p) => true;

        /// <summary>
        /// Hides closed orders.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnHideClosedCommandExecuted(object p)
        {
            SelectedOrder = null;
            if (!IsHided)
            {
                Orders = Order.Orders;
            }
            else
            {
                Orders = new ObservableCollection<Order>(from order in Order.Orders
                                                         where (order.Status & Status.Closed) == 0
                                                         select order);
            }
        }

        public ICommand AddItemCommand { get; }
        private bool CanAddItemCommandExecute(object p) => SelectedFolder?.Items != null;

        /// <summary>
        /// Adds item to selected folder.
        /// </summary>
        /// <param name="p"> Extra parameter </param>
        private void OnAddItemCommandExecuted(object p)
        {
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

        public ICommand CallBackCommand { get; }
        private bool CanCallBackCommandExecute(object p) => SelectedItem != null;


        private void OnCallBackCommandExecuted(object p)
        {
            var query = from order in Order.Orders
                        where order.Items.Any(x => x.Equals(SelectedItem))
                        group order by order.Buyer.Email into g
                        select new { Name = g.Key, Tel = g.First().Buyer.TelNumber, Dates = string.Join(" || ", g.Select(x=>x.Date))};

            MessageBox.Show($"Who has {SelectedItem.Name} item in order :{Environment.NewLine}{Environment.NewLine}" +
                            $"{string.Join(Environment.NewLine, query.ToList().ConvertAll(x => $"Client {x.Name}, tel.: {x.Tel}, bought at: {x.Dates}"))}", "Report");

        }

        #endregion

        #region MenuCommands

        public ICommand CreateSecondReportCommand { get; }
        private bool CanCreateSecondReportCommandExecute(object p) => true;

        /// <summary>
        /// Creates new warehouse.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnCreateSecondReportCommandExecuted(object p)
        {
            var query = from grp in
                            from order in
                                from order in Order.Orders
                                where (order.Status & Status.Paid) != 0
                                select order
                            group order by order.Buyer.Email
                        let sum = grp.Sum(x => x.Items.Sum(y => y.TotalPrice))
                        where sum > Settings.MinPayed
                        orderby sum descending
                        select (Email: grp.Key, Sum: sum);

            MessageBox.Show($"Who bought on more than {Settings.MinPayed}:{Environment.NewLine}" +
                            $"{string.Join(Environment.NewLine, query.ToList().ConvertAll(x=> $"{x.Email} with {x.Sum}"))}", "Report");
        }

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

        public ICommand CreateReportCommand { get; }
        private bool CanCreateReportCommandExecute(object p) => true;

        /// <summary>
        /// Creates CSV report for current warehouse.
        /// </summary>
        /// <param name="p"></param>
        private void OnCreateReportCommandExecuted(object p)
        {
            try
            {
                PathCreator creator = new PathCreator(RootsCollection[0]);
                using (StreamWriter tw = new StreamWriter($"reports{Path.DirectorySeparatorChar}{RootsCollection[0].Title}.csv"))
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

        public ICommand ShowInfoCommand { get; }
        protected bool CanShowInfoCommandExecute(object p) => true;

        /// <summary>
        /// Shows info.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        protected void OnShowInfoCommandExecuted(object p) =>
            MessageBox.Show($"Hello and welcome to my warehouse manager =)" +
                            $"{Environment.NewLine}" +
                            $"To add catalog or folder select section and use context menu (left part)" +
                            $"{Environment.NewLine}" +
                            $"To add goods to folder select folder and use context menu (right part)" +
                            $"{Environment.NewLine}" +
                            $"To open good's card make double click on it" +
                            $"{Environment.NewLine}" +
                            $"To add photo use context menu by clicking on text Photo upside (header of tab page)" +
                            $"{Environment.NewLine}" +
                            $"You can configure random creation using settings" +
                            $"{Environment.NewLine}" +
                            $"To create report about bad goods use context menu on that item");

        #endregion

        public SellerWindowViewModel(Client user) : base(user)
        {
            Buyers = Client.Buyers;
            Orders = Order.Orders;

            // Create all the commands.
            AddFolderCommand = new RelayCommand(OnAddFolderCommandExecuted, CanAddFolderCommandExecute);
            AddCatalogCommand = new RelayCommand(OnAddCatalogExecuted, CanAddCatalogCommandExecute);
            RemoveSectionCommand = new RelayCommand(OnRemoveSectionCommandExecuted, CanRemoveSectionCommandExecute);
            AddItemCommand = new RelayCommand(OnAddItemCommandExecuted, CanAddItemCommandExecute);
            EditItemCommand = new RelayCommand(OnEditItemCommandExecuted, CanEditItemCommandExecute);
            RemoveItemCommand = new RelayCommand(OnRemoveItemCommandExecuted, CanRemoveItemCommandExecute);
            EditSectionCommand = new RelayCommand(OnEditSectionCommandExecuted, CanEditSectionCommandExecute);
            NewWarehouseCommand = new RelayCommand(OnNewWarehouseCommandExecuted, CanNewWarehouseCommandExecute);
            CreateReportCommand = new RelayCommand(OnCreateReportCommandExecuted, CanCreateReportCommandExecute);
            OpenSettingsCommand = new RelayCommand(OnOpenSettingsCommandExecuted, CanOpenSettingsCommandExecute);
            CreateRandomCommand = new RelayCommand(OnCreateRandomCommandExecuted, CanCreateRandomCommandExecute);
            SortCommand = new RelayCommand(OnSortCommandExecuted, CanSortCommandExecute);
            ShowInfoCommand = new RelayCommand(OnShowInfoCommandExecuted, CanShowInfoCommandExecute);
            HideClosedCommand = new RelayCommand(OnHideClosedCommandExecuted, CanHideClosedCommandExecute);
            CreateSecondReportCommand =
                new RelayCommand(OnCreateSecondReportCommandExecuted, CanCreateSecondReportCommandExecute);
            CallBackCommand = new RelayCommand(OnCallBackCommandExecuted, CanCallBackCommandExecute);
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
    }
}