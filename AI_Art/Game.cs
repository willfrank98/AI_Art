using System;

namespace AI_Art
{
	internal class Game
	{
		private ImageData[] _gameImages;
		private Random _masterRandom;

		public Game(string seed)
		{
			_masterRandom = new Random(seed.GetHashCode());

			_gameImages = new ImageData[_masterRandom.Next(10, 20)];
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
	}
}