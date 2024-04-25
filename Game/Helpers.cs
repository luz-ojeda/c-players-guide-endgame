using Endgame.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endgame.Game;

public static class ConsoleHelper
{
	// Changes to the specified color and then displays the text on its own line.
	public static async Task WriteLine(string text, ConsoleColor color)
	{
		Statics.Console.ForegroundColor = color;
		await Statics.Console.WriteLine(text);
		Statics.Console.ResetColor();
	}

	public static async Task WriteLine(object o, ConsoleColor color)
	{
		Statics.Console.ForegroundColor = color;
		await Statics.Console.WriteLine(o);
		Statics.Console.ResetColor();
	}

	// Changes to the specified color and then displays the text without moving to the next line.
	public static async Task Write(string text, ConsoleColor color)
	{
		Statics.Console.ForegroundColor = color;
		await Statics.Console.Write(text);
		Statics.Console.ResetColor();
	}
}

public static class ComputerPlayer
{
	public static int GetWeightedRandomIndex(List<double> weights)
	{
		double totalWeight = 0;
		Random random = new();

		foreach (double weight in weights)
		{
			totalWeight += weight;
		}

		double randomWeight = random.NextDouble() * totalWeight;
		double currentSum = 0;

		for (int i = 0; i < weights.Count; i++)
		{
			currentSum += weights[i];
			if (currentSum >= randomWeight)
				return i;
		}

		return 0;
	}

	public static List<double> CalculateDynamicWeights(IEnumerable<IWeightedOption> actions)
	{
		double specifiedTotalWeight = actions.Where(a => a.Weight.HasValue).Sum(a => a.Weight.Value);
		int numActionsWithoutWeight = actions.Count(a => !a.Weight.HasValue);
		double remainingWeight = 1.0 - specifiedTotalWeight;

		// Options without a weight, such as "Do nothing" by defualt should be picked randomly
		double weightForUnspecifiedActions = (numActionsWithoutWeight > 0) ? remainingWeight / numActionsWithoutWeight : 0;
		List<double> weights = actions.Select(a => a.Weight ?? weightForUnspecifiedActions).ToList();
		return weights;
	}
}