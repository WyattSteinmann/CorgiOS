using CorgiOS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Commands
{
    public class Power : Command
    {
        public Power(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            if (args.Length != 1)
                return "Did not give a tag.";
            if (args[0].ToLower() == "-s" || args[0].ToLower() == "/s")
                return ACPIManager.Shutdown();
            if (args[0].ToLower() == "-r" || args[0].ToLower() == "/r")
                return ACPIManager.Reboot();
            else
                return "Invaild tag.";
        }
    }
}
