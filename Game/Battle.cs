using Endgame.Game.Actions;
using Endgame.Game.Characters;
using Endgame.Game.Interfaces;
using Endgame.Game.Menu;
using Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

	private async Task PlayTurn(Party party)
	{
		foreach (IPartyCharacter character in party.Characters)
		{
			if (Monsters.Characters.Count == 0 || Heroes.Characters.Count == 0) return;

			await DisplayBattleStatus(character);
			await Statics.Console.WriteLine();
			await Statics.Console.WriteLine($"It's {character.Name} turn...");
			await Statics.Console.WriteLine();
			if (party.PlayerInControl == PlayerType.Computer)
			{
				await Task.Delay(1500);
			}

			IAction action;
			if (party.PlayerInControl == PlayerType.Human)
			{
				action = await HumanChooseAction(character, party);
			}
			else
			{
				action = ComputerChooseAction(character, party);
			}

			await action.Run(character, this);
			if (party.PlayerInControl == PlayerType.Computer)
			{
				await Task.Delay(new Random().Next(2) * 1000);
			}
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
			await ConsoleHelper.WriteLine($"{c.CurrentHP} {c.Name} {c.Symbol}", color);
		}

		await Statics.Console.WriteLine("----------------------------------------------- VS ----------------------------------------------");

		foreach (ICharacter c in Monsters.Characters)
		{
			ConsoleColor color = currentCharacter == c ? ConsoleColor.Yellow : ConsoleColor.White;
			string characterInfo = $"{c.Symbol} {c.Name} {c.CurrentHP}";
			await ConsoleHelper.WriteLine($"{characterInfo,characterCount}", color);
		}

		await Statics.Console.WriteLine("=================================================================================================");
	}

	private async Task<IAction> HumanChooseAction(ICharacter character, Party party)
	{
		List<IAction> actions = GetPossibleActions(character, party);

		List<IMenuItem> menuItems = [];
		foreach (IAction action in actions)
		{
			menuItems.Add(new ActionMenuItem(action.Description, character.CanUseItems, action));
		}

		await Menu.Menu.DisplayMenuItems("Choose action: ", menuItems);
		await Statics.Console.Write("What do you want to do? ");
		await Statics.Console.WriteLine();
		return actions[await Menu.Menu.GetUserOption(menuItems.Count)];
	}

	private IAction ComputerChooseAction(ICharacter character, Party party)
	{
		List<IAction> actions = GetPossibleActions(character, party);

		List<double> weights = ComputerPlayer.CalculateDynamicWeights(actions);
		int actionIndex = ComputerPlayer.GetWeightedRandomIndex(weights);
		return actions[actionIndex];
	}

	private List<IAction> GetPossibleActions(
		ICharacter character,
		Party party)
	{
		List<IAction> actions = [
			new DoNothingAction(),
			new AttackAction(GetEnemyPartyFor(character).Characters[0])];


		if (party.Items.Count > 0 &&
			(party.PlayerInControl == PlayerType.Human ||
			(party.PlayerInControl == PlayerType.Computer && character.HP < (character.MaxHP * 0.25))))
		{
			actions.Add(new UseItemAction(GetPartyFor(character).Characters[0], character));
		}

		if (party.Gear.Count > 0 && character is IPartyCharacter partyCharacter)
		{
			actions.Add(new EquipAction(partyCharacter));
		}

		return actions;
	}
}
