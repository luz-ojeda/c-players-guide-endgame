using Endgame.Game.Characters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Endgame.Game;
public class Battle
{
	public Party HeroesParty { get; set; }
	public Party MonstersParty { get; set; }
	public PartyType Turn { get; set; }
	public bool BattleOver { get; set; } = false;

	public Battle(Party heroesParty, Party monstersParty)
	{
		HeroesParty = heroesParty;
		MonstersParty = monstersParty;
	}

	public async Task Run()
	{
		while (!BattleOver)
		{
			await PlayTurn(HeroesParty.Characters);
			await PlayTurn(MonstersParty.Characters);
		}
	}

	private static async Task PlayTurn(List<ICharacter> characters)
	{
		await Statics.Console.WriteLine();
		foreach (ICharacter c in characters)
		{
			await Statics.Console.WriteLine($"It's {c.Name} turn...");
			await c.Act();
			await Task.Delay(1000);
		}
	}
}
