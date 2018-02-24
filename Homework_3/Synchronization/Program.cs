using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> listS = new List<int>();
            List<int> listR = new List<int>();
            var lockerS = new Object();
            var lockerR = new Object();

            var threadA = new Thread(() =>
            {
                for (int i = 1; i < 21; i++)
                {
                    lock (lockerS)
                    {
                        Console.WriteLine("пишем элемент {0} в список S", i);
                        listS.Add(i);
                    }
                }
            });

            //DEADLOCK
            //var threadB = new Thread(() =>
            //{
            //    int count = 0;
            //    int number = 0;
            //    lock (lockerS)
            //    {
            //        count = listS.Count;
            //        if (count == 0)
            //        {
            //            Console.WriteLine("1.Thread.Sleep(1000)");
            //            Thread.Sleep(1000);                        
            //        }
            //        number = listS.Last();
            //        lock (lockerR)
            //        {
            //            Console.WriteLine("последний элемент списка S возводим в квадрат и заносим в список R");
            //            Thread.Sleep(3000);
            //            listR.Add(number * number);
            //        }
            //    }
            //    lock (lockerR)
            //    {
            //        lock (lockerS)
            //        {
            //            Thread.Sleep(3000);
            //            Console.WriteLine("последний элемент из списка S = {0}", listS.Count);  
            //        }
            //    }
            //});

            var threadB = new Thread(() =>
            {
                int count = 0;
                int number = 0;
                lock (lockerS)
                {
                    count = listS.Count;
                }

                if (count == 0)
                {
                    Console.WriteLine("1.Thread.Sleep(1000)");
                    Thread.Sleep(1000);
                }
                lock (lockerS)
                {
                    number = listS.Last();
                    lock (lockerR)
                    {
                        Console.WriteLine("последний элемент списка S возводим в квадрат и заносим в список R");
                        listR.Add(number * number);
                    }
                }
            });

            var threadC = new Thread(() =>
            {
                lock (lockerS)
                {
                    int count = 0;
                    int number = 0;
                    lock (lockerS)
                    {
                        count = listS.Count;
                    }
                    if (count != 0)
                    {
                        lock (lockerS)
                        {
                            number = listS.Last();
                            lock (lockerR)
                            {
                                Console.WriteLine("последний элемент списка S делим на 3 и заносим в список R");
                                listR.Add((int)(number / 3));
                            }
                        }
                    }
                }
            });

            var threadD = new Thread(() =>
            {
                lock (lockerR)
                {
                    if (listR.Count == 0)
                    {
                        Console.WriteLine("В списке R нет элементов");
                        Console.WriteLine("2.Thread.Sleep(1000)");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("последний элемент из списка R = {0}", listR.Last());
                }                
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
