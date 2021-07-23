using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MainApplication.Models
{
    /// <summary>
    /// Statuses for order.
    /// </summary>
    [Flags]
    internal enum Status
    {
        Created = 0,
        Processed = 1,
        Paid = 2,
        Shipped = 4,
        Closed = 8
    }

    /// <summary>
    /// Model of order.
    /// </summary>
    internal class Order : ViewModels.Base.ViewModelBase
    {
        public static uint currentNumber = 1;

        private Status status;

        /// <summary>
        /// Status of order.
        /// </summary>
        public Status Status
        {
            get => status;
            set
            {
                Set(ref status, value);
                OnPropertyChanged(nameof(ShortInfo));
            }
        }

        /// <summary>
        /// All orders.
        /// </summary>
        public static ObservableCollection<Order> Orders;

        /// <summary>
        /// Items in order.
        /// </summary>
        public ObservableCollection<Item> Items { get; }

        /// <summary>
        /// Number of order.
        /// </summary>
        public uint Number { get; set; }

        /// <summary>
        /// Date of creation.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Buyer of an order.
        /// </summary>
        public Client Buyer { get; }

        /// <summary>
        /// Constructor for order.
        /// </summary>
        /// <param name="buyer"> Buyer of order. </param>
        public Order(Client buyer)
        {
            Items = new ObservableCollection<Item>();
            Buyer = buyer;
            Number = currentNumber;
        }

        /// <summary>
        /// Short info to display.
        /// </summary>
        [JsonIgnore]
        public string ShortInfo => $"Order №{Number} {Status}";

        /// <summary>
        /// String of order.
        /// </summary>
        /// <returns> Order in string. </returns>
        public override string ToString() => string.Join(Environment.NewLine + Environment.NewLine +
                                                         "Consists of: " + Environment.NewLine,
            $"Order {Number},{Environment.NewLine}created at {Date},{Environment.NewLine}" +
            $"{((Status&Status.Paid)!=0 ? "Was" : "Was not")} paid",
            string.Join(Environment.NewLine, Items));

        /// <summary>
        /// Long info to display.
        /// </summary>
        public string LongInfo => string.Join(Environment.NewLine + Environment.NewLine +
                                              "Consists of: " + Environment.NewLine,
            $"Order {Number},{Environment.NewLine}created at {Date},{Environment.NewLine}" +
            $"has status: {Status} ", string.Join(Environment.NewLine, Items));
    }
}
