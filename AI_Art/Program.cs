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
			args = new string[] { "LastSupper.jpg", "LastSupper-Out.bmp", "1", "10", "10", "1", "Will", "MonaLisa.jpg", "20", "50" };

			string fileIn = args[0];
			string fileOut = args[1];
			int type = int.Parse(args[2]);
			int num = int.Parse(args[3]);
			int drawNum = int.Parse(args[4]);
			int granularity = int.Parse(args[5]);
			int seed = args[6].GetHashCode();
			List<string> parameters = args.Skip(7).ToList();

			if (type == 1)
			{
				Bitmap bp = new Bitmap(parameters[0]);
				parameters.Add(bp.Height.ToString());
				parameters.Add(bp.Width.ToString());
				bp.Dispose();
			}

			// loads the image to be recreated
			ImageRepresentation image = new ImageRepresentation(fileIn);

			// starts a timer, just for testing
			// TODO: add option to not time
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
