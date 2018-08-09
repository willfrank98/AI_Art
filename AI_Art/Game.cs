using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace AI_Art
{
	internal class Game
	{
		private ImageData[] _gameImages;
		private Image _targetImage;
		private Random _masterRandom;

		public Game(string seed, int numberOfImages, string targetImageFilePath)
		{
			_masterRandom = new Random(seed.GetHashCode());

			_targetImage = new Bitmap(targetImageFilePath);

			_gameImages = new ImageData[numberOfImages];
			for (int i = 0; i < _gameImages.Length; i++)
			{
				_gameImages[i] = new ImageData(_masterRandom, i + 1, _targetImage.Height, _targetImage.Width);
			}
		}

		/// <summary>
		/// Draws _gameImages to image files
		/// </summary>
		public void Draw()
		{
			foreach (var image in _gameImages)
			{
				image.Draw();
			}
		}

		public void CombineAndDraw(double[] combinePercents)
		{
			for (int i = 0; i < _gameImages.Length; i++)
			{
				var tempTris = _gameImages[i].GetTriangles();

				var newTris = new Triangle[tempTris.Length + 1];
				for (int j = 0; j < tempTris.Length; j++)
				{

				}
			}
		}

		public double[] EvaluateFitness()
		{
			double[] fitnessSums = new double[_gameImages.Length];

			for (int i = 0; i < fitnessSums.Length; i++)
			{
				fitnessSums[i] = SingleImageFitness(_gameImages[i].GetImage());
			}

			var total = fitnessSums.Sum();

			double[] fitness = new double[fitnessSums.Length];

			fitness[0] = fitnessSums[0] / total;
			for (int i = 1; i < fitness.Length; i++)
			{
				fitness[i] = (fitnessSums[i] / total) + fitness[i - 1];
			}

			return fitness;
		}

		private unsafe double SingleImageFitness(Image image)
		{
			Bitmap b1 = new Bitmap(image);
			BitmapData b1Data = b1.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, b1.PixelFormat);

			Bitmap b2 = new Bitmap(_targetImage);
			BitmapData b2Data = b2.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, b2.PixelFormat);

			byte bitsPerPixel = GetBitsPerPixel(b1Data.PixelFormat);

			byte* scan0 = (byte*)b1Data.Scan0.ToPointer();
			byte* scan1 = (byte*)b2Data.Scan0.ToPointer();

			double totalFitness = 0;

			for (int i = 0; i < b1Data.Height; ++i)
			{
				for (int j = 0; j < b1Data.Width; ++j)
				{
					byte* data1 = scan0 + i * b1Data.Stride + j * bitsPerPixel / 8;
					byte* data2 = scan1 + i * b2Data.Stride + j * bitsPerPixel / 8;

					totalFitness += Math.Abs(data2[0] - data1[0]);
					totalFitness += Math.Abs(data2[1] - data1[1]);
					totalFitness += Math.Abs(data2[2] - data1[2]);
				}
			}

			b1.UnlockBits(b1Data);
			b2.UnlockBits(b2Data);

			return totalFitness;
		}

		private byte GetBitsPerPixel(PixelFormat pixelFormat)
		{
			switch (pixelFormat)
			{
				case PixelFormat.Format24bppRgb:
					return 24;
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format32bppPArgb:
				case PixelFormat.Format32bppRgb:
					return 32;
				default:
					throw new ArgumentException("Only 24 and 32 bit images are supported");

			}
		}
	}
}