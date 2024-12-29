using CorgiOS.Core;
using CorgiOS.Graphics;
using CorgiOS.Programs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Commands
{
    public class OpenMIV : Command
    {
        public OpenMIV(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            if (Kernel.GUIOpen)
                return "MIV is not supported for butter.";
            MIV.StartMIV();
            return "";
        }
    }
}
