using CorgiOS.Core;
using CorgiOS.Graphics;

namespace CorgiOS.Commands
{
    public class OpenGUI : Command
    {
        public OpenGUI(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            if (Kernel.GUIOpen)
                return "GUI Is Allready Open.";
            GUI.StartGUI();
            Kernel.GUIOpen = true;
            return "Done.";
        }
    }
}
