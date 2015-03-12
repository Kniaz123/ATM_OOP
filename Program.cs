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
        static int availableMoney(int[,] countMoney)
        {
            Console.WriteLine("Доступно банкнот:");
            string[] readText = File.ReadAllLines(@"countMoney1.txt");
            int i = 0;
            foreach (string line in readText)
            {
                var read = line.Split(new char[] { '-' }, 2);
                countMoney[i, 0] = int.Parse(read[0]);
                countMoney[i, 1] = int.Parse(read[1]);
                Console.WriteLine("{0} - {1} купюр", countMoney[i, 0], countMoney[i, 1]);
                i++;
            }
            return --i;
        }

        static void giveMoney(int amountLines, int[,] countMoney, int moneyClient, int[] banknotes)
        {
            
            if (moneyClient != 0)
            {
                int j = amountLines;
                moneyClient = moneySelection(countMoney, moneyClient, banknotes, j);
                
                while (moneyClient != 0 )
                {
                    for (int i = 0; i < amountLines + 1; i++)
                    {
                        if (banknotes[i] != 0)
                        {
                            banknotes[i]--;
                            moneyClient += countMoney[i, 0];
                            j = --i;
                            break;
                        }
                    }
                    moneyClient = moneySelection(countMoney, moneyClient, banknotes, j);
                }
                Console.WriteLine("\nВыдано:");
                StreamWriter file = new StreamWriter(@"countMoney1.txt");
                for (int i = 0; i < amountLines + 1; i++)
                {   
                    if (banknotes[i] > 0)
                    {
                        Console.WriteLine("{0}: {1}", countMoney[i, 0], banknotes[i]);
                    }
                    file.WriteLine("{0} - {1}", countMoney[i, 0], countMoney[i, 1]);
                }
                file.Close();
                Console.WriteLine("Ваш остаток: {0} рублей", moneyClient);
            }
            else
                Console.WriteLine("Введена неверная сумма!");
        }

        static int moneySelection(int[,] countMoney, int moneyClient, int[] banknotes, int j)
        {
            for (int i = j; i >= 0; i--)
                {
                    if (moneyClient / countMoney[i, 0] > countMoney[i, 1])
                        banknotes[i] += countMoney[i, 1];
                    else
                        banknotes[i] += moneyClient / countMoney[i, 0];
                    moneyClient -= banknotes[i] * countMoney[i, 0];
                    countMoney[i, 1] -= banknotes[i];
                }
            return moneyClient;
        }

        static void Main(string[] args)
        {
            try
            {
                int[,] countMoney = new int[4, 2];
                int[] banknotes = new int[4] { 0, 0, 0, 0 };
                int amountLines = availableMoney(countMoney);
                Console.WriteLine("\nВведите сумму в рублях:");
                int moneyClient = int.Parse(Console.ReadLine());

                giveMoney(amountLines, countMoney, moneyClient, banknotes);

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
