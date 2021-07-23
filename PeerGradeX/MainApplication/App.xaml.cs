using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using MainApplication.Models;
using Newtonsoft.Json;

namespace MainApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Saves data.
        /// </summary>
        private void App_OnExit(object sender, ExitEventArgs e)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer() { ReferenceLoopHandling = ReferenceLoopHandling.Error };
                using (StreamWriter sw = new StreamWriter($"userData{Path.DirectorySeparatorChar}buyers.json"))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, Client.Buyers);
                    }
                }

                using (StreamWriter sw = new StreamWriter($"userData{Path.DirectorySeparatorChar}sellers.json"))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, Client.Sellers);
                    }
                }

                using (StreamWriter sw = new StreamWriter($"userData{Path.DirectorySeparatorChar}orders.json"))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, Order.Orders);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not save user data");
            }

            try
            {
                using (StreamWriter sw = new StreamWriter($"userData{Path.DirectorySeparatorChar}settings.txt"))
                {
                    sw.WriteLine(Order.currentNumber);
                    sw.WriteLine(Settings.CatalogNumber);
                    sw.WriteLine(Settings.ItemNumber);
                    sw.WriteLine(Settings.FolderNumber);
                    sw.WriteLine(Settings.MinPayed);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not save settings");
            }
        }

        /// <summary>
        /// Loads data.
        /// </summary>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sw = new StreamReader($"userData{Path.DirectorySeparatorChar}buyers.json"))
                {
                    using (JsonReader reader = new JsonTextReader(sw))
                    {
                        Client.Buyers = (List<Client>)serializer.Deserialize(reader, typeof(List<Client>));
                    }
                }

                using (StreamReader sw = new StreamReader($"userData{Path.DirectorySeparatorChar}sellers.json"))
                {
                    using (JsonReader reader = new JsonTextReader(sw))
                    {
                        Client.Sellers = (List<Client>)serializer.Deserialize(reader, typeof(List<Client>));
                    }
                }

                using (StreamReader sw = new StreamReader($"userData{Path.DirectorySeparatorChar}orders.json"))
                {
                    using (JsonReader reader = new JsonTextReader(sw))
                    {
                        Order.Orders = (ObservableCollection<Order>)serializer.Deserialize(reader, typeof(ObservableCollection<Order>));
                    }
                }
            }
            catch (Exception)
            {
                Client.Sellers = new List<Client>();
                Client.Buyers = new List<Client>();
                Order.Orders = new ObservableCollection<Order>();
                MessageBox.Show("Can not load user data");
            }

            try
            {
                using (StreamReader sr = new StreamReader($"userData{Path.DirectorySeparatorChar}settings.txt", Encoding.UTF8))
                {
                    Order.currentNumber = uint.Parse(sr.ReadLine());
                    Settings.CatalogNumber = int.Parse(sr.ReadLine());
                    Settings.ItemNumber = int.Parse(sr.ReadLine());
                    Settings.FolderNumber = int.Parse(sr.ReadLine());
                    Settings.MinPayed = int.Parse(sr.ReadLine());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not load settings");
            }
        }
    }
}
