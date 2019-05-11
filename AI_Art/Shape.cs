using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Art
{
	interface IShape
	{
		IEnumerable<Point> IteratePoints(int granularity);
		Color GetColorAtPoint(Point p);
		Brush GetBrush();
		Point[] GetPoints();
	}
}
