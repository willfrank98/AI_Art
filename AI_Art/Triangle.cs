using System;
using System.Collections.Generic;
using System.Drawing;

namespace AI_Art
{
	class Triangle : Shape
	{
		private Point[] _points;
		private Brush _brush;

		public Triangle(int height, int width, Random rand, List<string> parameters)
		{
			int minLength = int.Parse(parameters[0]);
			int maxLength = int.Parse(parameters[1]);

			var newPoints = MakeThreePoints(rand, height, width, minLength, maxLength);
			_points = newPoints;

			var brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
			_brush = brush;
		}

		public static Point[] MakeThreePoints(Random rand, int height, int width, int minLength = 50, int maxLength = 100)
		{
			if (minLength > maxLength)
			{
				throw new ArgumentOutOfRangeException("minLength", "minLength cannpt be greater than maxLength");
			}

			var tempX1 = rand.Next(minLength / 2, width - minLength / 2);
			var tempY1 = rand.Next(minLength / 2, height - minLength / 2);
			var point1 = new Point(tempX1, tempY1);

			var tempX2 = rand.Next((maxLength - minLength) * 2);
			if (tempX2 < maxLength - minLength)
			{
				tempX2 = tempX1 - minLength - tempX2;
			}
			else
			{
				tempX2 = tempX1 + minLength + tempX2;
			}
			var tempY2 = rand.Next((maxLength - minLength) * 2);
			if (tempY2 < maxLength - minLength)
			{
				tempY2 = tempY1 - minLength - tempY2;
			}
			else
			{
				tempY2 = tempY1 + minLength + tempY2;
			}
			var point2 = new Point(tempX2, tempY2);

			var tempX3 = rand.Next((maxLength - minLength) * 3);
			if (tempX3 < maxLength - minLength)
			{
				tempX3 = tempX1 - minLength - tempX3;
			}
			else
			{
				tempX3 = tempX1 + minLength + tempX3;
			}
			var tempY3 = rand.Next((maxLength - minLength) * 3);
			if (tempY3 < maxLength - minLength)
			{
				tempY3 = tempY1 - minLength - tempY3;
			}
			else
			{
				tempY3 = tempY1 + minLength + tempY3;
			}
			var point3 = new Point(tempX3, tempY3);

			var points = new Point[] { point1, point2, point3 };
			return points;
		}


		/* code I found at https://stackoverflow.com/questions/11075505/get-all-points-within-a-triangle, slightly modified */

		// Enumerates all points in triangle described by the given three points at the given level of granularity
		public IEnumerable<Point> IteratePoints(int granularity)
		{
			Point pt1 = _points[0];
			Point pt2 = _points[1];
			Point pt3 = _points[2];

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

		public Color GetColorAtPoint(Point p)
		{
			return ((SolidBrush)_brush).Color;
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
