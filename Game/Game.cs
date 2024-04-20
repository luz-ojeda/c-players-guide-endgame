using Endgame.Game.Characters;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Endgame.Game;

public class Game
{
    public readonly BlazorConsole Console = new();
    public TrueProgrammer Player { get; set; } = new();
    private ConsoleHelper ConsoleHelper { get; set; }
    private Party HeroesParty { get; set; } = Party.HeroesParty;
    private Party MonstersParty { get; set; } = Party.MonstersParty;
	private List<Battle> Battles { get; set; } = [];

    public Game()
    {
        ConsoleHelper = new(Console);
        Statics.Console = Console;
    }

    public async Task Run()
    {
        while (true)
        {
            await AskForPlayerName();
            HeroesParty.Characters.Add(Player);

            Battles.Add(new Battle(HeroesParty, MonstersParty));
            foreach(Battle b in Battles)
            {
                await b.Run();
            }
        }
    }

    private async Task AskForPlayerName()
    {
        await Console.Write("Enter your character name: ");
		string name = await Console.ReadLine();
        if (name != null)
        {
            Player.Name = name;
        }
	}
}
