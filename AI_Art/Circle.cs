using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Art
{
	class Circle : Shape
	{
		private Brush _brush;
		private Point _center;
		private int _r;

		public Circle(int height, int width, Random rand, List<string> parameters)
		{
			int minRadius = int.Parse(parameters[0]);
			int maxRadius = int.Parse(parameters[1]);

			int tempX1 = rand.Next(minRadius / 2, width - minRadius / 2);
			int tempY1 = rand.Next(minRadius / 2, height - minRadius / 2);
			_center = new Point(tempX1, tempY1);

			_r = rand.Next(minRadius, maxRadius);

			_brush = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
		}

		public Brush GetBrush()
		{
			return _brush;
		}

		public Color GetColorAtPoint(Point p)
		{
			return ((SolidBrush)_brush).Color;
		}

		public Point[] GetPoints()
		{
			throw new NotImplementedException();
		}

		public Rectangle GetRectangle()
		{
			return new Rectangle(_center.X - _r, _center.Y - _r, _r * 2, _r * 2);
		}

		public IEnumerable<Point> IteratePoints(int granularity)
		{
			int x = _center.X;
			int y = _center.Y;
			int r = _r;
			for (int i = y - r; i < y + r; i++)
			{
				for (int j = x; (j - x) * (j - x) + (i - y) * (i - y) <= r * r; j--)
				{
					yield return new Point(j, i);
				}
				for (int j = x + 1; (j - x) * (j - x) + (i - y) * (i - y) <= r * r; j++)
				{
					yield return new Point(j, i);
				}
			}
		}
	}
}
