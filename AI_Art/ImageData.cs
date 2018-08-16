using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace AI_Art
{
	internal class ImageData
	{
		private readonly int MAX_LENGTH = 200;
		private readonly int MIN_LENGTH = 100;
		private Triangle[] _triangles;
		private int _imageNumber;

		private Image _image;

		public ImageData(Random rand, int imageNumber, int height, int width)
		{
			_imageNumber = imageNumber;

			//TODO: investigate possible conflicting code?
			int numberOfTriangles = rand.Next(300, 350);

			_triangles = new Triangle[numberOfTriangles];
			for (int i = 0; i < _triangles.Length; i++)
			{
				var tempX1 = rand.Next(0, 1920);
				var tempY1 = rand.Next(0, 1920);
				var point1 = new Point(tempX1, tempY1);

				var tempX2 = rand.Next((MAX_LENGTH - MIN_LENGTH) * 2);
				if (tempX2 < MAX_LENGTH - MIN_LENGTH)
				{
					tempX2 = tempX1 - MIN_LENGTH - tempX2;
				}
				else
				{
					tempX2 = tempX1 + MIN_LENGTH + tempX2;
				}
				var tempY2 = rand.Next((MAX_LENGTH - MIN_LENGTH) * 2);
				if (tempY2 < MAX_LENGTH - MIN_LENGTH)
				{
					tempY2 = tempY1 - MIN_LENGTH - tempY2;
				}
				else
				{
					tempY2 = tempY1 + MIN_LENGTH + tempY2;
				}
				var point2 = new Point(tempX2, tempY2);

				var tempX3 = rand.Next((MAX_LENGTH - MIN_LENGTH) * 3);
				if (tempX3 < MAX_LENGTH - MIN_LENGTH)
				{
					tempX3 = tempX1 - MIN_LENGTH - tempX3;
				}
				else
				{
					tempX3 = tempX1 + MIN_LENGTH + tempX3;
				}
				var tempY3 = rand.Next((MAX_LENGTH - MIN_LENGTH) * 3);
				if (tempY3 < MAX_LENGTH - MIN_LENGTH)
				{
					tempY3 = tempY1 - MIN_LENGTH - tempY3;
				}
				else
				{
					tempY3 = tempY1 + MIN_LENGTH + tempY3;
				}
				var point3 = new Point(tempX3, tempY3);

				var brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));

				_triangles[i] = new Triangle(new Point[] { point1, point2, point3}, brush);
			}

			_image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
		}

		public ImageData(int imageNumber, int height, int width, Triangle[] triangles)
		{
			_imageNumber = imageNumber;
			_triangles = triangles;

			_image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
		}

		/// <summary>
		/// Draws each image to a file based on its data
		/// </summary>
		public void Draw()
		{
			using (var drawing = Graphics.FromImage(_image))
			{
				drawing.Clear(Color.White);

				foreach (var triangle in _triangles)
				{ 
					drawing.FillPolygon(triangle._brush, triangle._points);
				}
				drawing.Save();
			}

			_image.Save($"image{_imageNumber}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
			//_image.Dispose();
		}

		public Triangle GetTriangle(int position)
		{
			return _triangles[position];
		}

		public Triangle[] GetTriangles()
		{
			return _triangles;
		}

		public Image GetImage()
		{
			return _image;
		}

		private void Shuffle(Random rand, int[] arr)
		{
			int n = arr.Length;
			while (n > 1)
			{
				int k = rand.Next(n--);
				int temp = arr[n];
				arr[n] = arr[k];
				arr[k] = temp;
			}
		}
	}
}
