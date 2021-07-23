using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MainApplication.Infrastructure.Commands.Base;
using MainApplication.Models;

namespace MainApplication.ViewModels
{
    internal class BuyerWindowViewModel : Base.WorkingWindowBase
    {

        #region Properties
        // Properties from MVVM pattern.
        // Their names shows what are they doing.

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get => selectedOrder;
            set => Set(ref selectedOrder, value);
        }

        public List<Order> UserOrders => (from order in Order.Orders
                                          where order.Buyer.Equals(user)
                                          select order).ToList();

        private Order currentOrder;

        public Order CurrentOrder
        {
            get => currentOrder;
            set => Set(ref currentOrder, value);
        }

        #endregion

        #region Commands

        public ICommand ShowOrderCommand { get; }
        private bool CanShowOrderCommandExecute(object p) => SelectedOrder != null;

        /// <summary>
        /// Shows info about order.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnShowOrderCommandExecuted(object p) => MessageBox.Show(SelectedOrder.LongInfo, "Information");

        public ICommand PayForOrderCommand { get; }
        private bool CanPayForOrderCommandExecute(object p) => SelectedOrder != null &&
                                                               (SelectedOrder.Status & Status.Processed) == Status.Processed &&
                                                               (SelectedOrder.Status & Status.Paid) != Status.Paid;

        /// <summary>
        /// Pays for order.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnPayForOrderCommandExecuted(object p) =>
            SelectedOrder.Status = SelectedOrder.Status | Status.Paid;

        public ICommand CreateOrderCommand { get; }
        private bool CanCreateOrderCommandExecute(object p) => true;

        /// <summary>
        /// Creates new order.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnCreateOrderCommandExecuted(object p)
        {
            if (CurrentOrder.Items.Count == 0)
            {
                MessageBox.Show("Order can not be empty", "Attention");
                return;
            }
            CurrentOrder.Number = Order.currentNumber++;
            CurrentOrder.Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            Order.Orders.Add(CurrentOrder);
            OnPropertyChanged(nameof(UserOrders));
            CurrentOrder = new Order(user);
        }

        public ICommand AddItemCommand { get; }
        private bool CanAddItemCommandExecute(object p) => true;

        /// <summary>
        /// Adds item to current order.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        private void OnAddItemCommandExecuted(object p)
        {
            if (SelectedItem.Quantity < SelectedItem.ToBuy || SelectedItem.ToBuy == 0)
            {
                return;
            }

            if (CurrentOrder.Items.Any(x => x.Equals(SelectedItem)))
            {
                currentOrder.Items.First(x => x.Equals(SelectedItem)).Quantity += SelectedItem.ToBuy;
            }
            else
            {
                CurrentOrder.Items.Add(SelectedItem.Clone() as Item);
            }
            SelectedItem.Quantity -= SelectedItem.ToBuy;
        }

        public ICommand ShowInfoCommand { get; }
        protected bool CanShowInfoCommandExecute(object p) => true;

        /// <summary>
        /// Shows info.
        /// </summary>
        /// <param name="p"> Extra parameter. </param>
        protected void OnShowInfoCommandExecuted(object p) =>
            MessageBox.Show($"Hello and welcome =)" +
                            $"{Environment.NewLine}" +
                            $"To add something to the bracket select quantity and double left click on it" + 
                            $"{Environment.NewLine}" + 
                            $"Use context menu on your orders to manipulate them");


        #endregion

        public BuyerWindowViewModel(Client user) : base(user)
        {
            CurrentOrder = new Order(user);

            // Create all the commands.
            AddItemCommand = new RelayCommand(OnAddItemCommandExecuted, CanAddItemCommandExecute);
            ShowInfoCommand = new RelayCommand(OnShowInfoCommandExecuted, CanShowInfoCommandExecute);
            CreateOrderCommand = new RelayCommand(OnCreateOrderCommandExecuted, CanCreateOrderCommandExecute);
            ShowOrderCommand = new RelayCommand(OnShowOrderCommandExecuted, CanShowOrderCommandExecute);
            PayForOrderCommand = new RelayCommand(OnPayForOrderCommandExecuted, CanPayForOrderCommandExecute);
        }
    }
}
