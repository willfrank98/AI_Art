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

		/// <summary>
		/// Takes "percentage ranges" for each old image to generate new ones
		/// </summary>
		/// <param name="combinePercents">The "percentage ranges", an array of values where i's "range" is 0 to combinePercents[1] or combinePercents[i - 1] to combinePercents[i]</param>
		public void CombineAndDraw(double[] combinePercents)
		{
			//for each original image
			for (int i = 0; i < _gameImages.Length; i++)
			{
				//generate a list of new triangles
				var newTris = new Triangle[_gameImages[i].GetTriangles().Length];
				for (int j = 0; j < newTris.Length; j++)
				{
					//randomly choose the jth triangle from original image or generate new one
					var rand = _masterRandom.NextDouble() * 200;
					if (rand > 100)
					{
						//~1/(i + 1) chance for completely new triangle
						newTris[j] = new Triangle();
					}
					else
					{
						//if reusing triangle, use generated wights to determine which one
						for (int k = 0; k < combinePercents.Length; k++)
						{
							//iterate through each "percentage range"
							if (rand < combinePercents[k])
							{
								//if j is not out of bounds add jth triangle from GameImage k
								if (j < _gameImages[k].GetTriangles().Length)
								{
									newTris[j] = _gameImages[k].GetTriangle(j);
								}
								//if j is out of bounds, generate a new triangle
								else
								{
									newTris[j] = new Triangle();
								}
								
							}
						}
					}
				}
				//generate new ImageData from newTris
				_gameImages[i] = new ImageData(i + 1, _targetImage.Height, _targetImage.Width, newTris);
			}
			//draw new images
			Draw();
		}

		/// <summary>
		/// Determines the fitness of each image
		/// </summary>
		/// <param name="threshold">A color difference less than this amount is considered a match, greater than or equal is not a match. This is done to help amplify differences in fitness.</param>
		/// <returns>Array of doubles, input for CombineAndDraw</returns>
		public double[] EvaluateFitness(int threshold)
		{
			double[] fitnessSums = new double[_gameImages.Length];

			for (int i = 0; i < fitnessSums.Length; i++)
			{
				fitnessSums[i] = SingleImageFitness(_gameImages[i].GetImage(), threshold);
			}

			var total = fitnessSums.Sum();

			double[] fitness = new double[fitnessSums.Length];

			fitness[0] = (fitnessSums[0] / total) * 100;
			for (int i = 1; i < fitness.Length; i++)
			{
				fitness[i] = ((fitnessSums[i] / total) * 100) + fitness[i - 1];
			}

			return fitness;
		}

		private unsafe double SingleImageFitness(Image image, int threshold)
		{
			Bitmap bImage = new Bitmap(image);
			BitmapData bImageData = bImage.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, bImage.PixelFormat);

			Bitmap bTarget = new Bitmap(_targetImage);
			BitmapData bTargetData = bTarget.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, bTarget.PixelFormat);

			byte bitsPerPixel = GetBitsPerPixel(bImageData.PixelFormat);

			byte* scanImage = (byte*)bImageData.Scan0.ToPointer();
			byte* scanTarget = (byte*)bTargetData.Scan0.ToPointer();

			double imageFitness = 0;

			for (int i = 0; i < bImageData.Height; ++i)
			{
				for (int j = 0; j < bImageData.Width; ++j)
				{
					byte* data1 = scanImage + i * bImageData.Stride + j * bitsPerPixel / 8;
					byte* data2 = scanTarget + i * bTargetData.Stride + j * bitsPerPixel / 8;

					var colorDifference = Math.Abs(data2[0] - data1[0]) + Math.Abs(data2[1] - data1[1]) + Math.Abs(data2[2] - data1[2]);

					//TODO: investigate image drawing here
					if (colorDifference > threshold)
					{
						imageFitness++;
						//set match pixels to black, for fun testing
						//data1[0] = 255;
						//data1[1] = 255;
						//data1[2] = 255;
					}
					else
					{
						//set non match pixels to white, for fun testing
						//data1[0] = 0;
						//data1[1] = 0;
						//data1[2] = 0;
					}
				}
			}

			bImage.UnlockBits(bImageData);
			bTarget.UnlockBits(bTargetData);

			return imageFitness;
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