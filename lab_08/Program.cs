using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_8_zadanie_v2
{
    class Program
    {
        static void Main(string[] args)
        {

            Task task = Search();
            task.Wait();

        }

        async static Task Search()

        {
            Dictionary<int, HashSet<int>> Set = new();
            int x = 0, i = 0;
            bool TimeOut = false;


            Thread Timer = new(() =>{ Thread.Sleep(1000);TimeOut = true; });
            Timer.Start();


            while (!TimeOut)
            {
                Task<bool> task1 = DoTask(i + 0);
                Task<bool> task2 = DoTask(i + 1);
                Task<bool> task3 = DoTask(i + 2);
                Task<bool> task4 = DoTask(i + 3);

                if (await task1)
                {
                    ++x;
                    Set.Add(x, new HashSet<int>(x));
                }

                if (await task2)
                {
                    ++x;
                    Set.Add(x, new HashSet<int>(x));
                }

                if (await task3)
                {
                    ++x;
                    Set.Add(x, new HashSet<int>(x));
                }

                if (await task4)
                {
                    ++x;
                    Set.Add(x, new HashSet<int>(x));
                }

                i++;
            }


            Console.WriteLine(Set.Count);

        }

        private static async Task<bool> DoTask(int taskId)
        {
            return await Task<bool>.Run(() =>
            {
                return isPrimeNumber(taskId);
            });
        }

        static bool isPrimeNumber(int number)
        {

            if (number < 2)
                return false;


            if (number == 2 || number == 3)
                return true;


            if (number % 2 == 0)
                return false;



            int limit = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 5, u = 4; i <= limit; i += u)
            {
                if (number % i == 0)
                    return false;

                u = 6 - u;
            }
            return true;
        }
    }
}