using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace AI_Art
{
	internal class ImageData
	{

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
				var newPoints = Triangle.MakeThreePoints(rand);

				var brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));

				_triangles[i] = new Triangle(newPoints, brush);
			}
			//TODO: algorithmically determine pixelformat
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

			_image.Save($"image{_imageNumber}.jpeg", ImageFormat.Jpeg);
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
