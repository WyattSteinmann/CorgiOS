using System.Drawing;

namespace CorgiOS.Graphics
{
	public static class CustomDrawing
	{
		public static void DrawFullRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
		{
			GUI.canvas.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
			GUI.canvas.DrawFilledRectangle(col, x, y + radius, radius, height - 2 * radius);
			GUI.canvas.DrawFilledRectangle(col, x + width - radius, y + radius, radius, height - 2 * radius);
			GUI.canvas.DrawFilledCircle(col, x + radius, y + radius, radius);
			GUI.canvas.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
			GUI.canvas.DrawFilledCircle(col, x + radius, y + height - radius - 1, radius);
			GUI.canvas.DrawFilledCircle(col, x + width - radius - 1, y + height - radius - 1, radius);
		}
		public static void DrawTopRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
		{
			GUI.canvas.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
			GUI.canvas.DrawFilledRectangle(col, x, y + radius, width, height - radius);
			GUI.canvas.DrawFilledCircle(col, x + radius, y + radius, radius);
			GUI.canvas.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
		}
	}
}
