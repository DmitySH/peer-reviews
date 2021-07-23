using System;
using System.Collections.Generic;
using System.IO;

namespace PeerGrade4
{
    class Program
    {
        public delegate void VoidDel();

        /// <summary>
        /// Method for "arrows" control in a menu.
        /// </summary>
        /// <param name="begin">Number of current element in a menu.</param>
        /// <param name="toWrite">Array with names of actions in menu.</param>
        /// <param name="delegs">Array with delegates for each action of menu.</param>
        public static void ChooseAction(int begin, string[] toWrite, VoidDel[] delegs)
        {
            Console.Clear();
            for (int i = 0; i < toWrite.Length; i++)
            {
                if (i != begin)
                {
                    Console.WriteLine(" " + toWrite[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("> " + toWrite[begin]);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
            }

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    ChooseAction((toWrite.Length - 1 + begin) % toWrite.Length, toWrite, delegs);
                    break;

                case ConsoleKey.DownArrow:
                    ChooseAction((begin + 1) % toWrite.Length, toWrite, delegs);
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    //Choosing delegate.
                    delegs[begin]();
                    break;

                default:
                    ChooseAction(begin, toWrite, delegs);
                    break;
            }
        }

        /// <summary>
        /// Method to choose element of an array with "arrows" control.
        /// </summary>
        /// <param name="begin">Number of current element in a menu.</param>
        /// <param name="toWrite">Array with names of elements in menu.</param>
        public static void ChooseElement(int begin, string[] toWrite)
        {
            Console.Clear();
            for (int i = 0; i < toWrite.Length; i++)
            {
                if (i != begin)
                {
                    Console.WriteLine(" " + toWrite[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("> " + toWrite[begin]);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
            }

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    ChooseElement((toWrite.Length - 1 + begin) % toWrite.Length, toWrite);
                    break;

                case ConsoleKey.DownArrow:
                    ChooseElement((begin + 1) % toWrite.Length, toWrite);
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    //When user chooses an element i close the recursion by throwing an exception
                    //then program catches it in a place where program should be continued. It's very specific, isn't it?
                    throw new FoundException(begin.ToString());

                default:
                    ChooseElement(begin, toWrite);
                    break;
            }
        }

        /// <summary>
        /// Safe parse of a positive integer.
        /// </summary>
        /// <param name="prompt">String with help (what to enter).</param>
        /// <returns>Positive integer.</returns>
        public static int ParsePositiveInt(string prompt)
        {
            int num;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out num) || num <= 0);

            return num;
        }

        /// <summary>
        /// Safe parse of a positive double.
        /// </summary>
        /// <param name="prompt">String with help (what to enter).</param>
        /// <returns>Positive double.</returns>
        public static double ParsePositiveDouble(string prompt)
        {
            double num;
            do
            {
                Console.Write(prompt);
            } while (!double.TryParse(Console.ReadLine(), out num) || num <= 0);

            return num;
        }

        /// <summary>
        /// Creating storage.
        /// </summary>
        /// <returns>Storage - object.</returns>
        static Storage CreateStorage()
        {
            int size = ParsePositiveInt("Введите положительное целое число - размер вашего склада: ");
            double price = ParsePositiveDouble("Введите положительное вещественное число - размер "
                                               + "платы за хранение одного контейнера: ");
            Storage warehouse = Storage.GetInstance(size, price);
            PrintColor($"----------------------------------------------------" + Environment.NewLine +
                       $"Склад размера {warehouse.Containers.Capacity} и ценой хранения " +
                       $"{warehouse.Cost} успешно создан!" + Environment.NewLine +
                       $"----------------------------------------------------", ConsoleColor.Green);
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
            return Storage.GetInstance(size, price);
        }

        /// <summary>
        /// Creating storage from file.
        /// </summary>
        /// <param name="size">Size of a storage.</param>
        /// <param name="price">Price of a storage.</param>
        /// <returns>Storage - object.</returns>
        static Storage CreateStorageFile(int size, double price)
        {
            Storage warehouse = Storage.GetInstance(size, price);
            PrintColor($"----------------------------------------------------" + Environment.NewLine +
                       $"Склад размера {warehouse.Containers.Capacity} и ценой хранения " +
                       $"{warehouse.Cost} успешно создан!" + Environment.NewLine +
                       $"----------------------------------------------------", ConsoleColor.Green);
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
            return Storage.GetInstance(size, price);
        }

        /// <summary>
        /// Specific way of closing cycle.
        /// </summary>
        static void Exit()
        {
            //Throwing exception to catch it and break the cycle.
            throw new ApplicationException();
        }

        /// <summary>
        /// Menu for console way of working.
        /// </summary>
        static void Menu()
        {
            Storage warehouse = CreateStorage();
            string[] toWrite = { "Добаваить контейнер.", "Удалить контейнер.",
                "Посмотреть список текущих контейнеров на складе.","Посмотреть информацию о складе.",
                "Очистить склад.", "Найти самый дорогой и самый дешевый контейнеры.","Найти самый тяжелый и самый легкий контейнеры.",
                "Записать данные о текущем складе в файл.","Закончить обслуживание склада." };
            VoidDel[] delsAr = { warehouse.AddContainer, warehouse.RemoveContainer, warehouse.ShowContainers,
                warehouse.ShowInfo, warehouse.ClearContainres,warehouse.FindMostPrice,warehouse.FindMostWeight, warehouse.WriteWholeStorage,Exit };

            do
            {
                try
                {
                    ChooseAction(0, toWrite, delsAr);
                }
                catch (ApplicationException)
                {
                    //Where we catch exception from Exit method.
                    break;
                }
                catch (Exception)
                {
                    //This part I hope is unreachable. Just to be sure.
                    Console.WriteLine("Невозможно. Нажмите любую клавишу.");
                    Console.ReadKey();
                }
            } while (true);
            //Yes, true-cycle. That was the best way to realise my arrows menu, but it's very specific.
        }

        /// <summary>
        /// Showing information about this application.
        /// </summary>
        static void PrintInfo()
        {
            Console.WriteLine(" Добро пожаловать в приложение по управлению складом овощей!");
            Console.WriteLine(" Управление программой - стрелочки вверх-вниз и enter для выбора.");
            Console.WriteLine(" Прежде всего выбирается метод работы (консоль или файлы).");
            Console.WriteLine(" Для начала необходимо создать склад, после чего можно будет им манипулировать.");
            Console.WriteLine(@" Запись происходит в файл ""YourWarehouse.txt""");
            Console.WriteLine();
            Console.WriteLine(" Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Choosing console or file way of working.
        /// </summary>
        static void InputWay()
        {
            Console.WriteLine("Выберите способ работы со складом.");
            string[] toWrite = { "Ввод из консоли.", "Ввод из файла." };
            VoidDel[] delsAr = { Menu, FileMenu };
            ChooseAction(0, toWrite, delsAr);
        }

        /// <summary>
        /// Enter point.
        /// </summary>
        static void Main()
        {
            PrintInfo();
            Console.ForegroundColor = ConsoleColor.White;
            InputWay();
        }

        /// <summary>
        /// Recoloring the output text.
        /// </summary>
        /// <param name="print">What to print.</param>
        /// <param name="clr">Color for output text.</param>
        public static void PrintColor(string print, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine(print);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Reading storage from file.
        /// </summary>
        /// <param name="size">Size of a storage.</param>
        /// <param name="cost">Cost of a container in a storage.</param>
        public static void ReadStorage(out int size, out double cost)
        {
            string[] fileText = File.ReadAllLines("Storage.txt");
            string[] splittedSize = fileText[1].Split(' ');
            string[] splittedCost = fileText[2].Split(' ');
            if (!int.TryParse(splittedSize[1], out size) || size <= 0 ||
                !double.TryParse(splittedCost[1], out cost) || cost <= 0)
            {
                throw new FileException();
            }
        }

        /// <summary>
        /// One more way to use "arrows" menu.
        /// </summary>
        /// <param name="toDo">Number of current action.</param>
        /// <param name="warehouse">Storage - object.</param>
        static void ChooseActionFiles(int toDo, Storage warehouse)
        {
            switch (toDo)
            {
                case 0:
                    FileActions(warehouse);
                    break;

                case 1:
                    warehouse.ShowInfo();
                    break;

                case 2:
                    warehouse.ShowContainers();
                    break;

                case 3:
                    warehouse.FindMostPrice();
                    break;

                case 4:
                    warehouse.FindMostWeight();
                    break;

                case 5:
                    warehouse.WriteWholeStorage();
                    break;
                case 6:
                    //To brake the cycle.
                    throw new ApplicationException();
            }
        }

        /// <summary>
        /// Array to list converter.
        /// </summary>
        /// <param name="ar">Array to convert.</param>
        /// <returns>List with elements of array.</returns>
        private static List<string> ConvertToList(string[] ar)
        {
            List<string> list = new List<string>();
            foreach (var x in ar)
            {
                list.Add(x);
            }

            return list;
        }

        /// <summary>
        /// Reading actions from file.
        /// </summary>
        /// <param name="warehouse">Storage - object.</param>
        static void FileActions(Storage warehouse)
        {
            string[] actions = File.ReadAllLines("Actions.txt");
            string[] boxes = File.ReadAllLines("Boxes.txt");
            List<string> boxesList = ConvertToList(boxes);
            boxesList.RemoveAt(0);
            boxesList.RemoveAt(0);
            for (int i = 1; i < actions.Length; i++)
            {
                string[] splitted = actions[i].Split(' ');
                if (splitted[0].ToLower() == "remove")
                {
                    warehouse.RemoveContainerFile(int.Parse(splitted[1]));
                }
                else if (splitted[0].ToLower() == "add")
                {
                    warehouse.AddContainerFile(int.Parse(splitted[1]), boxesList);
                }
                else
                {
                    PrintColor("Некорректная команда.", ConsoleColor.DarkRed);
                }
                Console.WriteLine();
                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Shows how to fill files correctly.
        /// </summary>
        static void ShowRulesForFiles()
        {
            Console.WriteLine(" Все файлы расположены в папке с исполняемым модулем.");
            Console.WriteLine(@" На этом этапе впишите информацию о складе в файл ""Storage.txt"" (после двоеточия через пробел)");
            Console.WriteLine(@" Далее необходимо вписывать дейтвия в файл ""Actions.txt"" и информацию о ящиках в файл ""Boxes.txt""");
            Console.WriteLine(" Действия записываются так:");
            Console.WriteLine(" На каждой строке, начиная со второй, записана одна из команд. Все команды выполняются последовательно.");
            Console.WriteLine(" Для добавления контейнера напишите: add 'size', где size-размер контейнера.");
            Console.WriteLine(" Для удаления контейнера напишите: remove 'index', где index-номер контейнера на складе(текущем).");
            Console.WriteLine(" Информация о контейнерах записывается так:");
            Console.WriteLine(" На каждой строке, начиная с третьей, 2 числа-информация об одном ящике:" + Environment.NewLine +
                              " первое-вес ящика в кг, второе-цена одного кг.");
            Console.WriteLine(" Пример записи уже есть в данных файлах.");
            Console.WriteLine(" Все файлы, кроме информации о складе, можно перезаписвать, чтобы продолжать работать со складом.");
            Console.WriteLine();
            Console.WriteLine(" Когда будете готовы нажмите любую клавишу.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Menu for file way of working.
        /// </summary>
        static void FileMenu()
        {
            int size=0;
            double cost=0;
            bool flag;
            do
            {
                try
                {
                    flag = false;
                    ShowRulesForFiles();
                    ReadStorage(out size, out cost);
                }
                catch (Exception)
                {
                    flag = true;
                    PrintColor("Ввод склада из файла был некорректным.", ConsoleColor.Red);
                    Console.WriteLine("Для продолжения нажмите любую клавишу.");
                    Console.ReadKey();
                }
            } while (flag);
            
            Storage warehouse = CreateStorageFile(size, cost);
            do
            {
                try
                {
                    string[] toWrite = { "Считать данные о контейнерах и действиях из файлов.","Посмотреть информацию о складе",
                        "Заглянуть на склад." ,"Найти самый дорогой и самый дешевый контейнеры.","Найти самый тяжелый и самый легкий контейнеры.",
                        "Записать данные о текущем складе в файл.","Закончить обслуживание склада."};
                    int todo = 0;
                    try
                    {
                        ChooseElement(todo, toWrite);
                    }
                    catch (FoundException ex)
                    {
                        todo = int.Parse(ex.Message);
                    }
                    ChooseActionFiles(todo, warehouse);
                }
                catch (FileException ex)
                {
                    PrintColor(ex.Message, ConsoleColor.Red);
                    Console.WriteLine("Нажмите любую клавишу.");
                    Console.ReadKey();
                }
                catch (ApplicationException)
                {
                    break;
                }
                catch (Exception)
                {
                    PrintColor("Некорректный формат ввода из файла.", ConsoleColor.Red);
                    Console.WriteLine("Нажмите любую клавишу.");
                    Console.ReadKey();
                }
            } while (true);
            //I know that I use try-catch too much but it is necessary for me because this is the most
            //convenient way to change place of a control in program. 
        }
    }
}
//P.S. Ввод с файла весьма специфичен: это некое подобие рюкзака, то есть мы вводим размер контейненров,
//а потом последовательно кидаем в него ящики, если ящиков меньше, какой-то контейнер будет незаполнен и не создастся,
//больше - ничего страшного, они просто никуда не пойдут.
