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
			var game = new Game(seed);


			//var image = gen.DrawTextImage("sample text", new Font(FontFamily.GenericMonospace, 12), Color.Black, Color.White);
			//image.Save("image.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
			//image.Dispose();
		}
	}
}

//private Image DrawText(String text, Font font, Color textColor, Color backColor)
//{
//	//first, create a dummy bitmap just to get a graphics object
//	Image img = new Bitmap(1, 1);
//	Graphics drawing = Graphics.FromImage(img);

//	//measure the string to see how big the image needs to be
//	SizeF textSize = drawing.MeasureString(text, font);

//	//free up the dummy image and old graphics object
//	img.Dispose();
//	drawing.Dispose();

//	//create a new image of the right size
//	img = new Bitmap((int)textSize.Width, (int)textSize.Height);

//	drawing = Graphics.FromImage(img);

//	//paint the background
//	drawing.Clear(backColor);

//	//create a brush for the text
//	Brush textBrush = new SolidBrush(textColor);

//	drawing.DrawString(text, font, textBrush, 0, 0);

//	drawing.Save();

//	textBrush.Dispose();
//	drawing.Dispose();

//	return img;

//}

//private Image DrawTextImage(String currencyCode, Font font, Color textColor, Color backColor)
//{
//	return DrawTextImage(currencyCode, font, textColor, backColor, Size.Empty);
//}
//private Image DrawTextImage(String currencyCode, Font font, Color textColor, Color backColor, Size minSize)
//{
//	//first, create a dummy bitmap just to get a graphics object
//	SizeF textSize;
//	using (Image img = new Bitmap(1, 1))
//	{
//		using (Graphics drawing = Graphics.FromImage(img))
//		{
//			//measure the string to see how big the image needs to be
//			textSize = drawing.MeasureString(currencyCode, font);
//			if (!minSize.IsEmpty)
//			{
//				textSize.Width = textSize.Width > minSize.Width ? textSize.Width : minSize.Width;
//				textSize.Height = textSize.Height > minSize.Height ? textSize.Height : minSize.Height;
//			}
//		}
//	}

//	//create a new image of the right size
//	Image retImg = new Bitmap((int)textSize.Width, (int)textSize.Height);
//	using (var drawing = Graphics.FromImage(retImg))
//	{
//		//paint the background
//		drawing.Clear(backColor);

//		//create a brush for the text
//		using (Brush textBrush = new SolidBrush(textColor))
//		{
//			drawing.DrawString(currencyCode, font, textBrush, 0, 0);
//			drawing.Save();
//		}
//	}
//	return retImg;
//}
