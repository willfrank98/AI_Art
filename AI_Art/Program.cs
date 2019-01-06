using System;
using System.Drawing;

namespace AI_Art
{
	class Program
	{
		static void Main(string[] args)
		{
			//Console.Write("Enter a random seed: ");
			var seed = /*Console.ReadLine();*/DateTime.Now.Millisecond.GetHashCode();/*"Will"*/;
			//Console.Write("How many images would you like to generate at once?: ");
			var num = 1000000;
			var filepath = "MonaLisa.jpg";

			var t = new TriangleImage(seed, filepath);

			t.NewBatch(num, 10, 30);
			t.EvaluateFitness();
			t.Draw(100000);
		}
	}
}
