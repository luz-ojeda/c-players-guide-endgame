using System;
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