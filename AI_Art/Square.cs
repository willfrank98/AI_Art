using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Art
{
	class Square : Shape
	{
		private Point[] _points;
		private Brush _brush;

		public Square(int height, int width, Random rand, List<string> parameters)
		{
			int minLength = int.Parse(parameters[0]);
			int maxLength = int.Parse(parameters[1]);

			int tempX1 = rand.Next(minLength / 2, width - minLength / 2);
			int tempY1 = rand.Next(minLength / 2, height - minLength / 2);
			Point p1 = new Point(tempX1, tempY1);

			int sideLength = rand.Next(minLength, maxLength);
			double rotation = rand.NextDouble() * 2 * Math.PI;

			double shiftX = Math.Sin(rotation) * sideLength;
			double shiftY = Math.Cos(rotation) * sideLength;
			int tempX2 = tempX1 + (int)shiftX;
			int tempY2 = tempY1 + (int)shiftY;
			Point p2 = new Point(tempX2, tempY2);

			rotation -= (Math.PI / 2) % Math.PI;

			shiftX = Math.Sin(rotation) * sideLength;
			shiftY = Math.Cos(rotation) * sideLength;
			int tempX3 = tempX1 + (int)shiftX;
			int tempY3 = tempY1 + (int)shiftY;
			Point p3 = new Point(tempX3, tempY3);

			Point p4 = new Point(tempX2 + (int)shiftX, tempY2 + (int)shiftY);

			_points = new Point[] { p1, p2, p4, p3 }; //this order is needed for drawing purposes

			_brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
		}

		public Color GetColorAtPoint(Point p)
		{
			return ((SolidBrush)_brush).Color;
		}

		public IEnumerable<Point> IteratePoints(int granularity)
		{
			Point[] newPoints = new Point[] { _points[0], _points[1], _points[3] };
			foreach (Point point in IterateTriangle(newPoints, granularity))
			{
				yield return point;
			}

			newPoints = new Point[] { _points[0], _points[2], _points[3] };
			foreach (Point point in IterateTriangle(newPoints, granularity))
			{
				yield return point;
			}
		}

		private IEnumerable<Point> IterateTriangle(Point[] points, int granularity)
		{
			Point pt1 = points[0];
			Point pt2 = points[1];
			Point pt3 = points[2];

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

		public Brush GetBrush()
		{
			return _brush;
		}

		public Point[] GetPoints()
		{
			return _points;
		}
	}
}
