using CorgiOS.Commands;
using System;
using Cosmos.System.FileSystem;
using Sys = Cosmos.System;
using Cosmos.Core.Memory;
using CorgiOS.Graphics;
using Cosmos.System;
using System.Threading;
using CorgiOS.Programs;
using CorgiOS.Drivers;

namespace CorgiOS.Core
{
    public class Kernel : Sys.Kernel
    {
        public static string Name = "CorgiOS", Version = "0.0.1", Code = "Jaguar", Path = "0:\\";
        private CommandManager commandManager;
        public static CosmosVFS vfs;
        public static int lastHeapCollect;
        public static bool GUIOpen;
        public static bool BootingToConsole;

        protected override void BeforeRun()
        {
            Kernel.BootingToConsole = false;
            System.Console.BackgroundColor = ConsoleColor.DarkCyan;
            System.Console.WriteLine("Booting " + Kernel.Name + " " + Kernel.Version + " " + Kernel.Code);
            System.Console.WriteLine("Loaded CSM.");
            CSM.OnBoot();
            vfs = new CosmosVFS();
            System.Console.WriteLine("Loaded VFS Part 1 of 2.");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(vfs);
            System.Console.WriteLine("Loaded VFS Part 2 of 2.");
            lastHeapCollect = 0;
            if (BootingToConsole)
            {
                commandManager = new CommandManager();
                System.Console.WriteLine("Loaded Command Manager.");
                GUIOpen = false;
            }
            else
            {
                GUI.StartGUI();
                Kernel.GUIOpen = true;
            }
            System.Console.OutputEncoding = Sys.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
            System.Console.Clear();
            System.Console.WriteLine(
                " ██████╗ ██████╗ ██████╗  ██████╗ ██╗     ██████╗ ███████╗\n" +
                "██╔════╝██╔═══██╗██╔══██╗██╔════╝ ██║    ██╔═══██╗██╔════╝\n" +
                "██║     ██║   ██║██████╔╝██║  ███╗██║    ██║   ██║███████╗\n" +
                "██║     ██║   ██║██╔══██╗██║   ██║██║    ██║   ██║╚════██║\n" +
                "╚██████╗╚██████╔╝██║  ██║╚██████╔╝██║    ╚██████╔╝███████║\n" +
                " ╚═════╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═════╝ ╚══════╝\n"
            );

        }

        protected override void Run()
        {
            try
            {
                if (lastHeapCollect == 20)
                {
                    Heap.Collect();
                    lastHeapCollect = 0;
                }
                else
                    lastHeapCollect++;
                if (GUIOpen)
                    GUI.UpdateGUI();
                else
                {
                    System.Console.Write("$ ");
                    System.Console.WriteLine(commandManager.processInput(System.Console.ReadLine()));
                }
            }
            catch (Exception ex)
            {
                ShellX.BSOD(255, ex.Message); // 255 Means Unknown Fault
            }
        }
    }
}
