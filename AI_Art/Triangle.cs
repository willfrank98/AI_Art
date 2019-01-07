using System;
using System.Drawing;

namespace AI_Art
{
	class Triangle
	{
		public Point[] _points;
		public Brush _brush;
		public double _fitness;

		public Triangle(Point[] points, Brush brush)
		{
			_points = points;
			_brush = brush;
		}

		public Triangle(Random rand, int height, int width)
		{
			var newPoints = MakeThreePoints(rand, height, width);
			_points = newPoints;

			var brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
			_brush = brush;
		}

		public Triangle(Random rand, int height, int width, int minLength, int maxLength)
		{
			var newPoints = MakeThreePoints(rand, height, width, minLength, maxLength);
			_points = newPoints;

			var brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
			_brush = brush;
		}

		public Triangle()
		{
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
	}
}
