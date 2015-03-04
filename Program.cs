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
        static void availableMoney(int[,] CountMoney)
        {
            Console.WriteLine("Доступно банкнот:");

            string[] readText = File.ReadAllLines(@"CountMoney.txt");
            int i = 0;
            foreach (string line in readText)
            {
                var read = line.Split(new char[] { '-' }, 2);
                CountMoney[i, 0] = int.Parse(read[0]);
                CountMoney[i, 1] = int.Parse(read[1]);
                Console.WriteLine("{0} - {1} купюр", CountMoney[i, 0], CountMoney[i, 1]);
                i++;
            }
        }

        static void giveMoney(int[,] CountMoney, int moneyClient)
        {
            int[] banknotes = new int[4] { 0, 0, 0, 0 };
            if (moneyClient >= 20000 && moneyClient <= 5000000)
            {
                StreamWriter file = new StreamWriter(@"CountMoney.txt");
                for (int i = 3; i >= 0; i--)
                {
                    if (moneyClient / CountMoney[i, 0] > CountMoney[i, 1])
                        banknotes[i] = CountMoney[i, 1];
                    else 
                        banknotes[i] = moneyClient / CountMoney[i, 0];
                    moneyClient -= banknotes[i] * CountMoney[i, 0];
                    CountMoney[i, 1] -= banknotes[i];

                    if (banknotes[i] > 0)
                    {
                        Console.WriteLine("{0}: {1}", CountMoney[i, 0], banknotes[i]);
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    file.WriteLine("{0} - {1}", CountMoney[i, 0], CountMoney[i, 1]);
                }
                file.Close();
                Console.WriteLine("Ваш остаток: {0} рублей", moneyClient);
            }
            else
                Console.WriteLine("Введена неверная сумма!");
        }

        static void Main(string[] args)
        {
            int[,] CountMoney = new int[4, 2];
            availableMoney(CountMoney);
            Console.WriteLine("\nМинимальная сумма: 20000 р.\nМаксимальная сумма: 5000000 р.\n\nВведите сумму в рублях:");
            int moneyClient = int.Parse(Console.ReadLine());

            
            giveMoney(CountMoney, moneyClient);
            
            Console.ReadLine();
        }
        
    }
}
