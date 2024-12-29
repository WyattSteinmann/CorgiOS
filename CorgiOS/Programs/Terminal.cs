using CorgiOS.Commands;
using CorgiOS.Core;
using CorgiOS.Graphics.JXWS;
using System.Drawing;

namespace CorgiOS.Programs
{
    public class Terminal : Window
    {
        public Text outputText, inputText;
        public CommandManager commandManager;

        public Terminal(int x, int y, int width, int height) : base(x, y, width, height, "Terminal")
        {
            this.outputText = new Text(0, 0, "Welcome To " + Kernel.Name + " Terminal!", Color.FromArgb(255, 255, 255));
            this.addComponent(this.outputText);
            this.inputText = new Text(0, height - (Window.WindowTopSize + Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Height + 1), "", Color.FromArgb(255, 255, 255), false);
            this.addComponent(this.inputText);
            this.commandManager = new CommandManager();
        }

        public override void onKeyPress(Cosmos.System.KeyEvent keyData)
        {
            if (keyData.Key == Cosmos.System.ConsoleKeyEx.Enter || keyData.Key == Cosmos.System.ConsoleKeyEx.NumEnter)
            {
                if (this.inputText.text.ToLower().StartsWith("exit"))
                    this.Close();
                this.outputText.text = this.commandManager.processInput(this.inputText.text);
                this.inputText.text = "";
            }
            else if (keyData.Key == Cosmos.System.ConsoleKeyEx.Backspace)
            {
                this.inputText.text = this.inputText.text.Remove(this.inputText.text.Length - 1, 1);
            }
            else
            {
                this.inputText.text += keyData.KeyChar;
            }
        }
    }
}
