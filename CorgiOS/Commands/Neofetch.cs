using CorgiOS.Core;
using System.Threading;

namespace CorgiOS.Commands
{
    public class Neofetch : Command
    {
        public Neofetch(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            uint RAM = Cosmos.Core.CPU.GetAmountOfRAM();
            Thread.Sleep(2); // Prevent Deadlock
            string CPU = Cosmos.Core.CPU.GetCPUBrandString() + " | " + Cosmos.Core.CPU.GetCPUVendorName();
            return "Neofetch:" + "\n\tSystem OS: " + Kernel.Name + " " + Kernel.Version + " " + Kernel.Code + "\n\tSystem RAM: " + RAM + " MB" + "\n\tSystem CPU: " + CPU;
        }
    }
}
