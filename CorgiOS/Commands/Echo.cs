using CorgiOS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Commands
{
    public class Echo : Command
    {
        public Echo(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string arg in args)
                sb.Append(arg + " ");

            return sb.ToString().Substring(0, sb.Length - 1);
        }
    }
}
