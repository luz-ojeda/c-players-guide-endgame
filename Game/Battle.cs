using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game;
public class Battle
{
	public Battle(Party heroesParty, Party monstersParty)
	{
		HeroesParty = heroesParty;
		MonstersParty = monstersParty;
	}

	public Party HeroesParty { get; }
	public Party MonstersParty { get; }
	public PartyType Turn { get; set; }
	public bool BattleOver { get; set; } = false;

	public async Task Run()
	{
		while (!BattleOver)
		{
			foreach(ICharacter c in HeroesParty.Characters)
			{
				await Statics.Console.WriteLine($"It's {c.Type} turn...");
				await c.Act();
				await Task.Delay(1000);
			}

			await Statics.Console.WriteLine();

			foreach (ICharacter c in MonstersParty.Characters)
			{
				await Statics.Console.WriteLine($"It's {c.Type} turn...");
				await c.Act();
				await Task.Delay(1000);
			}
		}
	}
}
