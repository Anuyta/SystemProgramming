using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProblemsWithoutSynchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> listS = new List<int>();
            List<int> listR = new List<int>();

            var threadA = new Thread(() =>
                {
                    for (int i = 1; i < 101; i++)
                    {
                        Console.WriteLine("пишем элемент {0} в список S", i);
                        listS.Add(i);
                    }
                });

            var threadB = new Thread(() =>
            {
                Console.WriteLine("последний элемент списка S возводим в квадрат и заносим в список R");
                if (listS.Count == 0)
                    Thread.Sleep(1000);
                listR.Add(listS.Last() * listS.Last());
            });

            var threadC = new Thread(() =>
            {
                Console.WriteLine("последний элемент списка S делим на 3 и заносим в список R");
                if(listS.Count != 0)
                    listR.Add((int)(listS.Last() / 3));
            });

            var threadD = new Thread(() =>
            {
                if(listR.Count == 0)
                {
                    Console.WriteLine("В списке R нет элементов");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("последний элемент из списка R = {0}", listR.Last());
            });

            Console.WriteLine("1");

            threadA.Start();
            threadB.Start();
            threadC.Start();
            threadD.Start();

            Console.WriteLine("2");

            Console.ReadLine();
        }
    }
}
