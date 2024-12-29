using CorgiOS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Commands
{
    public class Time : Command
    {
        public Time(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            DateTime localTime = DateTime.Now;
            return "Local Date & Time: " + localTime;
        }
    }
}
