using System;
using System.IO;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace MainApplication.Models
{
    /// <summary>
    /// Model of good == item in folder.
    /// </summary>
    internal class Item : ViewModels.Base.ViewModelBase
    {
        private string name;

        /// <summary>
        /// Name of good.
        /// </summary>
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private int article;

        /// <summary>
        /// Article of good.
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
        /// Quantity of good.
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
        /// Price of good.
        /// </summary>
        public double Price
        {
            get => price;
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
        /// Description of good.
        /// </summary>
        [Ignore]
        public string Description
        {
            get => description;
            set => Set(ref description, value);

        }

        /// <summary>
        /// Name of picture of good.
        /// </summary>
        public string PictureName
        {
            get => picture;
            set => Picture = value;
        }
        private string picture;

        /// <summary>
        /// Source to the picture.
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
        /// Minimum quantity to be in warehouse.
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
        /// Path in tree.
        /// </summary>
        [JsonIgnore]
        public string Path { get; set; }
    }
}
