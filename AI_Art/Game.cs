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

			_gameImages = new ImageData[9];
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

		internal void CombineAndDraw(int[] toCombine)
		{
			//var T1_CHANCE = 40;
			//var T2_CHANCE = 30;
			//var T3_CHANCE = 20;
			//var NEW_CHANCE = 10;

			for (int i = 0; i < 9; i++)
			{
				var newTris = new List<Triangle>();
				for (int j = 0; j < 50; j++)
				{
					var next = _masterRandom.Next(10);
					if (next == 0)
					{
						newTris.Add(new Triangle());
					}
					else if (next < 3)
					{
						newTris.Add(_gameImages[toCombine[2]].GetTriangle(j));
					}
					else if (next < 6)
					{
						newTris.Add(_gameImages[toCombine[1]].GetTriangle(j));
					}
					else
					{
						newTris.Add(_gameImages[toCombine[0]].GetTriangle(j));
					}
				}

				_gameImages[i] = new ImageData(_masterRandom, newTris.ToArray());
			}

			Draw();
		}
	}
}