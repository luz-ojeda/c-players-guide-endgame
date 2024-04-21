using Endgame.Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endgame.Game;

public class Game
{
    public readonly BlazorConsole Console = new();
    public TrueProgrammer Player { get; set; }
    private Party Heroes { get; set; } = new Party(PartyType.Heroes);
    private List<Battle> Battles { get; set; } = [];
    private bool GameOver { get; set; }

    public Game()
    {
        Statics.Console = Console;
        Statics.ConsoleHelper = new(Console);
    }

    public async Task Run()
    {
        string playerName = await AskForPlayerName();
        Player = new(playerName);

        InitializeBattles();

        while (!GameOver)
        {
            foreach (var (index, b) in Battles.Select((b, index) => (index, b)))
            {
                await Console.WriteLine();
				await Statics.ConsoleHelper.WriteLine("Starting battle " + index, ConsoleColor.Magenta);
				Player.Battle = b;
                var battleWon = await b.Run();

                if (!battleWon)
                {
                    await Statics.ConsoleHelper.WriteLine($"The heroes have lost! The Uncoded One's forces have prevailed...", ConsoleColor.Red);
                    GameOver = true;
                    break;
                }

				if (battleWon && index == Battles.Count - 1)
                {
                    await Statics.ConsoleHelper.WriteLine($"The heroes have won all the battles!", ConsoleColor.Cyan);
					GameOver = true;
				}
			}
        }
    }

    private async Task<string> AskForPlayerName()
    {
        await Console.Write("Enter your character name: ");
        return await Console.ReadLine();
    }

    private void InitializeBattles()
    {
        Heroes.Characters.Add(Player);

        Battle battle1 = new(Heroes, new Party(PartyType.Monsters));
        battle1.Monsters.Characters.AddRange([new Skeleton(battle1)]);

        Battle battle2 = new(Heroes, new Party(PartyType.Monsters));
        battle2.Monsters.Characters.AddRange([new Skeleton(battle2), new Skeleton(battle2)]);

		Battle battle3 = new(Heroes, new Party(PartyType.Monsters));
		battle3.Monsters.Characters.AddRange([new TheUncodedOne(battle3)]);

		Battles.AddRange([battle1, battle2, battle3]);
    }
}
