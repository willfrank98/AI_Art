using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace AI_Art
{
	class TriangleImage
	{
		private Random rand;
		private int height;
		private int width;
		private Image image;
		private Triangle[] tris;
		private double[] fitness;

		/// <summary>
		/// Initializer
		/// </summary>
		/// <param name="seed">The random seed from which all triangles are generated</param>
		/// <param name="filepath">Path to the image to be recreated</param>
		public TriangleImage(int seed, string filepath)
		{
			rand = new Random(seed);

			Image temp = new Bitmap(filepath);
			height = temp.Height;
			width = temp.Width;
			image = temp;
		}

		/// <summary>
		/// Creates a new batch of random triangles
		/// </summary>
		/// <param name="num">Number of triangles to generate</param>
		/// <param name="minLength">Minimum side length of the triangles</param>
		/// <param name="maxLength">Maximum side length of the triangles</param>
		public void NewBatch(int num, int minLength, int maxLength)
		{
			tris = new Triangle[num];
			int onePercent = num / 100;
			for (int i = 0; i < num; i++)
			{
				tris[i] = new Triangle(rand, height, width, minLength, maxLength);

				if (i % onePercent == 0)
				{
					decimal percent = decimal.Divide(i, tris.Length) * 100;
					Console.SetCursorPosition(0, 0);
					Console.WriteLine("Generating: {0:0}%", percent);
				}
			}
		}

		/// <summary>
		/// Evaluates all generated triangles for fitness
		/// </summary>
		/// <param name="granularity">The level of granularity to evaulate the triangles (how many pixels per triangle are looked at),
		///							  directly affects performance</param>
		public unsafe void EvaluateFitness(int granularity)
		{
			//open image
			Bitmap b = new Bitmap(image);
			BitmapData bData = b.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, b.PixelFormat);

			byte bitsPerPixel = GetBitsPerPixel(bData.PixelFormat);

			byte* scanImage = (byte*)bData.Scan0.ToPointer();

			//evaluate triangles
			double[] fitness = new double[tris.Length];

			int onePercent = tris.Length / 100;
			for (int i = 0; i < tris.Length; i++)
			{
				int pixels = 0;
				foreach (Point point in PointsInTriangle(tris[i]._points[0], tris[i]._points[1], tris[i]._points[2], granularity))
				{
					if (point.X >= 0 && point.X < image.Width && point.Y >= 0 && point.Y < image.Height)
					{
						byte* data = scanImage + point.Y * bData.Stride + point.X * bitsPerPixel / 8;

						//higher fitness = better
						Color color = ((SolidBrush)tris[i]._brush).Color;
						fitness[i] += Math.Pow(1.03, 255 - Math.Abs(data[0] - color.B));
						fitness[i] += Math.Pow(1.03, 255 - Math.Abs(data[1] - color.G));
						fitness[i] += Math.Pow(1.03, 255 - Math.Abs(data[2] - color.R));
						pixels++;
					}
				}

				fitness[i] = fitness[i] / Math.Pow(pixels, .5);

				if (i % onePercent == 0)
				{
					decimal percent = decimal.Divide(i, tris.Length) * 100;
					Console.SetCursorPosition(0, 1);
					Console.WriteLine("Evaluating: {0:0}%", percent);
				}
			}

			this.fitness = fitness;
			Array.Sort(fitness, tris);
		}


		/* code I found at https://stackoverflow.com/questions/11075505/get-all-points-within-a-triangle, slightly modified */

		// Enumerates all points in triangle described by the given three points at the given level of granularity
		private IEnumerable<Point> PointsInTriangle(Point pt1, Point pt2, Point pt3, int granularity)
		{
			if (pt1.Y == pt2.Y && pt1.Y == pt3.Y)
			{
				throw new ArgumentException("The given points must form a triangle.");
			}

			Point tmp;

			if (pt2.X < pt1.X)
			{
				tmp = pt1;
				pt1 = pt2;
				pt2 = tmp;
			}

			if (pt3.X < pt2.X)
			{
				tmp = pt2;
				pt2 = pt3;
				pt3 = tmp;

				if (pt2.X < pt1.X)
				{
					tmp = pt1;
					pt1 = pt2;
					pt2 = tmp;
				}
			}

			var baseFunc = CreateFunc(pt1, pt3);
			var line1Func = pt1.X == pt2.X ? (x => pt2.Y) : CreateFunc(pt1, pt2);

			for (var x = pt1.X; x < pt2.X; x += granularity)
			{
				int maxY;
				int minY = GetRange(line1Func(x), baseFunc(x), out maxY);

				for (var y = minY; y <= maxY; y += granularity)
				{
					yield return new Point(x, y);
				}
			}

			var line2Func = pt2.X == pt3.X ? (x => pt2.Y) : CreateFunc(pt2, pt3);

			for (var x = pt2.X; x <= pt3.X; x += granularity)
			{
				int maxY;
				int minY = GetRange(line2Func(x), baseFunc(x), out maxY);

				for (var y = minY; y <= maxY; y += granularity)
				{
					yield return new Point(x, y);
				}
			}
		}

		private int GetRange(double y1, double y2, out int maxY)
		{
			if (y1 < y2)
			{
				maxY = (int)Math.Floor(y2);
				return (int)Math.Ceiling(y1);
			}

			maxY = (int)Math.Floor(y1);
			return (int)Math.Ceiling(y2);
		}

		private Func<int, double> CreateFunc(Point pt1, Point pt2)
		{
			var y0 = pt1.Y;

			if (y0 == pt2.Y)
			{
				return x => y0;
			}

			var m = (double)(pt2.Y - y0) / (pt2.X - pt1.X);

			return x => m * (x - pt1.X) + y0;
		}

		/* end of found code */


		/// <summary>
		/// Draws a certain number of the evaluated triangles.
		/// </summary>
		/// <param name="thisMany">How many of the top triangles to draw.</param>
		/// <param name="imageOut">Filepath for output image.</param>
		public void Draw(int thisMany, string imageOut)
		{
			using (var drawing = Graphics.FromImage(image))
			{
				drawing.Clear(Color.White);

				//draws the top ranking thisMany triangles, from worst to best
				int onePercent = thisMany / 100;
				for (int i = tris.Length - thisMany; i < tris.Length; i++)
				{
					Triangle triangle = tris[i];
					drawing.FillPolygon(triangle._brush, triangle._points);

					if (i % onePercent == 0)
					{
						decimal percent = decimal.Divide(i, tris.Length) * 100;
						Console.SetCursorPosition(0, 2);
						Console.WriteLine("Drawing: {0:0}%", percent);
					}
				}
				drawing.Save();
			}

			image.Save(imageOut, ImageFormat.Bmp);
			image.Dispose();
		}

		// gets the bits per pixel, used for pointer math in images
		private byte GetBitsPerPixel(PixelFormat pixelFormat)
		{
			switch (pixelFormat)
			{
				case PixelFormat.Format24bppRgb:
					return 24;
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format32bppPArgb:
				case PixelFormat.Format32bppRgb:
					return 32;
				default:
					throw new ArgumentException("Only 24 and 32 bit images are supported");

			}
		}
	}
}
