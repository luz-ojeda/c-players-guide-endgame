using System;
using System.Threading.Tasks;
using Endgame;

namespace Endgame.Game;

public class ConsoleHelper
{
	public BlazorConsole Console { get; set; }

	public ConsoleHelper(BlazorConsole console)
	{
		Console = console;
	}

	// Changes to the specified color and then displays the text on its own line.
	public async Task WriteLine(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		await Console.WriteLine(text);
		Console.ResetColor();
	}

	// Changes to the specified color and then displays the text without moving to the next line.
	public async Task Write(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		await Console.Write(text);
	}
}