using Endgame.Game.Actions;
using Endgame.Game.Characters;
using System;
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
			await PlayTurn(HeroesParty);
			await PlayTurn(MonstersParty);
		}
	}

	private static async Task PlayTurn(Party party)
	{
		await Statics.Console.WriteLine();
		foreach (ICharacter c in party.Characters)
		{
			await Statics.Console.WriteLine($"It's {c.Name} turn...");

			IAction action;
			if (party.PlayerInControl == PlayerType.Human)
			{
				action = await PromptForAction();
			}
			else
			{
				action = new DoNothing();
			}

			await c.Act(action);
		}
	}

	private static async Task<IAction> PromptForAction()
	{
		await Statics.Console.WriteLine("Choose action: ");
		return new DoNothing();
	}
}
