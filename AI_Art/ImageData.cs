using System;
using System.Drawing;
using System.Linq;

namespace AI_Art
{
	internal class ImageData
	{
		private readonly int MAX_LENGTH = 500;
		private Triangle[] _triangles;
		private int[] _displayOrder;
		private int _imageNumber;

		private Image _image;

		public ImageData(Random rand, int imageNumber)
		{
			_imageNumber = imageNumber;

			//int num = rand.Next(30, 50);
			int num = 50;

			_triangles = new Triangle[num];
			for (int i = 0; i < _triangles.Length; i++)
			{
				var tempX1 = rand.Next(0, 1920);
				var tempY1 = rand.Next(0, 1920);
				var point1 = new Point(tempX1, tempY1);

				var tempX2 = rand.Next(tempX1 - MAX_LENGTH, tempX1 + MAX_LENGTH);
				var tempY2 = rand.Next(tempY1 - MAX_LENGTH, tempY1 + MAX_LENGTH);
				var point2 = new Point(tempX2, tempY2);

				var tempX3 = rand.Next(tempX1 - MAX_LENGTH, tempX1 + MAX_LENGTH);
				var tempY3 = rand.Next(tempY1 - MAX_LENGTH, tempY1 + MAX_LENGTH);
				var point3 = new Point(tempX3, tempY3);

				var brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));

				_triangles[i] = new Triangle(new Point[] { point1, point2, point3}, brush);
			}

			_displayOrder = Enumerable.Range(0, num).ToArray();
			Shuffle(rand, _displayOrder);

			_image = new Bitmap(1920, 1080);
		}

		public ImageData(Random rand, params Triangle[] triangles)
		{
			_triangles = triangles;

			_displayOrder = Enumerable.Range(0, triangles.Length).ToArray();
			Shuffle(rand, _displayOrder);

			_image = new Bitmap(1920, 1080);
		}

		/// <summary>
		/// Draws each image to a file based on its data
		/// </summary>
		public void Draw()
		{
			using (var drawing = Graphics.FromImage(_image))
			{
				drawing.Clear(Color.White);

				for (int i = 0; i < _displayOrder.Length; i++)
				{
					var triangle = _triangles[_displayOrder[i]];

					drawing.FillPolygon(triangle._brush, triangle._points);
				}
				drawing.Save();
			}

			_image.Save($"image{_imageNumber}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
		}

		public Triangle GetTriangle(int position)
		{
			return _triangles[position];
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
