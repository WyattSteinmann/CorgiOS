using System;
using System.Drawing;
using System.Threading;
using CorgiOS.Core;
using CorgiOS.Drivers;
using CorgiOS.Graphics.JXWS;
using CorgiOS.Programs;
using CorgiOS.Resources;
using Cosmos.Core;
using Cosmos.System;
using Cosmos.System.Graphics;

namespace CorgiOS.Graphics
{
    public static class UI
    {
        private static MouseState prevMouseState = MouseState.None;
        private static bool Start = false;

        public static void DrawTaskbar()
        {
            CustomDrawing.DrawFullRoundedRectangle(10, (int)GUI.ScreenHeight - 40, (int)GUI.ScreenWidth - 20, 30, 12, Color.FromArgb(50, 50, 255));
            CustomDrawing.DrawFullRoundedRectangle(10, (int)GUI.ScreenHeight - 40, (int)GUI.ScreenWidth / 28, 30, 12, Color.FromArgb(20, 255, 50));
            GUI.canvas.DrawString("Start", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), 26, (int)GUI.ScreenHeight - 32);
        }
        
        public static void UpdateTasks()
        {
            Window.doUpdate();
            Rectangle mousePos = new Rectangle((int)MouseManager.X, (int)MouseManager.Y, 1, 1);
            KeyEvent keyData;
            if (KeyboardManager.TryReadKey(out keyData))
            {
                if (keyData.Key == ConsoleKeyEx.LWin || keyData.Key == ConsoleKeyEx.RWin)
                    Start = !Start;
                else
                    Window.processKeyPress(keyData);
            }

            if (Start)
            {
                CustomDrawing.DrawFullRoundedRectangle(10, (int)GUI.ScreenHeight - 400, (int)GUI.ScreenWidth / 10, 350, 12, Color.FromArgb(50, 50, 255));
                CustomDrawing.DrawFullRoundedRectangle(15, (int)GUI.ScreenHeight - 395, (int)GUI.ScreenWidth / 10 - 10, 30, 12, Color.FromArgb(50, 255, 50));
                GUI.canvas.DrawString("Terminal", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), 35, (int)GUI.ScreenHeight - 388);
                CustomDrawing.DrawFullRoundedRectangle(15, (int)GUI.ScreenHeight - 360, (int)GUI.ScreenWidth / 10 - 10, 30, 12, Color.FromArgb(50, 255, 50));
                GUI.canvas.DrawString("Notepad", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), 35, (int)GUI.ScreenHeight - 353);
                CustomDrawing.DrawFullRoundedRectangle(15, (int)GUI.ScreenHeight - 325, (int)GUI.ScreenWidth / 10 - 10, 30, 12, Color.FromArgb(50, 255, 50));
                GUI.canvas.DrawString("Task Manager", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), 35, (int)GUI.ScreenHeight - 318);
                CustomDrawing.DrawFullRoundedRectangle(15, (int)GUI.ScreenHeight - 125, (int)GUI.ScreenWidth / 10 - 10, 30, 12, Color.FromArgb(255, 50, 50));
                GUI.canvas.DrawString("Shutdown", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), 35, (int)GUI.ScreenHeight - 118);
                CustomDrawing.DrawFullRoundedRectangle(15, (int)GUI.ScreenHeight - 90, (int)GUI.ScreenWidth / 10 - 10, 30, 12, Color.FromArgb(20, 255, 20));
                GUI.canvas.DrawString("Restart", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.FromArgb(255, 255, 255), 35, (int)GUI.ScreenHeight - 83);
            }

            Window.drawWindows();

            if (Start)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    if (prevMouseState != MouseState.Left)
                    {
                        if (mousePos.IntersectsWith(new Rectangle(15, (int)GUI.ScreenHeight - 125, (int)GUI.ScreenWidth / 10 - 10, 30)))
                        {
                            GUI.canvas.DrawFilledRectangle(Color.Black, 0, 0, (int)GUI.ScreenWidth, (int)GUI.ScreenHeight);
                            GUI.canvas.DrawImage(Resource.Logo, (int)GUI.ScreenWidth / 2 - 128, (int)GUI.ScreenHeight / 2 - 128);
                            GUI.canvas.Display();
                            Audio.playSound(Resource.StartupAudioRAW);
                            GUI.ShowLoadingAnimation();
                            ACPIManager.Shutdown();
                        }
                        if (mousePos.IntersectsWith(new Rectangle(15, (int)GUI.ScreenHeight - 90, (int)GUI.ScreenWidth / 10 - 10, 30)))
                        {
                            GUI.canvas.DrawFilledRectangle(Color.Black, 0, 0, (int)GUI.ScreenWidth, (int)GUI.ScreenHeight);
                            GUI.canvas.DrawImage(Resource.Logo, (int)GUI.ScreenWidth / 2 - 128, (int)GUI.ScreenHeight / 2 - 128);
                            GUI.canvas.Display();
                            Audio.playSound(Resource.StartupAudioRAW);
                            GUI.ShowLoadingAnimation();
                            ACPIManager.Reboot();
                        }
                        if (mousePos.IntersectsWith(new Rectangle(15, (int)GUI.ScreenHeight - 395, (int)GUI.ScreenWidth / 10 - 10, 30)))
                        {
                            Start = false;
                            new Terminal(Window.windows.Count * 25, Window.windows.Count * 25, (int)GUI.ScreenWidth / 4, (int)GUI.ScreenHeight / 4);
                        }
                        if (mousePos.IntersectsWith(new Rectangle(15, (int)GUI.ScreenHeight - 360, (int)GUI.ScreenWidth / 10 - 10, 30)))
                        {
                            Start = false;
                            new Notepad(Window.windows.Count * 25, Window.windows.Count * 25, (int)GUI.ScreenWidth / 2, (int)GUI.ScreenHeight / 2);
                        }
                        if (mousePos.IntersectsWith(new Rectangle(15, (int)GUI.ScreenHeight - 325, (int)GUI.ScreenWidth / 10 - 10, 30)))
                        {
                            Start = false;
                            new TaskManager(Window.windows.Count * 25, Window.windows.Count * 25, (int)GUI.ScreenWidth / 2, (int)GUI.ScreenHeight / 2);
                        }
                    }
                }
            }
            
            if (MouseManager.MouseState == MouseState.Left)
            {
                if (prevMouseState != MouseState.Left)
                {
                    if (mousePos.IntersectsWith(new Rectangle(10, (int)GUI.ScreenHeight - 40, (int)GUI.ScreenWidth / 28, 30)))
                    {
                        Start = !Start;
                    }
                    Window.tryWindowLMBDown();
                }
            }

            if (MouseManager.MouseState == MouseState.None)
            {
                if (prevMouseState == MouseState.Left)
                {
                    Window.tryWindowLMBUp();
                }
            }

            prevMouseState = MouseManager.MouseState;
        }
    }
}
