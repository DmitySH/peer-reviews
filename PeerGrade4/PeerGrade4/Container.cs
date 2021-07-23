using System;
using System.Collections.Generic;
using static PeerGrade4.Program;
namespace PeerGrade4
{
    class Container
    {
        private Random rnd = new Random();
        public List<Box> Boxes { get; }
        public int Size { get; }
        public double Damage { get; }
        public int MaxMass { get; }
        private double price;
        private double weight;

        /// <summary>
        /// Get real price of a container.
        /// </summary>
        /// <returns>Real price of a container.</returns>
        public double GetContainerPrice()
        {
            //Real price of a container is sum of prices of boxes without damaged part.
            price = 0;
            foreach (var box in Boxes)
            {
                price += box.FullPrice;
            }

            return price * (1 - Damage);
        }

        /// <summary>
        /// Get full weight of a container.
        /// </summary>
        /// <returns>Weight of a container.</returns>
        public double GetContainerWeight()
        {
            //Full price of a container is sum of weights of each box.
            weight = 0;
            foreach (var box in Boxes)
            {
                weight += box.Weight;
            }

            return weight;
        }

        /// <summary>
        /// Adding box to the container.
        /// </summary>
        public void AddBox()
        {
            //All information about a box is given to contstructor.
            Box box = new Box(ParsePositiveDouble("Введите положительное вещественное число - вес ящика: "),
                ParsePositiveDouble("Введите положительное вещественное число - цену за один килограмм: "));
            PrintColor($"Ящик стоимостью {Math.Round(box.FullPrice, 3)} добавлен в контейнер.", ConsoleColor.DarkGreen);
            Console.WriteLine("---------------------------------------------------------");
            Boxes.Add(box);
        }

        /// <summary>
        /// Adding box to the container form file.
        /// </summary>
        /// <param name="boxInfo">String with an info about each box.</param>
        public void AddBoxFile(string boxInfo)
        {
            //First number in a string is a weight, second is a price for kg.
            string[] splitted = boxInfo.Split(' ');
            Box box = new Box(int.Parse(splitted[0]),double.Parse(splitted[1]));
            PrintColor($"Ящик стоимостью {Math.Round(box.FullPrice, 3)} добавлен в контейнер.", ConsoleColor.DarkGreen);
            Console.WriteLine("---------------------------------------------------------");
            Boxes.Add(box);
        }

        /// <summary>
        /// Container's constructor.
        /// </summary>
        /// <param name="size"></param>
        public Container(int size)
        {
            MaxMass = rnd.Next(50, 1001);
            this.Size = size;
            Damage = Math.Round(rnd.NextDouble() % 0.5, 3);
            Boxes = new List<Box>();
        }
    }
}
