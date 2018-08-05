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
	}
}
