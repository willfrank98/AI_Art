using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Art
{
	class ImageShape : Shape
	{
		public string _imagePath;
		public PointF[] _points;
		private PointF _point4;
		private double _rotation;
		private double _ratio;

		public ImageShape(int height, int width, Random rand, List<string> parameters)
		{
			_imagePath = parameters[0];
			int minLength = int.Parse(parameters[1]);
			int maxLength = int.Parse(parameters[2]);
			int newImgHeight = int.Parse(parameters[3]);
			int newImgWidth = int.Parse(parameters[4]);

			int tempX1 = rand.Next(minLength / 2, width - minLength / 2);
			int tempY1 = rand.Next(minLength / 2, height - minLength / 2);
			PointF p1 = new Point(tempX1, tempY1);

			double rotation = rand.NextDouble() * 2 * Math.PI;
			_rotation = rotation;

			double high = (double)maxLength / newImgHeight;
			double low = (double)minLength / newImgHeight;
			double ratio = rand.NextDouble() * (high - low) + low;
			_ratio = ratio;
			int newHeight = (int)(newImgHeight * ratio);
			int newWidth = (int)(newImgWidth * ratio);

			double shiftX = Math.Sin(rotation) * newHeight;
			double shiftY = Math.Cos(rotation) * newWidth;
			int tempX2 = tempX1 + (int)shiftX;
			int tempY2 = tempY1 + (int)shiftY;
			PointF p2 = new Point(tempX2, tempY2);

			rotation += (Math.PI / 2) % Math.PI;

			shiftX = Math.Sin(rotation) * newWidth;
			shiftY = Math.Cos(rotation) * newWidth;
			int tempX3 = tempX1 + (int)shiftX;
			int tempY3 = tempY1 + (int)shiftY;
			PointF p3 = new Point(tempX3, tempY3);

			_points = new PointF[] { p1, p2, p3 };

			_point4 = new PointF(tempX2 + (int)shiftX, tempY2 + (int)shiftY);
		}

		public void GenerateShape()
		{
			
		}

		public Color GetColorAtPoint(Point p)
		{
			int diffX = p.X - (int)_points[0].X;
			int diffY = p.Y - (int)_points[0].Y;

			int adjustedX = (int)(diffX / Math.Sin(_rotation) / _ratio);
			int adjustedY = (int)(diffY / Math.Cos(_rotation) / _ratio);

			Bitmap bp = new Bitmap(_imagePath);
			return bp.GetPixel(adjustedX, adjustedY);
		}

		public IEnumerable<Point> InteratePoints(int granularity)
		{
			foreach (Point point in InterateTriangle(_points, granularity))
			{
				yield return point;
			}

			PointF[] newPoints = new PointF[] { _points[2], _points[3], _point4 };
			foreach (Point point in InterateTriangle(newPoints, granularity))
			{
				yield return point;
			}
		}

		private IEnumerable<Point> InterateTriangle(PointF[] points, int granularity)
		{
			Point pt1 = new Point((int)points[0].X, (int)points[0].Y);
			Point pt2 = new Point((int)points[1].X, (int)points[1].Y);
			Point pt3 = new Point((int)points[2].X, (int)points[2].Y);

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

		public Image GetImage()
		{
			return new Bitmap(_imagePath);
		}

	}
}
