using CorgiOS.Resources;
using Cosmos.System;
using Cosmos.System.Graphics;
using Sys = Cosmos.System;
using System.Drawing;
using CorgiOS.Drivers;
using System;
using CorgiOS.Programs;
using System.Threading;

namespace CorgiOS.Graphics
{
    public static class GUI
    {
        public static uint ScreenWidth { get; private set; }
        public static uint ScreenHeight { get; private set; }
        public static SVGAIICanvas canvas;

        public static void StartGUI(uint Width = 1920, uint Height = 1080)
        {
            canvas = new SVGAIICanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));
            ScreenWidth = Width;
            ScreenHeight = Height;
            MouseManager.ScreenWidth = ScreenWidth;
            MouseManager.ScreenHeight = ScreenHeight;
            MouseManager.X = ScreenWidth / 2;
            MouseManager.Y = ScreenHeight / 2;
            canvas.DrawFilledRectangle(Color.Black, 0, 0, (int)ScreenWidth, (int)ScreenHeight);
            canvas.DrawImage(Resource.Logo, (int)ScreenWidth / 2 - 128, (int)ScreenHeight / 2 - 128);
            Audio.playSound(Resource.StartupAudioRAW);
            ShowLoadingAnimation();
        }

        public static void UpdateGUI()
        {
            if (ScreenWidth == 1920 && ScreenHeight == 1080)
                canvas.DrawImage(Resource.Bliss, 0, 0);
            else
                canvas.Clear(Color.Black);

            UI.DrawTaskbar();
            UI.UpdateTasks();
            UpdateMouse();
            canvas.Display();
        }

        public static void UpdateMouse()
        {
            // Default XP Cursor

            /*
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 1);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 1, (int)MouseManager.Y + 1);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 2);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 2);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 2, (int)MouseManager.Y + 2);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 3, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 4, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 5, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 8, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 9, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 9, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 10, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 3, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 2, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 4, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 1, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 4, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 5, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 5, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 16);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 16);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 8, (int)MouseManager.Y + 16);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 9, (int)MouseManager.Y + 16);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 17);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 17);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 8, (int)MouseManager.Y + 17);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 9, (int)MouseManager.Y + 17);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 18);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 18);
            */

            // Default XP Curosr With Short Tail

            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 1);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 1, (int)MouseManager.Y + 1);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 2);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 2);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 2, (int)MouseManager.Y + 2);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 3, (int)MouseManager.Y + 3);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 4, (int)MouseManager.Y + 4);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 5, (int)MouseManager.Y + 5);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 6);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 7);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 8);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 8, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 9, (int)MouseManager.Y + 9);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 3, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 9, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 10, (int)MouseManager.Y + 10);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 2, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 3, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 4, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 11);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 1, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 2, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 4, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 12);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 1, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 4, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 5, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 13);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 5, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 14);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 5, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 6, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.White, (int)MouseManager.X + 7, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 8, (int)MouseManager.Y + 15);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 6, (int)MouseManager.Y + 16);
            canvas.DrawPoint(Color.Black, (int)MouseManager.X + 7, (int)MouseManager.Y + 16);
        }

        public static void ShowLoadingAnimation()
        {
            const int dotRadius = 4; // Radius of dots
            const int animationRadius = 50; // Radius of the circular animation path
            const int numDots = 8; // Number of dots
            int centerX = (int)ScreenWidth / 2; // Center X of the screen
            int centerY = (int)ScreenHeight / 2 + 200; // Center Y below the logo
            const int delay = 45; // Milliseconds between frames

            double angleStep = 2 * Math.PI / numDots;
            double[] dotAngles = new double[numDots];

            for (int i = 0; i < numDots; i++)
            {
                dotAngles[i] = i * angleStep; // Initialize dot positions
            }

            int ctr = 0;

            while (ctr != 50)
            {
                // Clear the loading area
                canvas.DrawFilledRectangle(Color.Black, 0, centerY - animationRadius - 10, (int)ScreenWidth, 2 * animationRadius + 20);

                // Draw each dot
                for (int i = 0; i < numDots; i++)
                {
                    int dotX = centerX + (int)(animationRadius * Math.Cos(dotAngles[i]));
                    int dotY = centerY + (int)(animationRadius * Math.Sin(dotAngles[i]));

                    canvas.DrawFilledEllipse(Color.White, dotX - dotRadius, dotY - dotRadius, dotRadius, dotRadius);
                    dotAngles[i] += 0.1; // Move dot position
                }

                canvas.Display();
                Thread.Sleep(delay);
                ctr++;
            }
        }
    }
}
