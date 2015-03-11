using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Bankomat_Zdurov
{
    class Program
    {
        static void availableMoney(int[,] countMoney)
        {
            Console.WriteLine("Доступно банкнот:");

            string[] readText = File.ReadAllLines(@"countMoney.txt");
            int i = 0;
            foreach (string line in readText)
            {
                var read = line.Split(new char[] { '-' }, 2);
                countMoney[i, 0] = int.Parse(read[0]);
                countMoney[i, 1] = int.Parse(read[1]);
                Console.WriteLine("{0} - {1} купюр", countMoney[i, 0], countMoney[i, 1]);
                i++;
            }
        }

        static void giveMoney(int[,] countMoney, int moneyClient)
        {
            int[] banknotes = new int[4] { 0, 0, 0, 0 };
            if (moneyClient >= 20000 && moneyClient <= 5000000)
            {
                StreamWriter file = new StreamWriter(@"countMoney.txt");
                Console.WriteLine("\nВыдано:");
                if (moneyClient % 50000 != 0 && countMoney[0, 1] >= 3)
                {
                    banknotes[0] += 3;
                    moneyClient -= 60000;
                    countMoney[0, 1] -= 3;
                }
                for (int i = 3; i >= 0; i--)
                {
                    if (moneyClient / countMoney[i, 0] > countMoney[i, 1])
                        banknotes[i] = countMoney[i, 1];
                    else
                        banknotes[i] = moneyClient / countMoney[i, 0];
                    moneyClient -= banknotes[i] * countMoney[i, 0];
                    countMoney[i, 1] -= banknotes[i];

                    if (banknotes[i] > 0)
                    {
                        Console.WriteLine("{0}: {1}", countMoney[i, 0], banknotes[i]);
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    file.WriteLine("{0} - {1}", countMoney[i, 0], countMoney[i, 1]);
                }
                file.Close();
                Console.WriteLine("Ваш остаток: {0} рублей", moneyClient);
            }
            else
                Console.WriteLine("Введена неверная сумма!");
        }

        static void Main(string[] args)
        {
            try
            {
                int[,] countMoney = new int[4, 2];
                availableMoney(countMoney);
                Console.WriteLine("\nМинимальная сумма: 20000 р.\nМаксимальная сумма: 5000000 р.\n\nВведите сумму в рублях:");
                int moneyClient = int.Parse(Console.ReadLine());


                giveMoney(countMoney, moneyClient);

            }
            catch
            {
                Console.WriteLine("Error");
            }
            finally
            {
                Console.ReadLine();
            }
        }

    }
}
