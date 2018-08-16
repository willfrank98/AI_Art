using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AI_Art
{
	class Triangle
	{
		private static int MAX_LENGTH = 200;
		private static int MIN_LENGTH = 100;
		public Point[] _points;
		public Brush _brush;

		public Triangle(Point[] points, Brush brush)
		{
			_points = points;
			_brush = brush;
		}

		public Triangle(Random rand)
		{
			var newPoints = MakeThreePoints(rand);
			_points = newPoints;

			var brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
			_brush = brush;
		}

		public Triangle()
		{
		}

		public static Point[] MakeThreePoints(Random rand)
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

			var points = new Point[] { point1, point2, point3 };
			return points;
		}
	}
}
