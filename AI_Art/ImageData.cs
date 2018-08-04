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
