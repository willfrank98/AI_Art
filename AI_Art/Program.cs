using System;
using System.Diagnostics;

namespace AI_Art
{
	class Program
	{
		static int Main(string[] args)
		{
			string fileIn = "PearlEarring.jpg";
			TriangleImage image = new TriangleImage(0, fileIn);

			for (int i = 0; i < 1; i++)
			{
				string fileOut = "pearl-out-" + i + ".bmp";
				int num = 200000;
				int minLength = 5;
				int maxLength = 50;
				int granularity = 3;
				int drawNum = 50000;

				image.NewBatch(num, minLength, maxLength);
				image.EvaluateFitness(granularity);
				image.Draw(drawNum, fileOut);
			}

			return 0;
		}
	}
}
