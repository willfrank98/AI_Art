using System;
using System.Diagnostics;

namespace AI_Art
{
	class Program
	{
		static int Main(string[] args)
		{
			string fileIn = "";
			string fileOut = "";
			int num = 0;
			int minLength = 0;
			int maxLength = 0;
			int granularity = 0;
			int drawNum = 0;
			int seed = 0;

			// run with no command line args to be prompted for input
			if (args.Length == 0)
			{
				Console.WriteLine("Enter input file: ");
				fileIn = Console.ReadLine();
				Console.WriteLine("Enter output file: ");
				fileOut = Console.ReadLine();
				Console.WriteLine("Enter number of triangles to generate: ");
				if (!int.TryParse(Console.ReadLine(), out num))
				{
					Console.WriteLine("Invalid input!");
					return 1;
				}
				Console.WriteLine("Enter minimum side length: ");
				if (!int.TryParse(args[3], out minLength))
				{
					Console.WriteLine("Invalid input!");
					return 1;
				}
				Console.WriteLine("Enter maximum side length: ");
				if (!int.TryParse(args[4], out maxLength))
				{
					Console.WriteLine("Invalid input!");
					return 1;
				}
				Console.WriteLine("Enter granularity level: ");
				if (!int.TryParse(args[5], out granularity))
				{
					Console.WriteLine("Invalid input!");
					return 1;
				}
				Console.WriteLine("Enter number of triangles to generate: ");
				if (!int.TryParse(args[6], out drawNum))
				{
					Console.WriteLine("Invalid input!");
					return 1;
				}
			}
			else if (args.Length == 1)
			{
				if (string.Equals(args[0], "-hc"))	// -hc to use hardcoded values
				{
					args = new string[8];
					fileIn = "PearlEarring.jpg";		//source file
					fileOut = "PearlEarring-Out.bmp";	//destination file
					num = 2000000;						//number to generate
					minLength = 5;						//min side length
					maxLength = 30;						//max side length
					granularity = 3;					//granularity to evaluate at
					drawNum = 50000;					//number to draw
					seed = DateTime.Now.Millisecond;	//random seed
				}
				else
				{
					Console.WriteLine("Invalid Args!");
				}
			}
			else // see README.md for more info about command line args
			{ 
				fileIn = args[0];
				fileOut = args[1];

				if (!int.TryParse(args[2], out num))
				{
					Console.WriteLine("Invalid Args!");
					return 1;
				}

				if (!int.TryParse(args[3], out minLength))
				{
					Console.WriteLine("Invalid Args!");
					return 1;
				}

				if (!int.TryParse(args[4], out maxLength))
				{
					Console.WriteLine("Invalid Args!");
					return 1;
				}

				if (!int.TryParse(args[5], out granularity))
				{
					Console.WriteLine("Invalid Args!");
					return 1;
				}

				if (!int.TryParse(args[6], out drawNum))
				{
					Console.WriteLine("Invalid Args!");
					return 1;
				}

				seed = args[7].GetHashCode();
			}

			// loads the image to be recreated
			TriangleImage image = new TriangleImage(seed, fileIn);

			// starts a timer, just for testing
			// TODO: add option to not time
			//Stopwatch timer = new Stopwatch();
			//timer.Start();

			image.NewBatch(num, minLength, maxLength);
			image.EvaluateFitness(granularity);
			image.Draw(drawNum, fileOut);



			//timer.Stop();

			//TimeSpan ts = timer.Elapsed;
			//string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,	ts.Milliseconds / 10);
			//Console.WriteLine($"Finished in {elapsedTime}.");
			return 0;
		}
	}
}
