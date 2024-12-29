using Cosmos.Debug.Kernel.Plugs.Asm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Graphics.JXWS
{
    public class Pixel : WindowComponent
    {
        public Color color;

        public Pixel(int x, int y, Color color) : base(x, y)
        {
            this.color = color;
        }

        public override void Draw(Window sender)
        {
            GUI.canvas.DrawPoint(this.color, this.X + sender.X, this.Y + sender.Y + Window.WindowTopSize);
        }
    }
}
