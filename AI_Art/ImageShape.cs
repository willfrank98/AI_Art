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

		public void GenerateShape(int height, int width, Random rand, List<string> parameters)
		{
			Image image = new Bitmap(parameters[0]);
			int minLength = int.Parse(parameters[1]);
			int maxLength = int.Parse(parameters[2]);


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
