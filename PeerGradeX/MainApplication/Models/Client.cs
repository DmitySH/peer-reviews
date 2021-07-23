using System.Collections.Generic;
using System.Linq;

namespace MainApplication.Models
{
    /// <summary>
    /// Type of client.
    /// </summary>
    internal enum UserType
    {
        Buyer,
        Seller
    }

    /// <summary>
    /// Model of client.
    /// </summary>
    internal class Client
    {
        // Lists with users of an app.
        public static List<Client> Buyers = new List<Client>();
        public static List<Client> Sellers = new List<Client>();

        /// <summary>
        /// Telephone number.
        /// </summary>
        public string TelNumber { get; }

        /// <summary>
        /// Physical address.
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Type of user.
        /// </summary>
        public UserType UserType { get; }

        /// <summary>
        /// Name of client.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Second name of client.
        /// </summary>
        public string SurName { get; }

        /// <summary>
        /// Patronymic of client.
        /// </summary>
        public string Patronymic { get; }

        /// <summary>
        /// Unique email of client.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Hashed password of client.
        /// </summary>
        public string Password { get; }
        
        /// <summary>
        /// Total sum of this package of items.
        /// </summary>
        public string TotalPaid => $"{Email}:" +
                                    $" {Order.Orders.Where(order => order.Buyer.Equals(this) && (order.Status & Status.Paid) != 0).Sum(x => x.Items.Sum(item => item.TotalPrice))}" +
                                    $" paid";

        /// <summary>
        /// Constructor of client.
        /// </summary>
        /// <param name="name"> Name. </param>
        /// <param name="surName"> Surname. </param>
        /// <param name="patronymic"> Patronymic. </param>
        /// <param name="email"> Email (unique). </param>
        /// <param name="password"> Password (hashed). </param>
        /// <param name="telNumber"> Telephone number. </param>
        /// <param name="address"> Physical address. </param>
        /// <param name="userType"> Type of user. </param>
        public Client(string name, string surName, string patronymic, string email, string password,
            string telNumber, string address, UserType userType)
        {
            Name = name;
            SurName = surName;
            Patronymic = patronymic;
            Email = email;
            Password = password;
            UserType = userType;
            TelNumber = telNumber;
            Address = address;
        }

        /// <summary>
        /// Equals clients.
        /// </summary>
        /// <param name="client"> Client. </param>
        /// <returns> True if they are equal. </returns>
        public bool Equals(Client client) => this.Email.Equals(client.Email);
    }
}
