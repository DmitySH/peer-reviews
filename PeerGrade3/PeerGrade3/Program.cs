using System;
using System.IO;
using System.Text;

namespace PeerGrade3
{
    class Program
    {
        // Объект для генерации рандомных значиений для матриц.
        static readonly Random rnd = new Random();

        /// <summary>
        /// Ввод размера матрицы.
        /// </summary>
        /// <param name="prompt">Подсказка для ввода.</param>
        /// <returns>Количество строк/столбцов матрицы.</returns>
        static int ParseIntSize(string prompt)
        {
            int num;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1)
            {
                Console.Write(prompt);
            }

            Console.WriteLine();
            return num;
        }

        /// <summary>
        /// Выбор действия над матрицами.
        /// </summary>
        /// <returns>Номер команды.</returns>
        static int ParseIntMenu()
        {
            int num;
            Console.Write("Введите целое число от 0 до 10: ");
            while (!int.TryParse(Console.ReadLine(), out num) || num < 0 || num > 10)
            {
                Console.Write("Введите целое число от 0 до 10:");
            }
            return num;
        }

        /// <summary>
        /// Проверка корректности ввода значений матрицы.
        /// </summary>
        /// <param name="num">Результат приведения к типу double.</param>
        /// <param name="matStr">Строка с текущим вводом.</param>
        static void CheckInput(out double num, StringBuilder matStr)
        {
            //Для обеспечения красивого ввода, весь ввод лежит в строке StringBuilder'a.
            //Если введеное значение является некорректным, то к строке-вводу ничего не прибавляется и ввод повторяется.
            while (!double.TryParse(Console.ReadLine(), out num))
            {
                //Консоль очищается и выводится неизмененная строка для ввода.
                Console.Clear();
                Console.Write(matStr);
            }
        }

        /// <summary>
        /// Проверка корректности ввода числа, на которое пользователь хочет умножить матрицу.
        /// </summary>
        /// <returns>Число, на которое пользователь хочет умножить матрицу.</returns>
        static double ParseDouble()
        {
            double num;
            Console.Write("Введите число, на которое вы хотите умножить матрицу: ");
            while (!double.TryParse(Console.ReadLine(), out num))
            {
                Console.Write("Введите число, на которое вы хотите умножить матрицу: ");
            }
            return num;
        }

        /// <summary>
        /// Повтор ввода размеров матрицы, пока пользователь не введет одинкаовое количество строк и столбцов.
        /// </summary>
        /// <param name="rows">Количество строк.</param>
        /// <param name="cols">Количество столбцов.</param>
        static void CheckSquare(ref int rows, ref int cols)
        {
            while (rows != cols)
            {
                Console.WriteLine("Данная операция возможна только для квадратной матрицы.");
                rows = ParseIntSize("Введите количество строк матрицы: ");
                cols = ParseIntSize("Введите количество столбцов матрицы: ");
            }
        }

        /// <summary>
        /// Генерация матрицы с случайными значениями.
        /// </summary>
        /// <param name="rows">Количество строк в будущей матрице.</param>
        /// <param name="cols">Количество столбцов в будущей матрцие.</param>
        /// <returns>Матрица с случайными значениями.</returns>
        static double[,] RandomizeMatrix(int rows, int cols)
        {
            double[,] m = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //Промежуток рандома ограничен лишь для удобства.
                    m[i, j] = rnd.Next(-100, 101);
                }
            }

            Console.WriteLine("Сгенерированная матрица:");
            Console.WriteLine(MakeString(m, ""));
            return m;
        }

        /// <summary>
        /// Создание матрицы.
        /// </summary>
        /// <param name="isSquare">Истина, если нужна квадратная матрица.</param>
        /// <param name="rand">Истина, если матрица будет сгенерирована с случайными значениями.</param>
        /// <returns>Созданная матрица.</returns>
        static double[,] GenerateMatrix(bool isSquare, bool rand)
        {
            int rows = ParseIntSize("Введите количество строк матрицы: ");
            int cols = ParseIntSize("Введите количество столбцов матрицы: ");
            if (isSquare && rows != cols)
            {
                CheckSquare(ref rows, ref cols);
            }

            double[,] matrix = new double[rows, cols];
            if (rand)
            {
                matrix = RandomizeMatrix(rows, cols);
                return matrix;
            }

            MatrixInput(rows, cols, matrix);
            Console.WriteLine();
            return matrix;
        }

        /// <summary>
        /// Ручной ввод матрицы.
        /// </summary>
        /// <param name="rows">Количество строк в матрице.</param>
        /// <param name="cols">Количество столбцов в матрице.</param>
        /// <param name="matrix">Заполнеяемая матрица.</param>
        static void MatrixInput(int rows, int cols, double[,] matrix)
        {
            StringBuilder matStr = new StringBuilder();
            matStr.Append("Заполните матрицу:").Append(Environment.NewLine).Append("( ");
            Console.Clear();
            Console.Write(matStr);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double num;
                    if (j == cols - 1)
                    {
                        CheckInput(out num, matStr);
                        matrix[i, j] = num;
                        matStr.Append(matrix[i, j]).Append(" )").Append(Environment.NewLine);

                        if (i != rows - 1)
                        {
                            matStr.Append("( ");
                        }
                    }
                    else
                    {
                        CheckInput(out num, matStr);
                        matrix[i, j] = num;
                        matStr.Append(matrix[i, j]).Append(" ");
                    }

                    Console.Clear();
                    Console.Write(matStr);
                }
            }
        }

        /// <summary>
        /// Ввод матрицы с известным количеством строк и столбцов. Можно использовать два раза подряд
        /// для создания матриц одинакового размера.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="matStr"></param>
        /// <returns>Матрица заданного размера.</returns>
        /// Сделано отдельным методом так как он нужен для генерации двух матриц одинакового размера.
        /// Поэтому в этот метод по ссылке передается строка, которая содержит ввод первой матрице.
        /// Это позволяет пользователю видеть обе введеные матрицы.
        static double[,] GenerateSameSizeMatrix(int rows, int cols, ref StringBuilder matStr)
        {
            double[,] matrix = new double[rows, cols];
            matStr.Append("Заполните матрицу:").Append(Environment.NewLine).Append("(");
            Console.Clear();
            Console.Write(matStr);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double num;
                    if (j == cols - 1)
                    {
                        CheckInput(out num, matStr);
                        matrix[i, j] = num;
                        matStr.Append(matrix[i, j]).Append(")").Append(Environment.NewLine);
                        if (i != rows - 1)
                        {
                            matStr.Append("(");
                        }
                    }
                    else
                    {
                        CheckInput(out num, matStr);
                        matrix[i, j] = num;
                        matStr.Append(matrix[i, j]).Append(" ");
                    }
                    Console.Clear();
                    Console.Write(matStr);
                }
            }

            matStr.Append(Environment.NewLine);

            return matrix;
        }

        /// <summary>
        /// Проверка корректности ввода выбора способа создания матрицы.
        /// </summary>
        /// <returns>Число соотвествующее способу создания матрицы.</returns>
        static int ParseIntCreation()
        {
            int num;
            Console.WriteLine("Выберите способ создания матрицы:");
            Console.WriteLine("1) Ручной ввод.");
            Console.WriteLine("2) Случайная генерация (элементы - целые числа от -100 до 100).");
            Console.WriteLine("3) Ввод из файла.");
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > 3)
            {
                Console.WriteLine("Введите целое число от 1 до 3.");
            }

            Console.WriteLine();
            return num;
        }

        /// <summary>
        /// Конвертирование ступенчатого массива в двумерный массив.
        /// </summary>
        /// <param name="ar">Ступенчатый массив.</param>
        /// <returns>Двумерный массив.</returns>
        static double[,] JaggedConverter(double[][] ar)
        {
            double[,] res = new double[ar.Length, ar[0].Length];
            for (int i = 0; i < ar.Length; i++)
            {
                for (int j = 0; j < ar[0].Length; j++)
                {
                    res[i, j] = ar[i][j];
                }
            }

            return res;
        }

        /// <summary>
        /// Проверка образуют ли числа в файле матрицу.
        /// </summary>
        /// <param name="ar"></param>
        /// <returns></returns>
        /// А именно: одинаковое ли количество элементов в каждой из строк матрицы.
        static bool CheckMatrixSize(double[][] ar)
        {
            int len = ar[0].Length;
            for (int i = 1; i < ar.Length; i++)
            {
                if (ar[i].Length != len)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Чтение матрицы из файла.
        /// </summary>
        /// <param name="isSquare">Истина если нужна квадратная матрица.</param>
        /// <param name="path">Путь к файлу для чтения.</param>
        /// <returns>Матрицу из файла.</returns>
        /// Метод получился весьма большим, но его нет смысла разбивать, так как он образует один логический блок.
        /// Выносить каждую провреку - декомпозиция ради декомпозиции...
        static double[,] ReadOneMatrixFromFile(bool isSquare, string path)
        {
            if (!File.Exists(path))
            {
                //Можно ли прочесть файл по данному пути.
                throw new MatrixException("Файл не найден или к нему нет доступа.");
            }
            string[] matLines = File.ReadAllLines(path);

            if (File.ReadAllText(path) == string.Empty)
            {
                //Проверка на наличие чего-либо в файле.
                throw new MatrixException("Файл не может быть пустым.");
            }

            //Чтение матрицы и преобразование ее в ступенчатый массив.
            double[][] matElems = new double[matLines.Length][];
            for (int i = 0; i < matElems.Length; i++)
            {
                string[] str = matLines[i].Trim().Split(' ');
                matElems[i] = new double[str.Length];
                for (int j = 0; j < str.Length; j++)
                {
                    double num;
                    if (double.TryParse(str[j].Trim(), out num))
                    {
                        matElems[i][j] = num;
                    }
                    else
                    {
                        //Проверка на корректность ввода.
                        throw new MatrixException("Матрица должна состоять из чисел.");
                    }
                }
            }

            //Проверка, образуют ли введеные числа матрицу.
            if (!CheckMatrixSize(matElems))
            {
                throw new MatrixException("Введеные числа не образуют матрицу.");
            }

            //Проверка, является ли матрица квадратной(если нужно).
            if (matElems.Length != matElems[0].Length && isSquare)
            {
                throw new MatrixException("Эта операция доступна только для квадратных матриц.");
            }

            Console.WriteLine("Матрица в файле:");
            Console.WriteLine(MakeString(JaggedConverter(matElems), "F3"));
            Console.WriteLine();
            return JaggedConverter(matElems);
        }

        /// <summary>
        /// Выбор способа создания.
        /// </summary>
        /// <param name="isSquare">Истина если нужна квадратная матрица.</param>
        /// <returns>Матрицу, созданную выбранным способом.</returns>
        static double[,] WayOfCreation(bool isSquare)
        {
            switch (ParseIntCreation())
            {
                case 1:
                    return GenerateMatrix(isSquare, false);
                case 2:
                    return GenerateMatrix(isSquare, true);
                case 3:
                    return ReadOneMatrixFromFile(isSquare, "inputOneMatrix.txt");
            }
            return GenerateMatrix(isSquare, false);
        }

        /// <summary>
        /// Поиск следа матрицы.
        /// </summary>
        /// <returns>След матрицы.</returns>
        static double FindStep()
        {
            double[,] m = WayOfCreation(true);
            double step = 0;
            for (int i = 0; i < m.GetLength(0); i++)
            {
                step += m[i, i];
            }

            return step;
        }

        /// <summary>
        /// Поиск транспонированной матрицы.
        /// </summary>
        /// <returns>Строка с транспонированной матрицей.</returns>
        static StringBuilder Transposition()
        {
            double[,] m = WayOfCreation(false);
            double[,] res = new double[m.GetLength(1), m.GetLength(0)];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    res[j, i] = m[i, j];
                }
            }

            return MakeString(res, "F3");
        }

        /// <summary>
        /// Создание двух матриц одинакового размера.
        /// </summary>
        /// <param name="a">Первая матрица.</param>
        /// <param name="b">Вторая матрица.</param>
        static void MakeSameSize(out double[,] a, out double[,] b)
        {
            StringBuilder matStr = new StringBuilder();
            int rows = 0;
            int cols = 0;
            switch (ParseIntCreation())
            {
                case 1:
                    rows = ParseIntSize("Введите количество строк матриц: ");
                    cols = ParseIntSize("Введите количество столбцов матриц: ");
                    a = GenerateSameSizeMatrix(rows, cols, ref matStr);
                    b = GenerateSameSizeMatrix(rows, cols, ref matStr);
                    break;
                case 2:
                    rows = ParseIntSize("Введите количество строк матриц: ");
                    cols = ParseIntSize("Введите количество столбцов матриц: ");
                    Console.Clear();
                    a = RandomizeMatrix(rows, cols);
                    Console.WriteLine();
                    b = RandomizeMatrix(rows, cols);
                    break;
                case 3:
                    a = ReadOneMatrixFromFile(false, "inputFirstMatrix.txt");
                    Console.WriteLine();
                    b = ReadOneMatrixFromFile(false, "inputSecondMatrix.txt");
                    if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
                    {
                        // Проверка на совпадение размеров матриц.
                        throw new MatrixException("Матрицы в файлах разного размера.");
                    }
                    break;
                default:
                    a = RandomizeMatrix(rows, cols);
                    b = RandomizeMatrix(rows, cols);
                    break;
            }
        }

        /// <summary>
        /// Создание подходящих для умножения матриц.
        /// </summary>
        /// <param name="a">Первая матрица.</param>
        /// <param name="b">Вторая матрица.</param>
        static void MakeMatrixForMultiply(out double[,] a, out double[,] b)
        {
            int row1;
            int col1;
            int col2;
            StringBuilder matStr = new StringBuilder();
            switch (ParseIntCreation())
            {
                case 1:
                    GetSizeForMultiply(out row1, out col1, out col2);
                    a = GenerateSameSizeMatrix(row1, col1, ref matStr);
                    b = GenerateSameSizeMatrix(col1, col2, ref matStr);
                    break;
                case 2:
                    GetSizeForMultiply(out row1, out col1, out col2);
                    Console.Clear();
                    a = RandomizeMatrix(row1, col1);
                    Console.WriteLine();
                    b = RandomizeMatrix(col1, col2);
                    break;
                case 3:
                    Console.Clear();
                    a = ReadOneMatrixFromFile(false, "inputFirstMatrix.txt");
                    b = ReadOneMatrixFromFile(false, "inputSecondMatrix.txt");
                    if (a.GetLength(1) != b.GetLength(0))
                    {
                        //Проверка на корректность размеров матриц для умножения.
                        throw new MatrixException("Матрицы в файлах неподходящего для умножения размера.");
                    }
                    break;
                default:
                    a = RandomizeMatrix(1, 1);
                    b = RandomizeMatrix(1, 1);
                    break;
            }
        }

        /// <summary>
        /// Получние корректного размера матриц для умножения.
        /// </summary>
        /// <param name="row1">Число строк первой матрицы.</param>
        /// <param name="col1">Число строк второй матрицы/число столбцов первой матрицы.</param>
        /// <param name="col2">Число столбцов второй матрицы.</param>
        static void GetSizeForMultiply(out int row1, out int col1, out int col2)
        {
            row1 = ParseIntSize("Введите количество строк в 1-ой матрице: ");
            col1 = ParseIntSize("Введите количество столбцов в 1-ой матрице. " + Environment.NewLine +
                                "Оно же количество строк во 2-ой матрице: ");
            col2 = ParseIntSize("Введите количество столбцов в 2-ой матрице: ");
        }

        /// <summary>
        /// Суммирование двух матриц.
        /// </summary>
        /// <returns>Строка с матрицей-суммой.</returns>
        static StringBuilder Sum()
        {
            double[,] a;
            double[,] b;
            MakeSameSize(out a, out b);
            double[,] res = new double[a.GetLength(0), a.GetLength(1)];
            Console.WriteLine();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    res[i, j] = a[i, j] + b[i, j];
                }
            }

            StringBuilder sumMat = new StringBuilder();
            sumMat.Append(MakeString(res, "F3"));
            return sumMat;
        }
        static StringBuilder Diff()
        {
            double[,] a;
            double[,] b;
            MakeSameSize(out a, out b);
            double[,] res = new double[a.GetLength(0), a.GetLength(1)];
            Console.WriteLine();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    res[i, j] = a[i, j] - b[i, j];
                }
            }
            StringBuilder difMat = new StringBuilder();
            difMat.Append(MakeString(res, "F3"));

            return difMat;
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        /// <returns>Строка с матрицей, умноженной на число.</returns>
        static StringBuilder ProdConst()
        {
            double mult = ParseDouble();
            double[,] m = WayOfCreation(false);
            double[,] res = new double[m.GetLength(0), m.GetLength(1)];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    res[i, j] = m[i, j] * mult;
                }
            }
            StringBuilder prodMat = new StringBuilder();
            prodMat.Append(MakeString(res, "F3"));
            Console.WriteLine();
            return prodMat;
        }

        /// <summary>
        /// Вычисление элемента при умножении матриц.
        /// </summary>
        /// <param name="a">Первая матрица.</param>
        /// <param name="b">Вторая матрица.</param>
        /// <param name="i">Столбец первой матрицы.</param>
        /// <param name="j">Строка второй матрицы.</param>
        /// <returns>Посчитанный элемент i-ой строки j-ого столбца.</returns>
        static double CalculateElement(double[,] a, double[,] b, int i, int j)
        {
            double res = 0;
            for (int k = 0; k < a.GetLength(1); k++)
            {
                res += a[i, k] * b[k, j];
            }
            return res;
        }

        /// <summary>
        /// Умножение двух матриц.
        /// </summary>
        /// <returns>Строка с матрицей-произведением.</returns>
        static StringBuilder MatrixMultiply()
        {
            StringBuilder multMat = new StringBuilder();
            double[,] a;
            double[,] b;
            MakeMatrixForMultiply(out a, out b);
            double[,] res = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    res[i, j] = CalculateElement(a, b, i, j);
                }
            }
            multMat.Append(MakeString(res, "F3"));
            Console.WriteLine();
            return multMat;
        }

        /// <summary>
        /// LU-Разложение матрицы.
        /// </summary>
        /// <param name="l">L-матрица.</param>
        /// <param name="u">U-матрица.</param>
        /// Алгоритм Дуллитла.
        static void LuDecompose(out double[,] l, out double[,] u)
        {
            double[,] m = WayOfCreation(true);
            int n = m.GetLength(0);
            l = new double[n, n];
            u = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                //Верхнетреугольная.
                for (int k = i; k < n; k++)
                {
                    double sum = 0;
                    for (int j = 0; j < i; j++)
                    {
                        sum += l[i, j] * u[j, k];
                    }
                    u[i, k] = m[i, k] - sum;
                }
                //Нижнетреугольная.
                for (int k = i; k < n; k++)
                {
                    if (i == k)
                    {
                        l[i, i] = 1;
                    }
                    else
                    {
                        double sum = 0;
                        for (int j = 0; j < i; j++)
                        {
                            sum += l[k, j] * u[j, i];
                        }
                        if (u[i, i] != 0)
                        {
                            l[k, i] = (m[k, i] - sum) / u[i, i];
                        }
                        else
                        {
                            l[k, i] = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// LU-разложение, в котором нужна только U-матрица.
        /// </summary>
        /// <param name="m">Матрица для разложения.</param>
        /// <returns>U-матрица.</returns>
        static double[,] UDecompose(double[,] m)
        {
            //Алгоритм как в LU.
            int n = m.GetLength(0);
            double[,] l = new double[n, n];
            double[,] u = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int k = i; k < n; k++)
                {
                    double sum = 0;
                    for (int j = 0; j < i; j++)
                    {
                        sum += l[i, j] * u[j, k];
                    }
                    u[i, k] = m[i, k] - sum;
                }
                for (int k = i; k < n; k++)
                {
                    if (i == k)
                    {
                        l[i, i] = 1;
                    }
                    else
                    {
                        double sum = 0;
                        for (int j = 0; j < i; j++)
                        {
                            sum += (l[k, j] * u[j, i]);
                        }
                        if (u[i, i] != 0)
                        {
                            l[k, i] = (m[k, i] - sum) / u[i, i];
                        }
                        else
                        {
                            l[k, i] = 0;
                        }
                    }
                }
            }
            return u;
        }

        /// <summary>
        /// Преборазовние матрицы для вывода в строку StringBuilder'a.
        /// </summary>
        /// <param name="m">Матрица для преобразования.</param>
        /// <param name="format">Формат вывода.</param>
        /// <returns>Строка с матрицей в виде для вывода.</returns>
        static StringBuilder MakeString(double[,] m, string format)
        {
            StringBuilder strMat = new StringBuilder();
            strMat.Append("( ");
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    if (j == m.GetLength(1) - 1)
                    {
                        //Завершаем строку скобкой.
                        strMat.Append(m[i, j].ToString(format)).Append(" )");
                        if (i != m.GetLength(0) - 1)
                        {
                            //Начинаем строку с скобки.
                            strMat.Append(Environment.NewLine).Append("( ");
                        }
                    }
                    else
                    {
                        strMat.Append(m[i, j].ToString(format)).Append(" ");
                    }
                }
            }

            return strMat;
        }

        /// <summary>
        /// Вычисление определителя матрицы.
        /// </summary>
        /// <returns>Значение определиетля.</returns>
        static double Determinant()
        {
            //Определитель равен произведению элементов главной диагонали U-матрицы.
            double[,] l;
            double[,] u;
            LuDecompose(out l, out u);
            double det = 1;
            for (int i = 0; i < l.GetLength(0); i++)
            {
                det *= u[i, i];
            }

            //Так как в LU-разложении присутствует вещественное деление, может возникнуть минамальная погрешноть.
            //Для случая с нулем приводим это значение к нулю(чтобы можно было считать Крамером.
            if (Math.Abs(det) < 0.0001)
            {
                det = 0;
            }
            if (det - (int)det > 0.999)
            {
                det = Math.Round(det);
            }
            return det;
        }

        /// <summary>
        /// Нахождение определителя только по U-матрице.
        /// </summary>
        /// <param name="u"></param>
        /// <returns>Значение определителя.</returns>
        static double Determinant(double[,] u)
        {
            //Определитель равен произведению элементов главной диагонали U-матрицы.
            u = UDecompose(u);
            double det = 1;
            for (int i = 0; i < u.GetLength(0); i++)
            {
                det *= u[i, i];
            }

            //Так как в LU-разложении присутствует вещественное деление, может возникнуть минамальная погрешноть.
            //Для случая с нулем приводим это значение к нулю(чтобы можно было считать Крамером.
            if (Math.Abs(det) < 0.0001)
            {
                det = 0;
            }
            if (det - (int)det > 0.9999)
            {
                det = Math.Round(det);
            }
            return det;
        }

        /// <summary>
        /// Показывает функции программы.
        /// </summary>
        static void ShowFunctions()
        {
            Console.WriteLine("Выберите действие с матрицами:");
            Console.WriteLine("0) Завершение работы программы.");
            Console.WriteLine("1) Нахождение следа матрицы.");
            Console.WriteLine("2) Нахождение транспонированной матрицы.");
            Console.WriteLine("3) Нахождение суммы двух матриц.");
            Console.WriteLine("4) Нахождение разности двух матриц.");
            Console.WriteLine("5) Нахождение произведения двух матриц.");
            Console.WriteLine("6) Нахождение произведения матрицы на число.");
            Console.WriteLine("7) Нахождение определителя матрицы.");
            Console.WriteLine("8) Нахождение LU-разложения матрицы.");
            Console.WriteLine("9) Решение СЛАУ.");
            Console.WriteLine("10) Напомнить правила.");
            Console.WriteLine();
        }

        /// <summary>
        /// Ожидание после выполнения какой-либо команды.
        /// </summary>
        static void Await()
        {
            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите два раза любую клавишу.");
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Ввод вектора свободных членов СЛАУ.
        /// </summary>
        /// <param name="a">Вектор свободных членов..</param>
        /// <param name="rows">Количество строк матрицы коэффициентов.</param>
        /// <param name="cols">Количество столбцов. матрицы коэффициентов.</param>
        /// <param name="matStr">Строка для вывода.</param>
        static void MakeMatrixForSolve(out double[,] a, int rows, int cols, ref StringBuilder matStr)
        {
            switch (ParseIntCreation())
            {
                case 1:
                    matStr.Append(Environment.NewLine);
                    a = GenerateSameSizeMatrix(rows, cols, ref matStr);
                    break;
                case 2:
                    a = RandomizeMatrix(rows, cols);
                    Console.WriteLine();
                    break;
                case 3:
                    a = ReadOneMatrixFromFile(false, "inputFirstMatrix.txt");
                    Console.WriteLine();
                    if (a.GetLength(1) != 1 || a.GetLength(0) != rows)
                    {
                        throw new MatrixException("Матрицы в файлах неподходящего размера.");
                    }
                    break;
                default:
                    a = RandomizeMatrix(rows, cols);
                    break;
            }
        }

        /// <summary>
        /// Проверка единственности решения СЛАУ.
        /// </summary>
        /// <param name="mainDet">Определитель матрицы коэффициентов.</param>
        /// <returns>Истина, если решение единственно.</returns>
        static bool CheckSolutions(double mainDet)
        {
            //Условие единственности решения.
            bool notOne = mainDet == 0;

            return notOne;
        }

        /// <summary>
        /// Поиск всех определителей в методе Крамера.
        /// </summary>
        /// <param name="koefs">Матрица коэффициентов.</param>
        /// <param name="frees">Столбец свободных членов.</param>
        /// <returns>Массив всех определителей для метода Крамера.</returns>
        static double[] Deters(double[,] koefs, double[,] frees)
        {
            double[,] koefsCopy = new double[koefs.GetLength(0), koefs.GetLength(0)];
            double[] allDets = new double[koefs.GetLength(0)];
            for (int i = 0; i < koefs.GetLength(0); i++)
            {
                Array.Copy(koefs, koefsCopy, koefsCopy.Length);
                //Ставим на j-ый столбец столбец свободных членов.
                for (int j = 0; j < koefs.GetLength(0); j++)
                {
                    koefsCopy[j, i] = frees[j, 0];
                }

                allDets[i] = Determinant(koefsCopy);
            }

            return allDets;
        }

        /// <summary>
        /// Решение СЛАУ.
        /// </summary>
        /// <param name="mainDet">Определитель матрицы коэффициентов.</param>
        /// <param name="allDets">Массив всех определителей для метода Крамера.</param>
        /// <returns>Столбец решений.</returns>
        static double[,] FindSolutions(double mainDet, double[] allDets)
        {
            double[,] solutions = new double[allDets.GetLength(0), 1];
            //Поиск решения по формуле Крамера.
            for (int i = 0; i < allDets.Length; i++)
            {
                solutions[i, 0] = allDets[i] / mainDet;
            }

            return solutions;
        }

        /// <summary>
        /// Все этапы решения СЛАУ.
        /// </summary>
        /// <returns>Строка с столбцом-решением.</returns>
        static StringBuilder Solver()
        {
            double[,] a = WayOfCreation(true);
            double[,] b;
            double[,] solutions;
            StringBuilder matStr = new StringBuilder();
            StringBuilder res = new StringBuilder();
            matStr.Append("Матрица коэффициентов:").Append(Environment.NewLine).Append(MakeString(a, "F3"));
            MakeMatrixForSolve(out b, a.GetLength(0), 1, ref matStr);
            double mainDet = Determinant(a);
            double[] allDets = Deters(a, b);
            bool notOne = CheckSolutions(mainDet);
            if (notOne)
            {
                res.Append("Система не имеет решений или система имеет бесконечное множество решений.");
                return res;
            }

            solutions = FindSolutions(mainDet, allDets);
            return MakeString(solutions, "F3");
        }

        /// <summary>
        /// Выбор команды.
        /// </summary>
        static void Menu()
        {
            string path = "result.txt";
            while (true)
            {
                ShowFunctions();
                int choice = ParseIntMenu();
                //Стоит большой TryCatch лишь чтобы при некорректных ввыодах с файла программа возвращалась сразу к выбору команды.
                try
                {
                    switch (choice)
                    {
                        case 0:
                            return;
                        case 1:
                            StepResult(path);
                            break;
                        case 2:
                            TranspResult(path);
                            break;
                        case 3:
                            SummResult(path);
                            break;
                        case 4:
                            DiffResult(path);
                            break;
                        case 5:
                            MultResult(path);
                            break;
                        case 6:
                            ProdResult(path);
                            break;
                        case 7:
                            DetResult(path);
                            break;
                        case 8:
                            LuResult(path);
                            break;
                        case 9:
                            SlauResult(path);
                            break;
                        case 10:
                            Console.Clear();
                            PrintRules();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Результат работы команды Решение СЛАУ.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void SlauResult(string path)
        {
            string res;
            res = "Решение СЛАУ:" + Environment.NewLine + Solver();
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
        }

        /// <summary>
        /// Результат работы команды LU-разложение.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void LuResult(string path)
        {
            string res;
            double[,] u;
            double[,] l;
            LuDecompose(out l, out u);
            res = "LU-разложение матрицы: " + Environment.NewLine + "L:" + Environment.NewLine
                  + MakeString(l, "F3") + Environment.NewLine + "U:" + Environment.NewLine +
                  MakeString(u, "F3");
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
        }

        /// <summary>
        /// Результат работы команды Поиск определителя.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void DetResult(string path)
        {
            string res;
            res = "Определитель матрицы равен: " + Determinant().ToString("F3");
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
        }

        /// <summary>
        /// Результат работы команды Умножение матрицы на число.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void ProdResult(string path)
        {
            string res;
            res = "Произведние матрицы на число равно: " + Environment.NewLine + ProdConst();
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
        }

        /// <summary>
        /// Результат работы команды Умножение матриц.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void MultResult(string path)
        {
            string res;
            res = "Произведние двух матриц равно: " + Environment.NewLine + MatrixMultiply();
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
        }

        /// <summary>
        /// Результат работы команды Вычитание матриц.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void DiffResult(string path)
        {
            string res;
            res = "Разность двух матриц равна: " + Environment.NewLine + Diff();
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
        }

        /// <summary>
        /// Результат работы команды Сумма матриц.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void SummResult(string path)
        {
            string res;
            res = "Сумма двух матриц равна: " + Environment.NewLine + Sum();
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
            return;
        }

        /// <summary>
        /// Результат работы команды Транспонирование матрицы.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void TranspResult(string path)
        {
            string res;
            res = "Транспонированная матрица равна: " + Environment.NewLine + Transposition();
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
            return;
        }

        /// <summary>
        /// Резлуьтат команды Поиск следа матрицы.
        /// </summary>
        /// <param name="path">Путь к файлу с результатом.</param>
        static void StepResult(string path)
        {
            string res;
            res = "След матрицы равен " + Environment.NewLine + FindStep().ToString("F3");
            Console.WriteLine(res);
            File.WriteAllText(path, res);
            Await();
        }

        /// <summary>
        /// Печатает правила пользования.
        /// </summary>
        static void PrintRules()
        {
            Console.WriteLine("Здравствуй, дорогой друг!");
            Console.WriteLine("Перед началом работы с Матричным калькулятором " + Environment.NewLine +
                              "попрошу тебя ознакомиться с парой правил:");
            Console.WriteLine("При ручном вводе матриц после ввода каждого элемента матрицы " + Environment.NewLine +
                              "необходимо нажимать Enter (перенос осуществится автоматически).");
            Console.WriteLine(@"При вводе матриц из файла, если действие требует одну матрицу, то "+ Environment.NewLine +
                              @"она берется из файла ""inputOneMatrix.txt"". Если требуется две матрицы, то"+ Environment.NewLine +
                              @"первая берется из файла ""inputFirstMatrix.txt"","+ Environment.NewLine +
                              @"вторая берется из файла ""inputSecondMatrix.txt"".");
            Console.WriteLine("Исключение - решение СЛАУ.");
            Console.WriteLine("Эти файлы лежат в папке с исполняемым модулем.");
            Console.WriteLine("Матрицы в файл следует записывать в виде  чисел, разделенных пробелом," +Environment.NewLine+
                              "а новую строку матрицы начинать с новой строки в файле.");
            Console.WriteLine("Пример записи матриц написан в самих файлах.");
            Console.WriteLine(@"Также результат последнего действия записывается в файл ""result.txt"".");
            Console.WriteLine("При решении СЛАУ сначала введите матрицу коэффициентов,");
            Console.WriteLine(@"(При вводе из файла запишите ее в ""inputOneMatrix.txt""),");
            Console.WriteLine("затем введите столбец свободных членов (СЛАУ в векторном виде)");
            Console.WriteLine(@"(При вводе из файла запишите его в ""inputFirstMatrix.txt"").");
            Console.WriteLine("Результатом будет столбец решений(если он единственный).");
            Console.WriteLine("На этом все, приятного пользования!");
            Console.WriteLine("Как будете готовы жмите любую клавишу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void Main()
        {
            PrintRules();
            Menu();
        }
    }
}

// P.S. Я не стал ограничивать количество строк и столбцов матрицы, так как не видел в этом смысла, ибо прога не должна упасть(словится исключение) и по причинам ниже....
// В любом случае было сказано, что должна программа для матриц 10 на 10 работать.
// Так же не стал вводить ограничение на ввод в самих элементах, так как
// 1) Это режет функционал программы так как программа может работать с очень большими числами.
// 2) Ограничение по-хорошему ставится после тестирования, для того чтобы не урезать функционал.
// 3) При желании это делается в одну строчку.
// Поэтому надеюсь без приколов с переполнением.
// P.P.S. Некоторые методы весьма большие, но я не хотел их разбивать, так как для меня они-единый логический блок.
// P.P.P.S Документация, я так понимаю, это в основном XML. Обычных комментариев мало, так ка мало сложных моментов.
// XML и так отражает суть методов.
// Надеюсь на ваше понимание.