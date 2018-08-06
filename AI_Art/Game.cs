using System;
using System.Collections.Generic;

namespace AI_Art
{
	internal class Game
	{
		private ImageData[] _gameImages;
		private Random _masterRandom;

		public Game(string seed)
		{
			_masterRandom = new Random(seed.GetHashCode());

			_gameImages = new ImageData[10];
			for (int i = 0; i < _gameImages.Length; i++)
			{
				_gameImages[i] = new ImageData(_masterRandom, i);
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

		public void CombineAndDraw(int[] toCombine)
		{
			var trianglesOne = _gameImages[toCombine[0]].GetTriangles();
			var trianglesTwo = _gameImages[toCombine[1]].GetTriangles();
			var trianglesThree = _gameImages[toCombine[2]].GetTriangles();

			for (int i = 0; i < 9; i++)
			{
				var newLength = (trianglesOne.Length * 3 + trianglesTwo.Length * 2 + trianglesThree.Length) / 6;
				newLength += _masterRandom.Next(-newLength/10, newLength/10);

				var newTris = new Triangle[newLength];
				for (int j = 0; j < newLength; j++)
				{
					var next = _masterRandom.Next(100);
					if (next < 5)
					{
						newTris[j] = new Triangle();
					}
					else if (next < 30)
					{
						newTris[j] = trianglesOne[j] ?? new Triangle();
					}
					else if (next < 60)
					{
						newTris[j] = trianglesTwo[j] ?? new Triangle();
					}
					else
					{
						newTris[j] = trianglesThree[j] ?? new Triangle();
					}
				}

				_gameImages[i] = new ImageData(newTris);
			}

			Draw();
		}
	}
}