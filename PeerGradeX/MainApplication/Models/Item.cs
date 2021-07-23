using System;
using System.IO;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace MainApplication.Models
{
    /// <summary>
    /// Model of good == item in folder.
    /// </summary>
    internal class Item : ViewModels.Base.ViewModelBase, ICloneable
    {
        /// <summary>
        /// Sum of price of items' package.
        /// </summary>
        [JsonIgnore]
        public double TotalPrice => Math.Round(Quantity * Price, 2);

        private int toBuy;

        /// <summary>
        /// How much to buy.
        /// </summary>
        [JsonIgnore]
        public int ToBuy
        {
            get => toBuy;
            set => Set(ref toBuy, value);
        }

        private string name;

        /// <summary>
        /// Name of item.
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private int article;

        /// <summary>
        /// Article of item.
        /// </summary>
        public int Article
        {
            get => article;
            set
            {
                if (value < 0)
                {
                    Set(ref article, 0);
                }
                else
                {
                    Set(ref article, value);
                }
            }
        }

        private int quantity;

        /// <summary>
        /// Quantity of item.
        /// </summary>
        public int Quantity
        {
            get => quantity;
            set
            {
                if (value < 0)
                {
                    Set(ref quantity, 0);
                }
                else
                {
                    Set(ref quantity, value);
                }
            }
        }

        private double price;

        /// <summary>
        /// Price of item.
        /// </summary>
        public double Price
        {
            get => Math.Round(price, 2);
            set
            {
                if (value < 0)
                {
                    Set(ref price, 0);
                }
                else
                {
                    Set(ref price, value);
                }
            }
        }

        private string description;

        /// <summary>
        /// Item's description.
        /// </summary>
        [Ignore]
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        /// <summary>
        /// Name of picture of item.
        /// </summary>
        public string PictureName
        {
            get => picture;
            set => Picture = value;
        }

        private string picture;

        /// <summary>
        /// Path to picture.
        /// </summary>
        [Ignore]
        [JsonIgnore]
        public string Picture
        {
            get
            {
                try
                {
                    return Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "savedPictures" + picture;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set => Set(ref picture, value);
        }

        private double minQuantity;

        /// <summary>
        /// Minimal quantity for report.
        /// </summary>
        [Ignore]
        public double MinQuantity
        {
            get => minQuantity;
            set
            {
                if (value < 0)
                {
                    Set(ref minQuantity, 0);
                }
                else
                {
                    Set(ref minQuantity, value);
                }
            }
        }

        /// <summary>
        /// Path to item.
        /// </summary>
        [JsonIgnore]
        public string Path { get; set; }

        /// <summary>
        /// Clones items.
        /// </summary>
        /// <returns> Cloned item. </returns>
        public object Clone() => new Item()
        {
            Article = this.Article, Description = this.Description, Name = this.Name,
            Quantity = this.ToBuy, Price = this.Price, PictureName = this.PictureName
        };

        /// <summary>
        /// Equals for items.
        /// </summary>
        /// <param name="item"> Other item. </param>
        /// <returns> True if equal. </returns>
        public bool Equals(Item item) =>
            item.Name.Equals(Name) && item.Price.Equals(Price) && item.Article.Equals(Article);

        /// <summary>
        /// How to display item.
        /// </summary>
        /// <returns> Item in string. </returns>
        public override string ToString() =>
            $"{Name} ({Article}) in quantity {Quantity},{Environment.NewLine}{Price} for one, {TotalPrice} in total{Environment.NewLine}";
    }
}
