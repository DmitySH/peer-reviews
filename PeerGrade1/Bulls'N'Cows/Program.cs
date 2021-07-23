﻿using System;

namespace Bulls_N_Cows
{
    class Program
    {
        /// <summary>
        /// Метод безопасного преобразования строки в целое число типа Int.
        /// Используется только для ввода количества цифр числа(n).
        /// </summary>
        /// <returns>Преобразованное в Int число пользователя (количество разрядов).</returns>
        static int ParseInt()
        {
            int num;
            // Есть проверка на положительность и меньшести десяти, так как если кол-во цифр больше 10,
            // то невозможно из них составить число без повторения цифр.
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > 10)
            {
                Console.WriteLine("Ошибка ввода! Для игры следует ввести целое число от 1 до 10.\n\r" +
                                  "Это количество цифр в числе, которое я загадываю");
            }

            return num;
        }

        /// <summary>
        /// Метод безопасного преобразования строки в целое число типа Long.
        /// </summary>
        /// <param name="n">Число, которое вводит пользователь, пытаясь угадать.</param>
        /// <returns>Преобразованное в Long число пользователя.</returns>
        static long ParseLong(int n)
        {
            // Используется Long так как int'а может не хватить
            // например, когда угадываем десятизначное число.
            long num;
            /*
              От себя решил добавить проверку на попадание в промежуток n - значных
              чисел, например при n = 3 число должно находиться в промежутке [10^3 ; 10^4).
              Но при этом разрешаем вводить -1, как число, оканчивающее текущую игру (ниже будет видно).
            */
            while ((!long.TryParse(Console.ReadLine(), out num) || num < Math.Pow(10, n - 1)
                                                                 || num >= Math.Pow(10, n))
                                                                && num != -1)
            {
                Console.WriteLine("Ошибка ввода! Для игры следует ввести " +
                                  $"положительное целое {n}-значное число или -1.\n\r");
            }

            return num;
        }

        /// <summary>
        /// Метод генерации рандомного числа без повторов цифр.
        /// </summary>
        /// <param name="n"> Количество разрядов в числе.</param>
        /// <returns>Рандомное n-разрядное число типа Long.</returns>
        static long Generate(int n)
        {
            /*
             P.S. тут можно придумать много способов
             как это сделать, способ, который я использовал изначально, был всем показан
             Николаем Константиновичем на конференции про PeerGrade, поэтому решил сделать по-другому.
             Это к слову об альтернативОчке. Можно было вообще while с миллионом условий сделать...
             Но мне захотелось так. Ниже - что я делаю.
            */

            // В массив вносим все возможные цифры.
            int[] digits = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rnd = new Random();
            // Создаем пустую строку в которой будем собирать число.
            string randString = "";
            int index;
            // Цикл до тех пор, пока длина строки не составит n, то есть пока  у числа не будет n разрядов.
            while (randString.Length < n)
            {
                // Номер ячейки обращения - рандомное число.
                index = rnd.Next(0, 10);
                /*
                   Если в мы обратимся к ячейке массива и возьмем цирфу оттуда, в нее запишем -1,
                   чтобы понять, что цифра уже использовалась||прямо под||проверка для того, чтобы
                   число не начало создаваться с нуля, без которой я около часа ловил в 10% случаев exception.
                 */
                if ((digits[index] != -1) && (randString.Length > 0 || digits[index] != 0))
                {
                    // Приклеиваем к нашей строке цифру из массива.
                    randString = randString + digits[index];
                    digits[index] = -1;
                }
            }
            // Парсим полученную строку-число в Long.
            return long.Parse(randString);
        }

        /// <summary>
        /// Метод проверки числа на повтор цифр.
        /// </summary>
        /// <param name="num">Строка-число пользователя.</param>
        /// <returns>True, если есть повтор, иначе false.</returns>
        static bool NotDifferent(string num)
        {
            /*
               Сравниваем каждый элемент строки с каждым, если совпало возвращаем true, 
               в противном случае false. Без это проверки была слишком имбовая фича, позволяющая
               легко проверить, есть ли в числе конкретная цифра.
             */
            for (int i = 0; i < num.Length - 1; i++)
            {
                for (int j = i + 1; j <= num.Length - 1; j++)
                {
                    if (num[i] == num[j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        /// <summary>
        /// Метод, где реализована сама логика игры.
        /// </summary>
        /// <param name="n">Количество разрядов в числе</param>
        /// <param name="trueNum">Сгенерированное рандомное число.</param>
        static void Game(int n, long trueNum)
        {
            // Думаю самый простой способ реализации логики - работа со строкой, а не числом (имхо!).
            // Поэтому загаданное число и число игрока (чуть позже) приводим к строке.
            string playerInStr, trueInStr;
            trueInStr = trueNum.ToString();
            // Создаем массив в n элементов чтобы хранить в нем все цифры (но как символы, так как работаем
            // со строкой), что были использованы в числе игрока, чтобы потом легко посчитать коров.
            char[] usedNums = new char[n];
            long playerNum;
            for (int i = 0; i < n; i++)
            {
                usedNums[i] = trueInStr[i];
            }
            // Повторяем ввод игроком числа, пока он не угадает его.
            do
            {
                // Считаем быков и коров.
                int cows = 0, bulls = 0;
                Console.WriteLine($"Попытайтесь угадать число, напомню, оно {n}-значное " +
                                  "и цифры в нём НЕ могут повторяться! Если надоело играть - не беда!" +
                                  " Вводи -1 и радуйся жизни :)");
                // Безопасный ввод n-значного числа.
                playerNum = ParseLong(n);
                // Если игроку надоело и он ввел -1, сворачиваем лавочку.
                if (playerNum == -1)
                {
                    return;
                }
                // Число игрока - в строку.
                playerInStr = playerNum.ToString();
                // Проверяем введеное число на повтор цифр.
                if (NotDifferent(playerInStr))
                {
                    Console.WriteLine("Ну как же так, в твоем числе цифры-то повторяются!");
                    continue;// Пропускает текущую итерацию цикла.
                }
                for (int i = 0; i < n; i++)
                {
                    // Проверяем, совпадает ли цифры на одинаковых позициях в числе игрока и загаданном .
                    if (playerInStr[i] == trueInStr[i])
                    {
                        // Сопало - значит это бык.
                        bulls += 1;
                    }
                    else
                    {
                        // Не совпало, проверяем, может хоть цифры такие были в числе.
                        for (int j = 0; j < n; j++)
                        {
                            // Пригодился массив со всеми цифрами загаданного числа. Цифра числа игрока
                            // сравнивается с каждой цифрой загаданного числа. Совпала - это корова.
                            if (usedNums[j] == playerInStr[i])
                            {
                                cows += 1;
                            }
                        }
                    }
                }
                // Ну и коли не угадал - так и быть говорю сколько быков, сколько коров.
                if (playerNum != trueNum)
                {
                    Console.WriteLine($"Вы нашли {bulls} быков");
                    Console.WriteLine($"Вы нашли {cows} коров");
                }
            } while (playerNum != trueNum);
            // А уж если угадал - мое почтение! P.S. ниже написано WIN, не очень красиво получилось...
            Console.WriteLine("*       *  * * *  ***    ** ");
            Console.WriteLine(" *  *  *     *    **  ** ** ");
            Console.WriteLine("  *   *    * * *  **    *** ");
        }
        // В Main'e только вызываем методы и поясняем игроку ХаУтУпЛеЙ.
        static void Main()
        {
            // Повтор решения, думаю, в комментариях не нуждается.
            do
            {
                Console.WriteLine(@"Привет! Вижу, ты зашел поиграть в ""Быков и коров"". Правила очень " +
                                  "простые: выбираешь, скольки значное число я загадаю, после чего пытаешься " +
                                  "его отгадать. Угадал и цифру, и ее место в этой жизни - это бык, угадал " +
                                  "только цифру, но не место - это корова." +
                                  " Помни: цифры в числе НЕ повторяются! Удачи :)");

                Console.WriteLine("Для того, чтобы начать играть, введите число от 1 до 10.\n\r" +
                                  "Это будет количество цифр в числе, которое я загадываю");

                int n = ParseInt(); // Кол-во цифр в числе.
                long randInt = Generate(n); // Генерим рандомное число.

                // Ниже - загаданное число, если хочется его видеть, можно раскомментить.
                //Console.WriteLine(randInt);

                Game(n, randInt); // Собственно вся игра там.
                Console.WriteLine("Нажмите Escape для выхода.\n\r" +
                                  "Чтобы продолжить игру нажмите что-угодно, кроме Escape\n\r");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
