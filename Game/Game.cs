using Endgame.Game.Characters;
using Endgame.Game.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Endgame.Game;

public class Game
{
	public readonly BlazorConsole Console = new();
	public TrueProgrammer Player { get; set; } = new();
	public GameplayMode Mode { get; set; }
	private Party Heroes { get; set; }
	private List<Battle> Battles { get; set; } = [];
	private bool GameOver { get; set; }

	public Game()
	{
		Statics.Console = Console;
	}

	public async Task Run()
	{
		await PromptForGameplayMode();
		if (Mode != GameplayMode.ComputerVsComputer) await Player.SetupName();

		InitializeBattles();

		while (!GameOver)
		{
			foreach (var (index, b) in Battles.Select((b, index) => (index, b)))
			{
				await Statics.Console.WriteLine();

				Player.Battle = b;
				bool battleWon = await b.Run();

				if (!battleWon)
				{
					await ConsoleHelper.WriteLine($"The heroes have lost! The Uncoded One's forces have prevailed...", ConsoleColor.Red);
					GameOver = true;
					break;
				}

				if (battleWon && index == Battles.Count - 1)
				{
					await Statics.Console.WriteLine("...");
					await Task.Delay(500);
					await Statics.Console.WriteLine("...");
					await Task.Delay(500);
					await Statics.Console.WriteLine("...");
					await Task.Delay(500);

					await Statics.Console.WriteLine("The Uncoded One begins to disintegrate, binary streams flowing out of it until it bursts apart in a dazzling bluelight.");
					await ConsoleHelper.WriteLine("You have done it... You have defeated the Uncoded One", ConsoleColor.Cyan);
					GameOver = true;
				}
			}
		}
	}

	private async Task PromptForGameplayMode()
	{
		List<IMenuItem> menuItems = [];
		foreach (GameplayMode mode in Enum.GetValues(typeof(GameplayMode)))
		{
			menuItems.Add(mode switch
			{
				GameplayMode.PlayerVsComputer => new MenuItem("Player vs Computer"),
				GameplayMode.ComputerVsComputer => new MenuItem("Computer vs Computer"),
				GameplayMode.HumanVsHuman => new MenuItem("Human vs Human"),
			});
		}
		await Menu.Menu.DisplayMenuItems("Choose the gameplay mode: ", menuItems);
		int userOption = await Menu.Menu.GetUserOption(menuItems.Count);
		Mode = (GameplayMode)userOption;

		await Statics.Console.Clear();
	}

	private void InitializeBattles()
	{
		PlayerType heroesPlayer = Mode != GameplayMode.ComputerVsComputer ? PlayerType.Human : PlayerType.Computer;
		PlayerType monstersPlayer = Mode == GameplayMode.HumanVsHuman? PlayerType.Human : PlayerType.Computer;

		Heroes = new Party(PartyType.Heroes, heroesPlayer);
		Heroes.Characters.Add(Player);

		Battle battle1 = new(Heroes, new Party(PartyType.Monsters, monstersPlayer));
		battle1.Monsters.Characters.AddRange([new Skeleton(battle1)]);

		Battle battle2 = new(Heroes, new Party(PartyType.Monsters, monstersPlayer));
		battle2.Monsters.Characters.AddRange([new Skeleton(battle2), new Skeleton(battle2)]);

		Battle battle3 = new(Heroes, new Party(PartyType.Monsters, monstersPlayer));
		battle3.Monsters.Characters.AddRange([new TheUncodedOne(battle3)]);

		Battles.AddRange([battle1, battle2, battle3]);
	}
}

public enum GameplayMode { PlayerVsComputer, ComputerVsComputer, HumanVsHuman };