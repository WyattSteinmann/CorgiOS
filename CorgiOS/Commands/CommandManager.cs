using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Commands
{
    public class CommandManager
    {
        private List<Command> commands;

        public CommandManager()
        {
            commands = new List<Command>(10);
            commands.Add(new Help("help"));
            commands.Add(new Power("power"));
            commands.Add(new Neofetch("neofetch"));
            commands.Add(new Clear("cls"));
            commands.Add(new Echo("echo"));
            commands.Add(new Calc("calc"));
            commands.Add(new Time("time"));
            commands.Add(new File("file"));
            commands.Add(new OpenGUI("butter"));
            commands.Add(new OpenMIV("miv"));
        }
        
        public string processInput(string input)
        {
            input = input.Trim();
            string[] split = input.Split(' ');
            List<string> args = new List<string>();

            int ctr = 0;
            foreach (string arg in split)
            {
                if (ctr != 0)
                    args.Add(arg);
                ctr++;
            }

            foreach (Command cmd in commands)
            {
                if (cmd.name.ToLower() == split[0].ToLower())
                    return cmd.Execute(args.ToArray());
            }

            return "Your command \"" + split[0] + "\" does not exist!";
        }
    }
}