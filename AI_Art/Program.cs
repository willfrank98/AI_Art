using System;
using System.Drawing;

namespace AI_Art
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Enter a random seed: ");
			var seed = Console.ReadLine();
			var gen = new ImageGenerator();
			var image = gen.DrawTextImage("sample text", new Font(FontFamily.GenericMonospace, 12), Color.Black, Color.White);
			image.Save("image.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
			image.Dispose();
		}
	}
}
