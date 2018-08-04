using System;
using System.Drawing;
using System.Linq;

namespace AI_Art
{
	internal class ImageData
	{
		private readonly int MAX_LENGTH = 100;
		private int[] _x1;
		private int[] _x2;
		private int[] _x3;
		private int[] _y1;
		private int[] _y2;
		private int[] _y3;
		private int[] _displayOrder;
		private int _imageNumber;

		private Image _image;

		public ImageData(Random rand, int imageNumber)
		{
			_imageNumber = imageNumber;

			int num = rand.Next(10, 30);

			_x1 = new int[num];
			for (int i = 0; i < _x1.Length; i++)
			{
				_x1[i] = rand.Next(0, 1920);
			}

			_x2 = new int[num];
			for (int i = 0; i < _x2.Length; i++)
			{
				_x2[i] = rand.Next(0, 1920);
			}

			_x3 = new int[num];
			for (int i = 0; i < _x3.Length; i++)
			{
				_x3[i] = rand.Next(0, 1920);
			}

			_y1 = new int[num];
			for (int i = 0; i < _y1.Length; i++)
			{
				_y1[i] = rand.Next(0, 1080);
			}

			_y2 = new int[num];
			for (int i = 0; i < _y2.Length; i++)
			{
				_y1[i] = rand.Next(0, 1080);
			}

			_y3 = new int[num];
			for (int i = 0; i < _y3.Length; i++)
			{
				_y1[i] = rand.Next(0, 1080);
			}

			_displayOrder = Enumerable.Range(0, num).ToArray();
			Shuffle(rand, _displayOrder);

			Image temp = new Bitmap(1920, 1080);
		}

		/// <summary>
		/// Draws each image to a file based on its data
		/// </summary>
		public void Draw()
		{
			ClearBackground();

			for (int i = 0; i < _displayOrder.Length; i++)
			{
				int position = _displayOrder[i];

				var points = new Point[3];
				points[0] = new Point(_x1[position], _y1[position]);
				points[1] = new Point(_x2[position], _y2[position]);
				points[2] = new Point(_x3[position], _y3[position]);

				var pen = new Pen(Color.Black, 3);

				DrawToFile(points, pen);
			}

			_image.Save($"image{_imageNumber}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
		}

		private void ClearBackground()
		{
			using (var drawing = Graphics.FromImage(_image))
			{
				drawing.Clear(Color.White);
				drawing.Save();
			}
		}

		private void DrawToFile(Point[] points, Pen pen)
		{
			using (var drawing = Graphics.FromImage(_image))
			{
				drawing.DrawPolygon(pen, points);
			}
		}

		private void SaveImage()
		{

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
