using System;
using System.Threading;
using Sys = Cosmos.System;

namespace CorgiOS.Core
{
    public static class CSM
    {
        public static void OnBoot()
        {
            Console.WriteLine("Hold down key 'C' to boot to corgi system manager.");
            Thread.Sleep(500);
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.C)
                {
                    while (true)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Clear();
                        Console.WriteLine("Corgi system manager:");
                        Console.WriteLine("[F1] Veiw System Info");
                        Console.WriteLine("[F2] System Speed Test");
                        Console.WriteLine("[F3] Boot To Console");
                        Console.WriteLine("[F4] Boot To Butter");
                        Console.WriteLine("[ESC] Shutdown");
                        keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.F1)
                        {
                            Console.Clear();
                            uint RAM = Cosmos.Core.CPU.GetAmountOfRAM();
                            Thread.Sleep(2); // Prevent Deadlock
                            string CPU = Cosmos.Core.CPU.GetCPUBrandString() + " | " + Cosmos.Core.CPU.GetCPUVendorName();
                            Console.WriteLine("System Info:");
                            Console.WriteLine("\tCPU:  " + CPU);
                            Console.WriteLine("\tRAM:  " + RAM + " MB");
                            Console.WriteLine("\tOS:   " + Kernel.Name + " " + Kernel.Version + " " + Kernel.Code);
                            if (Kernel.Name != "CorgiOS")
                                Console.WriteLine("\tBASE: CorgiOS"); // If Your Making A Distro Please Do Not Remove This As I Put Hard Work In To This OS And I Want Credit
                            Console.Write("Press Any Key To Continue.");
                            Console.ReadKey(true);
                        }
                        if (keyInfo.Key == ConsoleKey.F2)
                        {
                            Console.Clear();
                            Console.WriteLine("Starting system speed test...");

                            // Run CPU test
                            long cpuSingalScore = TestCPUSingleCore();
                            Console.WriteLine($"CPU Singal Core Score: {cpuSingalScore} Points");

                            // Run RAM test
                            long ramScore = TestRAM();
                            Console.WriteLine($"RAM Score: {ramScore} Points");

                            // Calculate total score
                            long totalScore = cpuSingalScore + ramScore;
                            Console.WriteLine($"Total Score: {totalScore} Points");

                            Console.Write("Press Any Key To Continue.");
                            Console.ReadKey(true);
                        }
                        if (keyInfo.Key == ConsoleKey.F3)
                        {
                            Kernel.BootingToConsole = true;
                            break;
                        }
                        if (keyInfo.Key == ConsoleKey.F4)
                            break;
                        if (keyInfo.Key == ConsoleKey.Escape)
                            Sys.Power.Shutdown();
                    }
                }
            }
        }
        public static long TestCPUSingleCore()
        {
            long score = 16384;

            DateTime beforeTest = DateTime.Now;

            for (int i = 1; i < 1000000000; i++)
            {
                int TEST1 = (i + 1) * 102 * 920;
                int TEST2 = TEST1 / 102 * (26 % 290 * 600000);
                int TEST3 = TEST2 / (i + 1) * 102 * 920;
                int TEST4 = TEST3 * 102 * 920 * 29470 / 999 * 429;
                int TEST5 = TEST4 * 102 * 920 * 29470 / 999 * 4290;
            }

            DateTime afterTest = DateTime.Now;

            TimeSpan span = afterTest - beforeTest;

            if (span.TotalMilliseconds == 0)
            {
                span = TimeSpan.FromMilliseconds(40);
            }

            return (long)(score * 10000 / span.TotalMilliseconds);
        }

        private static long TestRAM()
        {
            return Cosmos.Core.CPU.GetAmountOfRAM() * 4;
        }
    }
}
