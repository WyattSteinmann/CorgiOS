using CorgiOS.Core;
using CorgiOS.Graphics;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using System;
using System.Drawing;
using System.Threading;

namespace CorgiOS.Programs
{
    public class ShellX
    {
        public static void BSOD(byte code, string info) // Black Screen Of Death
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine("Corgi OS Has Crashed Due To Unexpected Error.\nWe Are Sorry For The Error We Tryed Hard To Prevent This In Programing.\nWe Hope You Did Not Lose Any Work.\nYou Can Support This Project To Prevent Errors Like This.\nThis Error Came From File ShellX.cs From The SRC Code.\n\nError Code: " + code + "\nInfo: " + info);
            Drivers.Audio.PlayBeep(1000, 1000);
            Thread.Sleep(10000);
            ACPIManager.Reboot();
        }
    }
}
