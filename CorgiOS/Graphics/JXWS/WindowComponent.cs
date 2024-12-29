using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiOS.Graphics.JXWS
{
    public class WindowComponent
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public WindowComponent(int x, int y)
        {
            X = x;
            Y = y;
        }

        public virtual void Draw(Window sender)
        {

        }
    }
}
