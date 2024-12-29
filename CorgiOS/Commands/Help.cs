using CorgiOS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Commands
{
    public class Help : Command
    {
        public Help(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            if (args.Length != 1)
            {
                if (!Kernel.GUIOpen)
                    return "Available commands: help, power, neofetch, cls, echo, calc, time, file, butter, miv, run";
                else
                    return "Available commands: help, power, neofetch, cls, echo, calc, time, file, butter, miv, run, exit";
            }
            if (args[0].ToLower() == "help")
                return "Gives help about the operating system.";
            if (args[0].ToLower() == "power")
                return "Shutdown or restart the system. Usage: use tag '-s' to shutdown, use tag '-r' to restart";
            if (args[0].ToLower() == "neofetch")
                return "Grabs system info.";
            if (args[0].ToLower() == "cls")
                return "Clears the screen.";
            if (args[0].ToLower() == "echo")
                return "Echos text back to screen useful for testing. Usage: <Text>";
            if (args[0].ToLower() == "calc")
                return "Calculate 2 numbers. Usage: <num> <op> <num>";
            if (args[0].ToLower() == "time")
                return "Get the time.";
            if (args[0].ToLower() == "file")
                return "Basic file system stuff Usage: 'file <Command>' or 'file <Command> <Path>' or 'file <Command> <path> <text>' Need help? Check the github page.";
            if (args[0].ToLower() == "butter")
                return "Open the butter desktop environment.";
            if (args[0].ToLower() == "miv")
                return "Open the miv text editor.";
            if (args[0].ToLower() == "exit" && Kernel.GUIOpen)
                return "Exit the terminal.";
            if (args[0].ToLower() == "run")
                return "Run Ferret Apps.";
            else
                return "Help can not give help about command's that does not exist.";
        }
    }
}
