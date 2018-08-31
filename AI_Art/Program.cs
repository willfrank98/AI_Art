using System;
using System.Drawing;

namespace AI_Art
{
	class Program
	{
		static void Main(string[] args)
		{
			//Console.Write("Enter a random seed: ");
			var seed = /*Console.ReadLine();*/DateTime.Now.Millisecond.ToString()/*"Will"*/;
			//Console.Write("How many images would you like to generate at once?: ");
			var num = /*int.Parse(Console.ReadLine())*/20;
			var filepath = "C:\\Users\\Will\\Desktop\\MonaLisa.jpg";
			var game = new Game(seed, num, filepath);

			game.Draw();

			for (int i = 0; i <= 768; i++)
			{
				int threshold = 500;
				double[] matchPercents = game.EvaluateFitness(threshold);

				//TODO: take the totalDeviance into account to amplify small differences when deviance is low
				var totalDeviance = 0.0;
				for (int j = 0; j < matchPercents.Length; j++)
				{
					totalDeviance += Math.Abs(matchPercents[j] - (j + 1) * (100 / matchPercents.Length));
				}
				game.CombineAndDraw(matchPercents);

				Console.WriteLine("Threshold: " + i + "\t\tTotal Deviance: " + totalDeviance);
			}
		}
	}
}
