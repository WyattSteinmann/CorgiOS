using Cosmos.System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace CorgiOS.Graphics.JXWS
{
    public class Window
    {
        public int WindowWidth, WindowHeight;
        public string Title;
        public const int WindowTopSize = 25;
        public static List<Window> windows = new List<Window>();
        protected List<WindowComponent> components;
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool focused { get; private set; }
        private protected bool beingDragged;
        private protected int offsetX, offsetY;

        public Window(int x, int y, int width, int height, string title)
        {
            this.X = x;
            this.Y = y;
            this.WindowWidth = width;
            this.WindowHeight = height;
            if (this.X > GUI.ScreenWidth - width)
                this.X = (int)GUI.ScreenWidth - width;
            if (this.X < 0)
                this.X = 0;
            if (this.Y > GUI.ScreenHeight - height)
                this.Y = (int)GUI.ScreenHeight - height;
            if (this.Y < 0)
                this.Y = 0;
            this.Title = title;
            this.components = new List<WindowComponent>();
            this.focus();
            Window.windows.Add(this);
        }

        private void Draw()
        {
            CustomDrawing.DrawFullRoundedRectangle(this.X, this.Y, this.WindowWidth, this.WindowHeight, Window.WindowTopSize / 2 - 2, Color.FromArgb(80, 80, 80));
            if (this.focused)
                CustomDrawing.DrawTopRoundedRectangle(this.X, this.Y, this.WindowWidth, Window.WindowTopSize, Window.WindowTopSize / 2 - 2, Color.FromArgb(40, 40, 255));
            else
                CustomDrawing.DrawTopRoundedRectangle(this.X, this.Y, this.WindowWidth, Window.WindowTopSize, Window.WindowTopSize / 2 - 2, Color.FromArgb(50, 50, 255));
            CustomDrawing.DrawFullRoundedRectangle(this.X + (this.WindowWidth - Window.WindowTopSize), this.Y, Window.WindowTopSize, Window.WindowTopSize, Window.WindowTopSize / 2 - 2, Color.FromArgb(255, 40, 40));
            GUI.canvas.DrawString("X", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), this.X + (this.WindowWidth - Window.WindowTopSize) + Window.WindowTopSize / 3, this.Y + Window.WindowTopSize / 5);
            GUI.canvas.DrawString(this.Title, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), this.X + 15, this.Y + 5);
            foreach (WindowComponent component in this.components)
                component.Draw(this);
        }

        public static void drawWindows()
        {
            if (Window.windows.Count == 0) return;

            foreach (Window win in Window.windows)
                if (!win.focused)
                    win.Draw();
            foreach (Window win in Window.windows)
                if (win.focused)
                {
                    win.Draw();
                    break;
                }
        }

        public void addComponent(WindowComponent windowComponent)
        {
            this.components.Add(windowComponent);
        }

        public void Move(int x, int y)
        {
            this.X = x;
            this.Y = y;

            if (this.X > GUI.ScreenWidth - this.WindowWidth)
                this.X = (int)GUI.ScreenWidth - this.WindowWidth;
            if (this.X < 0)
                this.X = 0;
            if (this.Y > GUI.ScreenHeight - this.WindowHeight)
                this.Y = (int)GUI.ScreenHeight - this.WindowHeight;
            if (this.Y < 0)
                this.Y = 0;
        }

        public void Close()
        {
            Window.windows.Remove(this);
        }

        public void focus()
        {
            foreach (Window win in Window.windows)
            {
                if (win.focused)
                {
                    win.focused = false;
                    break;
                }
            }

            this.focused = true;
        }

        public static void tryWindowLMBDown()
        {
            if (Window.windows.Count == 0) return;

            foreach (Window win in Window.windows)
            {
                Rectangle mousePos = new Rectangle((int)MouseManager.X, (int)MouseManager.Y, 1, 1);
                if (mousePos.IntersectsWith(new Rectangle(win.X, win.Y, win.WindowWidth, win.WindowHeight)))
                {
                    win.focus();
                }
                if (mousePos.IntersectsWith(new Rectangle(win.X + (win.WindowWidth - Window.WindowTopSize), win.Y, Window.WindowTopSize, Window.WindowTopSize)))
                {
                    win.Close();
                }
                else if (mousePos.IntersectsWith(new Rectangle(win.X, win.Y, win.WindowWidth, Window.WindowTopSize)))
                {
                    win.offsetX = (int)MouseManager.X - win.X;
                    win.offsetY = (int)MouseManager.Y - win.Y;
                    win.beingDragged = true;
                }
            }
        }

        public static void tryWindowLMBUp()
        {
            if (Window.windows.Count == 0) return;

            foreach (Window win in Window.windows)
            {
                if (win.beingDragged)
                {
                    win.beingDragged = false;
                    win.Move((int)MouseManager.X - win.offsetX, (int)MouseManager.Y - win.offsetY);
                }
            }
        }

        public virtual void onKeyPress(Cosmos.System.KeyEvent keyData)
        {

        }

        public virtual void onUpdate()
        {

        }

        public static void processKeyPress(Cosmos.System.KeyEvent keyData)
        {
            if (Window.windows.Count == 0) return;

            foreach (Window win in Window.windows)
            {
                if (win.focused)
                    win.onKeyPress(keyData);
            }
        }

        public static void doUpdate()
        {
            if (Window.windows.Count == 0) return;

            foreach (Window win in Window.windows)
            {
                win.onUpdate();
            }
        }
    }
}
