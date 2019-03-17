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
		public Image _image;
		public PointF[] _points;

		public ImageShape()
		{

		}

		public void GenerateShape(List<string> parameters)
		{
			throw new NotImplementedException();
		}

		public Color GetColorAtPoint(Point p)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Point> InteratePoints(int granularity)
		{
			throw new NotImplementedException();
		}

		
	}
}
