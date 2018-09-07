using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Art
{
	class TriangleImage
	{
		private Random rand;
		private int height;
		private int width;
		private Image image;
		private Triangle[] tris;

		public TriangleImage(int seed, string filepath)
		{
			rand = new Random(seed);

			Image image = new Bitmap(filepath);
			height = image.Height;
			width = image.Width;
		}

		public void NewBatch(int num, int minLength, int maxLength)
		{
			tris = new Triangle[num];
			for (int i = 0; i < num; i++)
			{
				tris[i] = new Triangle(rand, height, width, minLength, maxLength);
			}
		}

		private unsafe double EvaluateFitness()
		{
			//open image
			Bitmap b = new Bitmap(image);
			BitmapData bData = b.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, b.PixelFormat);

			byte bitsPerPixel = GetBitsPerPixel(bData.PixelFormat);

			byte* scanImage = (byte*)bData.Scan0.ToPointer();

			//evaluate triangles
			double[] fitness = new double[tris.Length];

			for (int i = 0; i < tris.Length; i++)
			{
				int x1 = tris[i]._points[0].X;
				int x2 = tris[i]._points[1].X;
				int x3 = tris[i]._points[2].X;

				int y1 = tris[i]._points[0].Y;
				int y2 = tris[i]._points[1].Y;
				int y3 = tris[i]._points[2].Y;

				//find smallest/largest x
				int topX = Math.Min(x1, Math.Max(x2, x3));
				int botX = Math.Max(x1, Math.Max(x2, x3));

				//find smallest/largest y
				int leftY = Math.Min(y1, Math.Max(y2, y3));
				int rghtY = Math.Max(y1, Math.Max(y2, y3));

				int m1 = (y2 - y1) / (x2 - x1);
				int m2 = (y3 - y2) / (x3 - x2);
				int m3 = (y1 - y3) / (x1 - x3);

				//TODO: optimize this
				for (int x = topX; x <= botX; x++)
				{
					for (int y = leftY; y <= rghtY; y++)
					{
						if ((y - y1) > m1 * (x - x1) && (y - y2) < m2 * (x - x2) && (y - y3) > m3 * (x - x3))
						{
							byte* data = scanImage + x * bData.Stride + y * bitsPerPixel / 8;

							Color color = ((SolidBrush) tris[i]._brush).Color;
							fitness[i] += Math.Abs(data[0] - color.B);
							fitness[i] += Math.Abs(data[1] - color.R);
							fitness[i] += Math.Abs(data[2] - color.G);
						}
					}
				}
			}

			

			return 0.0;
		}

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
