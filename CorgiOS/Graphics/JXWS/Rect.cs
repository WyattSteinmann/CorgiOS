using Cosmos.Debug.Kernel.Plugs.Asm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Graphics.JXWS
{
    public class Rect : WindowComponent
    {
        public Color color;
        public int width, height;
        public bool fullRounded, topRounded, filled;

        public Rect(int x, int y, int width, int height, Color color, bool filled = true, bool fullRounded = false, bool topRounded = false) : base(x, y)
        {
            this.color = color;
            this.width = width;
            this.height = height;
            this.fullRounded = fullRounded;
            this.topRounded = topRounded;
            this.filled = filled;
        }

        public override void Draw(Window sender)
        {
            if (filled)
                GUI.canvas.DrawFilledRectangle(this.color, this.X + sender.X, this.Y + sender.Y + Window.WindowTopSize, this.width, this.height);
            else if (filled & fullRounded)
                CustomDrawing.DrawFullRoundedRectangle(this.X + sender.X, this.Y + sender.Y + Window.WindowTopSize, this.width, this.height, this.height / 2 - 2, this.color);
            else if (filled & topRounded)
                CustomDrawing.DrawFullRoundedRectangle(this.X + sender.X, this.Y + sender.Y + Window.WindowTopSize, this.width, this.height, this.height / 2 - 2, this.color);
            else if (!filled)
                GUI.canvas.DrawRectangle(this.color, this.X + sender.X, this.Y + sender.Y + Window.WindowTopSize, this.width, this.height);
        }
    }
}
