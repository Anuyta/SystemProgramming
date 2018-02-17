using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        private const string notRunningProcess = "Данный процесс не запущен";

        static void Main(string[] args)
        {
            GiveChoiseToUser();
            Console.ReadLine();
        }

        private static void GiveChoiseToUser()
        {
            while (true)
            {
                ShowMenu();
                string str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                        {
                            GetAllProcess();
                            break;
                        }
                    case "2":
                        {
                            GetProcessByPID();
                            break;
                        }
                    case "3":
                        {
                            StartProcess();
                            break;
                        }
                    case "4":
                        {
                            CloseProcess();
                            break;
                        }
                    case "5":
                        {
                            ShowThreadsInfo();
                            break;
                        }
                    case "6":
                        {
                            ShowModulesInfo();
                            break;
                        }
                    case "7": { Console.WriteLine("Press any key to exit.."); return; }
                    default:
                        Console.WriteLine("bad choice");
                        break;
                }

            }
            
        }

        private static void ShowModulesInfo()
        {
            string nameProc = GetProcessName();
            try
            {
                var proc = Process.GetProcessesByName(nameProc)[0];
                var modules = proc.Modules;

                foreach (ProcessModule module in modules)
                {
                    Console.WriteLine("Name: {0}  MemorySize: {1}",
                                module.ModuleName, module.ModuleMemorySize);
                }
            }
            catch (Exception)
            {
                Console.WriteLine(notRunningProcess);
            }
        }

        private static void ShowThreadsInfo()
        {
            string nameProc = GetProcessName();
            try
            {
                var proc = Process.GetProcessesByName(nameProc)[0];
                ProcessThreadCollection threads = proc.Threads;
                Console.WriteLine("Количество потоков для данного процесса = {0}", threads.Count);
            }
            catch (Exception)
            {
                Console.WriteLine(notRunningProcess);
            }
        }           

        private static void CloseProcess()
        {
            string nameProc = GetProcessName();
            try
            {
                var proc = Process.GetProcessesByName(nameProc)[0];
                proc.CloseMainWindow();
                proc.Close();
                Console.WriteLine("Процесс остановлен");
            }
            catch (Exception)
            {
                Console.WriteLine(notRunningProcess);
            }
        }

        private static void StartProcess()
        {
            string nameProc = GetProcessName();
            try
            {
                Process.Start(nameProc);
            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось запустить процесс");
            }
        }

        private static void GetProcessByPID()
        {
            Console.WriteLine("Введите PID:");
            int pid = -1;
            int.TryParse(Console.ReadLine(), out pid);
            if (pid != -1)
            {
                try
                {
                    var process = Process.GetProcessById(pid);
                    Console.WriteLine(string.Format(@"Process name: {0}", process.ProcessName));
                }
                catch (Exception)
                {
                    Console.WriteLine("Процесса с таким PID нет в системе");
                }
            }
        }

        private static void GetAllProcess()
        {
            var process = Process.GetProcesses(".").Select(proc => proc).OrderBy(proc => proc.Id);
            int i = 0;
            foreach (var proc in process)
            {
                i++;
                string info = string.Format(@"{0}: PID: {1} Name: {2}", i, proc.Id, proc.ProcessName);
                Console.WriteLine(info);
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n1.Список всех процессов");
            Console.WriteLine("2.Выбрать процесс по PID(вывести инфо)");
            Console.WriteLine("3.Запустить процесс");
            Console.WriteLine("4.Оставить процесс");
            Console.WriteLine("5.Показать информацию о потоках");
            Console.WriteLine("6.Показать информацию о модулях");
            Console.WriteLine("7.Выход");
        }

        private static string GetProcessName()
        {
            Console.WriteLine("Введите название процесса");
            string nameProc = Console.ReadLine();
            return nameProc;
        }
    }
}
