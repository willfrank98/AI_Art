using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace AI_Art
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args[0].Equals("-hc"))
			{
				args = new string[] { "MonaLisa.jpg", "MonaLisa-Out.bmp", "0", "500000", "100000", "2", "Will", "5", "20" };
			}

			string fileIn = args[0];
			string fileOut = args[1];

			int type;
			if (!int.TryParse(args[2], out type))
			{
				Console.WriteLine("Invalid Args!");
				return 1;
			}

			int num;
			if (!int.TryParse(args[3], out num))
			{
				Console.WriteLine("Invalid Args!");
				return 1;
			}

			int drawNum;
			if (!int.TryParse(args[4], out drawNum))
			{
				Console.WriteLine("Invalid Args!");
				return 1;
			}

			int granularity;
			if (!int.TryParse(args[5], out granularity))
			{
				Console.WriteLine("Invalid Args!");
				return 1;
			}

			int seed = args[7].GetHashCode();
		
			List<string> parameters = args.Skip(7).ToList();

			// loads the image to be recreated
			ImageRepresentation image = new ImageRepresentation(fileIn);

			// starts a timer, just for testing
			// TODO: add option to not time?
			Stopwatch timer = new Stopwatch();
			timer.Start();

			image.NewBatch(num, type, seed, parameters);
			image.EvaluateFitness(granularity);
			image.Draw(drawNum, type, fileOut);

			timer.Stop();

			TimeSpan ts = timer.Elapsed;
			string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,	ts.Milliseconds / 10);
			Console.WriteLine($"Finished in {elapsedTime}.");
			return 0;
		}
	}
}
