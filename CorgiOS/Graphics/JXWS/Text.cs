using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Graphics.JXWS
{
    public class Text : WindowComponent
    {
        public string text;
        public Color color;
        public bool multiline;

        public Text(int x, int y, string text, Color color, bool multiline = true) : base(x, y)
        {
            this.text = text;
            this.color = color;
            this.multiline = multiline;
        }

        public override void Draw(Window sender)
        {
            int ctrX = 0, ctrY = 0;
            foreach (char c in this.text)
            {
                if (c == ' ')
                {
                    ctrX += Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Width;
                    continue;
                }
                if (c == '\t')
                {
                    ctrX += Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Width * 4;
                    continue;
                }
                if (c == '\n')
                {
                    ctrY += Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Height;
                    ctrX = 0;
                    continue;
                }

                if (ctrX + Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Width * 2 > sender.WindowWidth - this.X)
                {
                    if (!this.multiline) break;

                    if (ctrY + Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Height * 3 > sender.WindowHeight - Window.WindowTopSize - this.Y) break;

                    ctrY += Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Height;
                    ctrX = 0;
                }

                GUI.canvas.DrawString(c.ToString(), Cosmos.System.Graphics.Fonts.PCScreenFont.Default, this.color, ctrX + sender.X + this.X, ctrY + sender.Y + Window.WindowTopSize + this.Y);
                ctrX += Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Width;
            }
        }
    }
}
