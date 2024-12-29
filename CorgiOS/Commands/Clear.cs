using CorgiOS.Core;
using CorgiOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Commands
{
    public class Clear : Command
    {
        public Clear(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            if (!Kernel.GUIOpen)
                Console.Clear();
            return "";
        }
    }
}
