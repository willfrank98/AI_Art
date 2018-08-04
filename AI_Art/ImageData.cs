using System;

namespace AI_Art
{
	internal class ImageData
	{
		private int[] sideLength;
		private int[] xCenter;
		private int[] yCenter;
		private int[] rotation;
		private int[] displayOrder;

		public ImageData(Random rand)
		{
			int num = rand.Next(10, 30);

			sideLength = new int[num];
			for (int i = 0; i < sideLength.Length; i++)
			{
				sideLength[i] = rand.Next(20, 50);
			}

			xCenter = new int[num];
			for (int i = 0; i < xCenter.Length; i++)
			{
				xCenter[i] = rand.Next(0, 1920);
			}

			yCenter = new int[num];
			for (int i = 0; i < yCenter.Length; i++)
			{
				yCenter[i] = rand.Next(0, 1080);
			}

			rotation = new int[num];
			for (int i = 0; i < rotation.Length; i++)
			{
				rotation[i] = rand.Next(20, 50);
			}

			displayOrder = new int[num];
			for (int i = 0; i < displayOrder.Length; i++)
			{
				displayOrder[i] = rand.Next(20, 50);
			}
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
