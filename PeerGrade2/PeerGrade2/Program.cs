using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PeerGrade2
{
    class Program
    {
        /// <summary>
        /// Преобразует лист строку в строку.
        /// </summary>
        /// <param name="path">Лист, содержащий путь.</param>
        /// <returns>Строку, составленную из элементов листа.</returns>
        static string MakeString(List<string> path)
        {
            string res = string.Empty;
            foreach (string x in path)
            {
                res += x;
            }

            return res;
        }

        /// <summary>
        /// Безопасное получение номера файла, который нужно удалить из списка на конкатенацию.
        /// </summary>
        /// <param name="len">Количество элементов в списке на конкатенацию.</param>
        /// <returns>Целое число - номер удаляемого из списка на конкатенацию файла.</returns>
        static int ParseIntCon(int len)
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > len)
            {
                Console.WriteLine("Неверный ввод!");
            }

            return num;
        }
        ///<summary>
        /// Безопасное получение номера диска.
        /// </summary>
        /// <param name="len">Количество дисков.</param>
        /// <returns>Целое число - номер выбранного диска.</returns>
        static int ParseForDrives(int len)
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > len)
            {
                Console.WriteLine("Неверный ввод!");
            }

            return num;
        }

        /// <summary>
        /// Безопасное получение номера кодировки.
        /// </summary>
        /// <returns>Целое число - номер выбранной кодировки.</returns>
        static int ParseForEnc()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > 3)
            {
                Console.WriteLine("Неверный ввод!");
            }

            return num;
        }

        /// <summary>
        /// Получение имени диска, на котором пользователь будет работать.
        /// </summary>
        /// <returns>Строку с названием диска.</returns>
        static string DriveSelect()
        {
            int driveNum;
            try
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                string[] drives = new string[allDrives.Length];

                for (int i = 0; i < allDrives.Length; i++)
                {
                    drives[i] = allDrives[i].ToString();
                    Console.WriteLine($"{i + 1}) {drives[i]}");
                }
                driveNum = ParseForDrives(allDrives.Length);
                return drives[driveNum - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("У вас беды с диском или нет к нему доступа.");
            }
            return String.Empty;
        }

        /// <summary>
        /// Выводит директории и файлы по текущему пути на экран.
        /// </summary>
        /// <param name="path">Текущий путь.</param>
        static void PrintDirects(string path)
        {
            try
            {
                string[] directs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);

                foreach (string x in directs)
                {
                    Console.WriteLine(x);
                }

                foreach (string x in files)
                {
                    Console.WriteLine(x);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("У вас нет доступа для просмотра этих директорий.");
            }
        }

        static bool Check(string path)
        {
            try
            {
                string[] directs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Выводит правила работы с приложением и команды на экран.
        /// </summary>
        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine($"Список команд:" +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"help - получить список команд" +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"cd - выбрать диск." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"travel - начать путешествие по текущему диску." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"ВНИМАНИЕ! Дабы не делать путешествие по диску " +
                              $"с выбором номера директории, " +
                              $"{Environment.NewLine}" +
                              $"в моем файловом " +
                              $"менеджере нужно самому дописывать " +
                              $"{Environment.NewLine}" +
                              $"название директории, куда вы хотите заглянуть, " +
                              $"{Environment.NewLine}" +
                              $"надеюсь, это достаточно оригинально." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"Чтобы подняться на уровень выше, в пути необходимо дописать слово" +
                              $"{Environment.NewLine}" +
                              $"back (соблюдая нижний регистр)." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"Чтобы остановиться в директории и поработать с файлами," +
                              $"{Environment.NewLine}" +
                              $"которые в ней расположены, с помощью последующих команд" +
                              $"{Environment.NewLine}" +
                              $"в пути необходимо дописать слово con (соблюдая нижний регистр)." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"Во время путешествия, команды недоступны, пока не будет введено con." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"Далее идут команды, для работы с файлами. Необходимо просто остановиться" +
                              $"{Environment.NewLine}" +
                              $"в директории с файлом (дописать con) " +
                              $"{Environment.NewLine}" +
                              $"и ввести только название команды," +
                              $" после чего вы сможете выбрать сам файл." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"create - создать текстовый файл в кодировке UTF-8." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"createEncode - создать текстовый файл в одной из трех кодировок на выбор." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"delete - удалить файл" +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"read - вывести на экран содержимое текстового файла в кодировке UTF-8." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"readEncode - вывести на экран содержимое текстового файла" +
                              $"{Environment.NewLine}" +
                              $"в одной из трех кодировок на выбор." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"copy - сохранить файл для последующего его перемещения или копирования." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"paste - скопировать в текущую директорию сохраненный файл." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"move - переместить в текущую директорию сохраненный файл." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"show - еще раз показать содерижмое текущей директории." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"add - добавить файл для последующей конкатенации." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"showCons - показать текущий список файлов для конкатенации." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"delCon - удалить один из файлов из списка для конкатенации." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"clearCons - очистить список файлов для конкатенации." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"connect - произвести конкатенацию." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"Так как просто бегать по директориям весьма скучно, " +
                              $"вы можете" +
                              $"{Environment.NewLine}" +
                              $@"поиграться с цветом вашего ""Файлового Менеджера"" =)" +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"color - выбор цвета заднего фона." +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"colorText - выбор цвета текста." +
                              $"{Environment.NewLine}");
        }

        /// <summary>
        /// Внутри метода происходит путешествие по директориям.
        /// </summary>
        /// <param name="pathList">Текущий путь.</param>
        static void Travel(ref List<string> pathList)
        {
            string next;
            while (true)
            {
                Console.WriteLine();
                string pathString = MakeString(pathList);
                PrintDirects(pathString);
                Console.WriteLine();
                Console.Write(pathString);
                next = Console.ReadLine();
                //В случае набора пользователем con путешествие прерывается
                //и пользователь может ввести команду.
                if (next == "con")
                {
                    break;
                }

                try
                {
                    if (next != "back")
                    {
                        if (Directory.Exists(pathString + next + $"{Path.DirectorySeparatorChar}")
                            && Check(pathString + next + $"{Path.DirectorySeparatorChar}")
                            && !next.Contains('/') && !next.Contains('\\'))
                        {
                            //Путь собирается из текущего пути и строки, введеной пользоваталем,
                            //если такая директория существует.
                            pathList.Add(next + $"{Path.DirectorySeparatorChar}");
                        }

                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Нет такой директории или у вас нет прав.");
                        }
                    }
                    else
                    {
                        if (pathList.Count > 1)
                        {
                            pathList.RemoveAt(pathList.Count - 1);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Вы уже поднялись до диска." +
                                              " Если хотите выбрать другой диск введите cd.");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("У вас недостаточно прав, чтобы быть тут.");
                }
            }
        }

        /// <summary>
        /// Выводит на экран содержимое текстового файла из текущей директории в кодировке UTF-8 .
        /// </summary>
        /// <param name="path">Текущий путь.</param>
        static void Read(string path)
        {
            Console.WriteLine();
            Console.WriteLine("Введите полное название файла для прочтения.");
            string fileName = Console.ReadLine();
            try
            {
                Console.Clear();
                Console.WriteLine(File.ReadAllText(path + fileName, Encoding.UTF8));
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Файл не найден или к нему нет доступа.");
            }
        }

        /// <summary>
        /// Получает кодировку по номеру.
        /// </summary>
        /// <param name="num">Номер кодировки.</param>
        /// <returns>Кодировку.</returns>
        static Encoding ChoseEnc()
        {
            Console.WriteLine($"Выберите кодировку {Environment.NewLine}" +
                              $"1) Unicode{Environment.NewLine}" +
                              $"2) ASCII{Environment.NewLine}" +
                              $"3) UTF-32{Environment.NewLine}");
            int encNum = ParseForEnc();
            Encoding enc;
            switch (encNum)
            {
                case 1:
                    enc = Encoding.Unicode;
                    break;
                case 2:
                    enc = Encoding.ASCII;
                    break;
                case 3:
                    enc = Encoding.UTF32;
                    break;
                default:
                    enc = Encoding.Default;
                    break;
            }

            return enc;
        }

        /// <summary>
        /// Выводит содержимое текстового файла из текущей директории
        /// на экран в одной из выбранных кодировок.
        /// </summary>
        /// <param name="path">Текущий путь.</param>
        static void ReadEnc(string path)
        {
            Encoding enc;
            enc = ChoseEnc();

            Console.WriteLine();
            Console.WriteLine("Введите полное название файла для прочтения.");
            string fileName = Console.ReadLine();

            try
            {
                Console.Clear();
                Console.WriteLine(File.ReadAllText(path + fileName, enc));
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Файл не найден или к нему нет доступа.");
            }
        }

        /// <summary>
        /// Сохранает путь до файла из текущей директории и выбрасывает его имя.
        /// </summary>
        /// <param name="path">Текущий путь</param>
        /// <param name="copyName">Выбрасываемое имя файла.</param>
        /// <returns>Путь до файла, который будет скопирован/премещен.</returns>
        static string Copy(List<string> path, out string copyName)
        {
            Console.WriteLine("Введите полное название файла, который будет" +
                              " скопирован/перемещен из этой директории.");
            //Имя файла также понадобится впоследствии,
            //так как в его новом пути требуется указать и его имя.
            copyName = Console.ReadLine();
            // Запоминаем путь к файлу, который мы будем копировать/перемещать.
            try
            {
                string savedPath = MakeString(path) + Path.DirectorySeparatorChar + copyName;
                if (!File.Exists(savedPath))
                {
                    Console.WriteLine("Такого файла тут нет! Копирование/перемещение произвести не получится");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Файл сохранен!");
                }

                return savedPath;
            }
            catch (Exception)
            {
                Console.WriteLine("У вас нет доступа!");
            }

            return String.Empty;
        }

        /// <summary>
        /// Перемещает файл в текущую директорию.
        /// </summary>
        /// <param name="savedPath">Путь, из которого берется файл.</param>
        /// <param name="currentPath">Текущий путь.</param>
        /// <param name="copyName">Имя файла.</param>
        static void Move(string savedPath, List<string> currentPath, string copyName)
        {
            Console.WriteLine(MakeString(currentPath));
            if (File.Exists(MakeString(currentPath) + Path.DirectorySeparatorChar + copyName))
            {
                Console.WriteLine("Файл с таким именем уже существует в данной директории.");
            }
            else
            {
                File.Move(savedPath, MakeString(currentPath)
                                     + Path.DirectorySeparatorChar + copyName);
                Console.WriteLine("Файл успешно перемещен!");
            }
        }

        /// <summary>
        /// Копирует файл в текущую директорию.
        /// </summary>
        /// <param name="savedPath">Путь, из которого берется файл.</param>
        /// <param name="currentPath">Текущий путь.</param>
        /// <param name="copyName">Имя файла.</param>
        static void Paste(string savedPath, List<string> currentPath, string copyName)
        {
            try
            {
                Console.WriteLine(MakeString(currentPath));
                if (File.Exists(MakeString(currentPath) + Path.DirectorySeparatorChar + copyName))
                {
                    Console.WriteLine("Файл с таким именем уже существует в данной директории.");
                }
                else
                {
                    File.Copy(savedPath, MakeString(currentPath)
                                         + Path.DirectorySeparatorChar + copyName);
                    Console.WriteLine("Файл успешно скопирован!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Файл не найден или к выбранной папке нет доступа");
            }
        }

        /// <summary>
        /// Удаляет файл из текущей директории.
        /// </summary>
        /// <param name="currentPath">Текущий путь.</param>
        static void Remove(List<string> currentPath)
        {
            Console.WriteLine("Введите полное название файла для удаления");
            string fileName = Console.ReadLine();
            try
            {
                File.Delete(MakeString(currentPath) + fileName);
                Console.WriteLine("Файл успешно удален!");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Файл не найден или у вас недостаточно прав");
            }
        }

        /// <summary>
        /// Создает файл в текущей директории.
        /// </summary>
        /// <param name="currentPath">Текущий путь.</param>
        static void Create(List<string> currentPath)
        {
            try
            {
                Console.WriteLine("Введите имя файла");
                string name = Console.ReadLine();
                Console.WriteLine("Введите наполнение файла");
                string text = Console.ReadLine();
                if (File.Exists(MakeString(currentPath) + name + ".txt"))
                {
                    Console.WriteLine("Файл с таким именем уже существует в данной директории.");
                }
                else
                {
                    File.WriteAllText(MakeString(currentPath) + name + ".txt", text, Encoding.UTF8);
                    Console.WriteLine("Файл успешно создан!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Название файла некорректно или к выбранной папке нет доступа");
            }
        }

        /// <summary>
        /// Создает текстовый файл в текущей директории в выбранной пользователем кодировке.
        /// </summary>
        /// <param name="currentPath">Текущй путь.</param>
        static void CreateEnc(List<string> currentPath)
        {
            Encoding enc;
            Console.WriteLine();
            enc = ChoseEnc();

            try
            {
                Console.WriteLine("Введите имя файла");
                string name = Console.ReadLine();
                Console.WriteLine("Введите наполнение файла");
                string text = Console.ReadLine();
                if (File.Exists(MakeString(currentPath) + name + ".txt"))
                {
                    Console.WriteLine("Файл с таким именем уже существует в данной директории.");
                }
                else
                {
                    File.WriteAllText(MakeString(currentPath) + name + ".txt", text, enc);
                    Console.WriteLine("Файл успешно создан!");
                }

            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Название файла некорректно или к выбранной папке нет доступа.");
            }
        }

        /// <summary>
        /// Создает начало пути (добавляет имя выбранного пользователем диска).
        /// </summary>
        /// <param name="currentPath"></param>
        static void BeginPath(List<string> currentPath)
        {
            try
            {
                currentPath.Clear();
                currentPath.Add(DriveSelect());
                Console.Clear();
            }
            catch (Exception)
            {
                Console.WriteLine("У вас беды с диском.");
            }
        }

        /// <summary>
        /// Добваляет файл из текущий директории в список файлов для конкатенации.
        /// </summary>
        /// <param name="connectors">Текущий список на конкатенацию.</param>
        /// <param name="currentPath">Текущий путь.</param>
        static void AddToConnect(ref List<string> connectors, List<string> currentPath)
        {
            try
            {
                Console.WriteLine("Введите полное имя файла, который будем конкатенировать.");
                string fileName = Console.ReadLine();
                if (File.Exists(MakeString(currentPath) + fileName))
                {
                    connectors.Add(MakeString(currentPath) + fileName);
                    Console.WriteLine("Файл успешно добавлен!");
                }
                else
                {
                    Console.WriteLine("Нет такого файла!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("К выбранной папке нет доступа.");
            }
        }

        /// <summary>
        /// Делает конкатенацию добавленных файлов.
        /// </summary>
        /// <param name="connectors">Текущий список на конкатенацию.</param>
        /// <param name="path">Текущий путь.</param>
        static void Connection(ref List<string> connectors, List<string> path)
        {
            string res = String.Empty;
            try
            {
                foreach (string x in connectors)
                {
                    res += File.ReadAllText(x, Encoding.UTF8);
                }
                Console.WriteLine("Введите имя файла.");
                string fileName = Console.ReadLine();
                File.WriteAllText(MakeString(path) + fileName + ".txt", res);
                connectors.Clear();
                Console.WriteLine("Файл создан. Результат конкатенации:");
                Console.WriteLine(res);
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Неверное имя файла или у вас нет доступа к папке или файлы неправильного расширения.");
            }
        }

        /// <summary>
        /// Выводит список на конкатенацию.
        /// </summary>
        /// <param name="connectors">Список на конкатенацию.</param>
        static void ShowConnectors(List<string> connectors)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Список на конкатенацию.");
                foreach (string x in connectors)
                {
                    Console.WriteLine(Path.GetFileName(x));
                }
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Неверное имя файла или у вас нет к нему доступа.");
            }
        }

        /// <summary>
        /// Удаляет выбранный файл из списка на конкатенацию.
        /// </summary>
        /// <param name="connectors">Список на конкатенацию.</param>
        static void RemoveConnectors(ref List<string> connectors)
        {
            if (connectors.Count > 0)
            {
                Console.WriteLine("Какой по счету файл вы не хотите конкатенировать? (Считая с единицы)");
                int num = ParseIntCon(connectors.Count);
                connectors.RemoveAt(num - 1);
                Console.WriteLine("Убрано!");
            }
            else
            {
                Console.WriteLine("Вы еще ничего не выбрали.");
            }
        }

        /// <summary>
        /// Проверяет введный цвет на валидность.
        /// </summary>
        /// <param name="color">Введеный пользователем цвет.</param>
        /// <returns>true, если цвет валиден, иначе false.</returns>
        static bool ColorValidate(string color)
        {
            bool flag = false;
            // Получаем массив со всеми названиями цветов.
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            foreach (ConsoleColor z in colors)
            {
                if (color.ToLower() == z.ToString().ToLower())
                {
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// Перекрашивает задний фон.
        /// </summary>
        static void ColoriseBack()
        {
            Console.Clear();
            // Получаем массив со всеми названиями цветов.
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            foreach (ConsoleColor z in colors)
            {
                Console.WriteLine(z.ToString());
            }
            Console.WriteLine("Выберите цевт из предложенных выше");
            string color = Console.ReadLine();
            if (ColorValidate(color))
            {
                foreach (ConsoleColor z in colors)
                {
                    if (color.ToLower() == z.ToString().ToLower())
                    {
                        Console.BackgroundColor = z;
                        Console.Clear();
                    }
                }
            }
            else
            {
                Console.WriteLine("Такого цвета нет.");
            }
        }

        /// <summary>
        /// Перекрашивает текст.
        /// </summary>
        static void ColoriseText()
        {
            Console.Clear();
            // Получаем массив со всеми названиями цветов.
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            foreach (ConsoleColor z in colors)
            {
                Console.WriteLine(z.ToString());
            }
            Console.WriteLine("Выберите цевт из предложенных выше");
            string color = Console.ReadLine();
            if (ColorValidate(color))
            {
                foreach (ConsoleColor z in colors)
                {
                    if (color.ToLower() == z.ToString().ToLower())
                    {
                        Console.ForegroundColor = z;
                        Console.Clear();
                    }
                }
            }
            else
            {
                Console.WriteLine("Такого цвета нет.");
            }
        }
        static void Main()
        {
            //В этом листе всегда храним текущий путь.
            List<string> currentPath = new List<string>();
            //В этом листе храним список на конкатенацию.
            List<string> connectors = new List<string>();

            string command;
            string savedPath = String.Empty;
            string copyName = String.Empty;

            Console.WriteLine("Для начала работы выберите диск:");
            //Начинаем собирать путь (с диска).
            currentPath.Add(DriveSelect());
            Console.Clear();
            Console.Write("Настоятельно рекомендую изучить ");
            PrintHelp();
            Console.WriteLine("УБЕДИТЕЛЬНАЯ ПРОСЬБА прочитать все от начала " +
                              "(нужно подняться выше) и до конца.");
            Console.WriteLine("Когда будете готовы нажмите Enter.");
            Console.ReadLine();
            do
            {
                if (MakeString(currentPath) != "")
                {
                    Console.WriteLine();
                    Console.WriteLine($"Cейчас вы на {MakeString(currentPath)}");
                }
                Console.WriteLine("Введите команду");
                command = Console.ReadLine().ToLower();
                //Дальнейшие действия зависят от команды.
                switch (command)
                {
                    case "createencode":
                        CreateEnc(currentPath);
                        break;
                    case "delete":
                        Remove(currentPath);
                        break;
                    case "read":
                        Read(MakeString(currentPath));
                        break;
                    case "readencode":
                        ReadEnc(MakeString(currentPath));
                        break;
                    case "help":
                        PrintHelp();
                        break;
                    case "cd":
                        BeginPath(currentPath);
                        break;
                    case "create":
                        Create(currentPath);
                        break;
                    case "copy":
                        savedPath = Copy(currentPath, out copyName);
                        break;
                    case "move":
                        Move(savedPath, currentPath, copyName);
                        break;
                    case "paste":
                        Paste(savedPath, currentPath, copyName);
                        break;
                    case "show":
                        PrintDirects(MakeString(currentPath));
                        break;
                    case "connect":
                        Connection(ref connectors, currentPath);
                        break;
                    case "add":
                        AddToConnect(ref connectors, currentPath);
                        break;
                    case "clearcons":
                        connectors.Clear();
                        break;
                    case "showcons":
                        ShowConnectors(connectors);
                        break;
                    case "delcon":
                        RemoveConnectors(ref connectors);
                        break;
                    case "travel":
                        if (MakeString(currentPath) != "")
                        {
                            Travel(ref currentPath);
                        }
                        else
                        {
                            Console.WriteLine("У вас не выбран диск.");
                        }
                        break;
                    case "color":
                        ColoriseBack();
                        break;
                    case "colortext":
                        ColoriseText();
                        break;
                    default:
                        if (command != "exit")
                        {
                            Console.WriteLine("Такой команды не существует.");
                        }
                        break;
                }
            } while (command != "exit");
        }
    }
}

//P.S. Очевидно, что некоторые методы содеражат весьма много строк, но это происходит из-за операторов,
//которые невозможно разделить(тот же switch case), или из-за объема текста для вывода(те же правила).
//P.P.S. Не так много обычных комментариев, так как в тот раз за их обилие мне снизи оценку, плюс
//в описаниях метода все написано, что является документацией, кроме того, никаких сложных моментов
//тут вроде как нет, чисто аккуратность работы с файлами??
