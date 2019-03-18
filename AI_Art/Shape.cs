using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Art
{
	interface Shape
	{
		IEnumerable<Point> InteratePoints(int granularity);
		void GenerateShape(int height, int width, Random rand, List<string> parameters);
		Color GetColorAtPoint(Point p);
	}
}
