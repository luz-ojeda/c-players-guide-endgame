using Endgame.Game.Actions;
using Endgame.Game.Characters;
using Endgame.Game.Items;
using Endgame.Game.Menu;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Endgame.Game;
public class Battle
{
	public Party Heroes { get; set; }
	public Party Monsters { get; set; }
	public bool BattleOver { get; set; } = false;
	public bool HeroesWon { get; set; } = false;

	public Battle(Party heroes, Party monsters)
	{
		Heroes = heroes;
		Monsters = monsters;
	}
	public async Task<bool> Run()
	{
		while (!BattleOver)
		{
			await PlayTurn(Heroes);

			if (Monsters.Characters.Count == 0)
			{
				await Statics.Console.WriteLine();
				await ConsoleHelper.WriteLine($"The heroes have won the battle!", ConsoleColor.Green);
				BattleOver = true;
				HeroesWon = true;
			}
			else
			{
				await PlayTurn(Monsters);

				if (Heroes.Characters.Count == 0)
				{
					BattleOver = true;
				}
			}

		}
		return HeroesWon;
	}

	public void RemoveCharacterFromParty(ICharacter character)
	{
		if (character.PartyType == PartyType.Monsters)
		{
			Monsters.Characters.Remove(character);
		}
		else
		{
			Heroes.Characters.Remove(character);
		}
	}
	public void RemoveItemFromPartyItems(ICharacter character, IItem item)
	{
		if (character.PartyType == PartyType.Monsters)
		{
			Monsters.Items.Remove(item);
		}
		else
		{
			Heroes.Items.Remove(item);
		}
	}

	private async Task PlayTurn(Party party)
	{
		foreach (ICharacter character in party.Characters)
		{
			if (Monsters.Characters.Count == 0 || Heroes.Characters.Count == 0) return;

			await DisplayBattleStatus(character);
			await Statics.Console.WriteLine();
			await Statics.Console.WriteLine($"It's {character.Name} turn...");
			await Statics.Console.WriteLine();

			IAction action;
			if (party.PlayerInControl == PlayerType.Human)
			{
				action = await PlayHumanTurn(character, party);
			}
			else
			{
				action = PlayComputerTurn(character, party);
			}

			await action.Run(character, this);
		}
	}

	public Party GetPartyFor(ICharacter c) => c.PartyType == PartyType.Heroes ? Heroes : Monsters;

	public Party GetEnemyPartyFor(ICharacter c) => c.PartyType == PartyType.Monsters ? Heroes : Monsters;

	private async Task DisplayBattleStatus(ICharacter currentCharacter)
	{
		const int characterCount = 97;

		await Statics.Console.WriteLine();
		await Statics.Console.WriteLine("============================================= BATTLE ============================================");

		foreach (ICharacter c in Heroes.Characters)
		{
			ConsoleColor color = currentCharacter == c ? ConsoleColor.Yellow : ConsoleColor.White;
			await ConsoleHelper.WriteLine($"({c.HP} / {c.MaxHP}) {c.Name} {c.Symbol}", color);
		}

		await Statics.Console.WriteLine("----------------------------------------------- VS ----------------------------------------------");

		foreach (ICharacter c in Monsters.Characters)
		{
			ConsoleColor color = currentCharacter == c ? ConsoleColor.Yellow : ConsoleColor.White;
			string characterInfo = $"{c.Symbol} {c.Name} ({c.HP} / {c.MaxHP})";
			await ConsoleHelper.WriteLine($"{characterInfo,characterCount}", color);
		}

		await Statics.Console.WriteLine("=================================================================================================");
	}

	private async Task<IAction> PlayHumanTurn(ICharacter character, Party party)
	{
		List<IAction> actions = GetPossibleActions(character, party);

		List<IMenuItem> menuItems = [];
		foreach (IAction action in actions)
		{
			menuItems.Add(action switch
			{
				DoNothingAction => new ActionMenuItem("Do Nothing", true, action),
				AttackAction => new ActionMenuItem("Standard Attack", true, action),
				UseItemAction => new ActionMenuItem("Use Item", character.CanUseItems, action),
			});
		}

		await Menu.Menu.DisplayMenuItems("Choose action: ", menuItems);
		await Statics.Console.Write("What do you want to do? ");
		return actions[await Menu.Menu.GetUserOption(menuItems.Count)];
	}

	private IAction PlayComputerTurn(ICharacter character, Party party)
	{
		List<IAction> actions = GetPossibleActions(character, party);

		return actions[new Random().Next(0, actions.Count)];
	}

	private List<IAction> GetPossibleActions(
		ICharacter character,
		Party party)
	{
		List<IAction> actions = [
			new DoNothingAction(),
			new AttackAction(GetEnemyPartyFor(character).Characters[0])];

		if (party.Items.Count == 0) return actions;

		if (party.PlayerInControl == PlayerType.Human ||
			(character.HP < (character.MaxHP * 0.25) && party.PlayerInControl == PlayerType.Computer))
		{
			actions.Add(new UseItemAction(GetPartyFor(character).Characters[0]));
		}

		return actions;
	}
}
