using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static PeerGrade4.Program;

namespace PeerGrade4
{
    class Storage
    {
        private static Storage instance;
        private int size;
        public double Cost { get; }
        public List<Container> Containers { get; }
        private int index = 0;
        
        /// <summary>
        /// Showing current containers in a storage.
        /// </summary>
        public void ShowContainers()
        {
            if (Containers.Count == 0)
            {
                PrintColor("Склад пуст.", ConsoleColor.DarkRed);
            }
            else
            {
                for (int i = 0; i < Containers.Count; i++)
                {
                    Console.WriteLine($"Контейнер №{i + 1}: ");
                    Console.WriteLine($"Количество ящиков: {Containers[i].Size}");
                    Console.WriteLine($"Общий вес: {Containers[i].GetContainerWeight()}");
                    Console.WriteLine($"Реальная стоимость: {Math.Round(Containers[i].GetContainerPrice(), 3)}");
                    Console.WriteLine("-----------------------------------------------");
                }
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Writes all info about containers to the file.
        /// </summary>
        public void WriteWholeStorage()
        {
            string res = string.Empty;
            if (Containers.Count == 0)
            {
                res+="Склад пуст.";
            }
            else
            {
                for (int i = 0; i < Containers.Count; i++)
                {
                    res += $"Контейнер №{i + 1}: " + Environment.NewLine + $"Количество ящиков: {Containers[i].Size}" +
                           Environment.NewLine +
                           $"Общий вес: {Containers[i].GetContainerWeight()}" + Environment.NewLine +
                           $"Реальная стоимость: {Math.Round(Containers[i].GetContainerPrice(), 3)}" +
                           Environment.NewLine + "-----------------------------------------------";
                    res += Environment.NewLine;
                }
            }

            File.WriteAllText("YourWarehouse.txt",res);
            PrintColor("Информация записана в файл.",ConsoleColor.Green);
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Showing info about the storage.
        /// </summary>
        public void ShowInfo()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Размер склада: {size}");
            Console.WriteLine($"Цена хранения одного контейнера: {Cost}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Removing all containers from storage.
        /// </summary>
        public void ClearContainres()
        {
            if (Containers.Count == 0)
            {
                PrintColor("Склад пуст.", ConsoleColor.DarkRed);
            }
            else
            {
                Containers.Clear();
                PrintColor("Склад успешно очищен.", ConsoleColor.Green);
            }
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Adding container from file.
        /// </summary>
        /// <param name="size">Size of the container.</param>
        /// <param name="boxesList">List with info about boxes from file.</param>
        public void AddContainerFile(int size, List<string> boxesList)
        {
            Container container = new Container(size);

            PrintColor($"Максимальная масса для данного контейнера: {container.MaxMass}", ConsoleColor.DarkYellow);
            PrintColor($"Контейнер поврежден на {container.Damage}", ConsoleColor.Yellow);
            Console.WriteLine();

            //Adding box then removing it from a list of boxes to fill container.
            for (int i = 0; i < container.Size; i++)
            {
                container.AddBoxFile(boxesList[0]);
                boxesList.RemoveAt(0);
            }

            //Checking weight and price of the container.
            if (container.GetContainerPrice() >= Cost)
            {
                if (container.GetContainerWeight() <= container.MaxMass)
                {
                    if (Containers.Count == Containers.Capacity)
                    {
                        Containers[index] = container;
                        index = (index + 1) % Containers.Capacity;
                    }
                    else
                    {
                        index = 0;
                        Containers.Add(container);
                    }
                    PrintColor("Контейнер добавлен на склад.", ConsoleColor.Green);
                }
                else
                {
                    PrintColor("Масса ящиков превысила максимально допустимую для данного контейнера, " +
                               "поэтому он не будет помещен на склад.", ConsoleColor.Red);
                }
            }
            else
            {
                PrintColor("Контейнер не окупился и не будет помещен на склад.", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Adding container from console.
        /// </summary>
        public void AddContainer()
        {
            Container container = new Container(ParsePositiveInt("Введите положительное целое " +
                                                                 "число - количество ящиков в контейнере: "));
            PrintColor($"Максимальная масса для данного контейнера: {container.MaxMass}", ConsoleColor.DarkYellow);
            PrintColor($"Контейнер поврежден на {container.Damage}", ConsoleColor.Yellow);
            Console.WriteLine($"Добавьте количество ящиков равное {container.Size} в контейнер.");
            Console.WriteLine();

            for (int i = 0; i < container.Size; i++)
            {
                container.AddBox();
            }
            //Checking weight and price of the container.
            if (container.GetContainerPrice() > Cost)
            {
                if (container.GetContainerWeight() <= container.MaxMass)
                {
                    if (Containers.Count == Containers.Capacity)
                    {
                        Containers[index] = container;
                        index = (index + 1) % Containers.Capacity;
                    }
                    else
                    {
                        Containers.Add(container);
                    }
                    PrintColor("Контейнер добавлен на склад.", ConsoleColor.Green);
                }
                else
                {
                    PrintColor("Масса ящиков превысила максимально допустимую для данного контейнера, " +
                               "поэтому он не будет помещен на склад.", ConsoleColor.Red);
                }
            }
            else
            {
                PrintColor("Контейнер не окупился и не будет помещен на склад.", ConsoleColor.Red);
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Removing container from file.
        /// </summary>
        /// <param name="index">Index of a container to remove.</param>
        public void RemoveContainerFile(int index)
        {
            if (index - 1 < 0 || index - 1 >= Containers.Count)
            {
                PrintColor("Некорректный номер контейнера при удалении.", ConsoleColor.Red);
                return;
            }

            PrintColor($"Контейнер {index} удален.", ConsoleColor.DarkGreen);
            Containers.RemoveAt(index - 1);
        }

        /// <summary>
        /// Removing container from console.
        /// </summary>
        public void RemoveContainer()
        {
            if (Containers.Count == 0)
            {
                PrintColor("Склад пуст.", ConsoleColor.DarkRed);
            }
            else
            {
                //Showing current containers.
                string[] toWrite = new string[Containers.Count];
                for (int i = 0; i < Containers.Count; i++)
                {
                    toWrite[i] = $"Контейнер №{i + 1}";
                }

                int toRemove = 0;
                //Waiting for user's choice what to remove.
                try
                {
                    ChooseElement(toRemove, toWrite);
                }
                catch (Exception ex)
                {
                    toRemove = int.Parse(ex.Message);
                }

                if (toRemove < Containers.Count)
                {
                    Containers.RemoveAt(toRemove);
                    PrintColor("Контейнер успешно удален со склада.", ConsoleColor.Green);
                }
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Finds the most expensive and the cheapest containers.
        /// </summary>
        public void FindMostWeight()
        {
            if (Containers.Count == 0)
            {
                PrintColor("Склад пуст.", ConsoleColor.DarkRed);
                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
                return;
            }

            //LINQ query.
            double max = Containers.Max(x => x.GetContainerWeight());
            double min = Containers.Min(x => x.GetContainerWeight());

            for (int i = 0; i < Containers.Count; i++)
            {
                if (max == Containers[i].GetContainerWeight())
                {
                    PrintColor($"Самый тяжелый контейнер под номером {i + 1}, " +
                               $"его вес = {Math.Round(Containers[i].GetContainerWeight(), 3)}", ConsoleColor.DarkRed);
                }
                if (min == Containers[i].GetContainerWeight())
                {
                    PrintColor($"Самый легкий контейнер под номером {i + 1}, " +
                               $"его вес = {Math.Round(Containers[i].GetContainerWeight(), 3)}", ConsoleColor.DarkGreen);
                }

                if (min == Containers[i].GetContainerWeight() && Containers[i].GetContainerWeight() == max)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Finds the most massive and the less massive containers.
        /// </summary>
        public void FindMostPrice()
        {
            if (Containers.Count == 0)
            {
                PrintColor("Склад пуст.", ConsoleColor.DarkRed);
                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
                return;
            }

            //LINQ queries.
            double max = Containers.Max(x => x.GetContainerPrice());
            double min = Containers.Min(x => x.GetContainerPrice());

            for (int i = 0; i < Containers.Count; i++)
            {
                if (max == Containers[i].GetContainerPrice())
                {
                    PrintColor($"Самый дорогой контейнер под номером {i + 1}, " +
                               $"его цена = {Math.Round(Containers[i].GetContainerPrice(), 3)}", ConsoleColor.DarkGreen);
                }
                if (min == Containers[i].GetContainerPrice())
                {
                    PrintColor($"Самый дешевый контейнер под номером {i + 1}, " +
                               $"его цена = {Math.Round(Containers[i].GetContainerPrice(), 3)}", ConsoleColor.DarkRed);
                }
                if (min == Containers[i].GetContainerPrice() && Containers[i].GetContainerPrice() == max)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Protected constructor to realise singleton.
        /// </summary>
        /// <param name="size">Size of the storage.</param>
        /// <param name="price">Minimal price of each container.</param>
        protected Storage(int size, double price)
        {
            this.size = size;
            this.Cost = price;
            Containers = new List<Container>(size);
        }

        /// <summary>
        /// Creating instance of a storage. Reaslised for singleton.
        /// </summary>
        /// <param name="size">Size of the storage.</param>
        /// <param name="price">Minimal price of each container.</param>
        /// <returns></returns>
        public static Storage GetInstance(int size, double price)
        {
            return instance ??= new Storage(size, price);
        }
    }
}
