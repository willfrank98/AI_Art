using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AI_Art
{
	class Triangle
	{
		public Point[] _points;
		public Brush _brush;

		public Triangle(Point[] points, Brush brush)
		{
			_points = points;
			_brush = brush;
		}

		public Triangle()
		{
			var rand = new Random(DateTime.Now.Millisecond);
			var MAX_LENGTH = 500;

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

			_points = new Point[] { point1, point2, point3 };
			_brush = brush;
		}
	}
}
