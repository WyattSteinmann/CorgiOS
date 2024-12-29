using CorgiOS.Graphics.JXWS;
using System.Drawing;
using System.Threading;

namespace CorgiOS.Programs
{
    public class TaskManager : Window
    {
        public Text outputText, processList;
        public uint RAM;
        public string CPU;

        public TaskManager(int x, int y, int width, int height) : base(x, y, width, height, "Task Manager")
        {
            this.RAM = Cosmos.Core.CPU.GetAmountOfRAM();
            Thread.Sleep(2); // Prevent Deadlock
            this.CPU = Cosmos.Core.CPU.GetCPUBrandString() + " | " + Cosmos.Core.CPU.GetCPUVendorName();
            this.outputText = new Text(0, 0, "", Color.FromArgb(255, 255, 255));
            this.processList = new Text(0, Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Height * 2 + 1, "", Color.FromArgb(255, 255, 255));
            this.components.Add(this.outputText);
            this.components.Add(this.processList);
        }

        public override void onUpdate()
        {
            int total = Window.windows.Count + 1;
            this.outputText.text = "Processes: " + Window.windows.Count + "\nSystemProcesses: 1\nTotalProcesses: " + total + "\nCPU: " + this.CPU + "\nRAM: " + this.RAM;
            this.processList.text = "\n\n\nProcessList:\nButter Desktop_ENV\n";
            foreach (Window win in Window.windows)
                this.processList.text += win.Title + "\n";
        }
    }
}
