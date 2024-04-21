using Endgame.Game.Actions;
using Endgame.Game.Characters;
using Endgame.Game.Menu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Endgame.Game;
public class Battle
{
	public Party Heroes { get; set; }
	public Party Monsters { get; set; }
	public PartyType Turn { get; set; }
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
				await Statics.ConsoleHelper.WriteLine($"The heroes have won the battle!", ConsoleColor.Green);
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

	private async Task PlayTurn(Party party)
	{
		foreach (ICharacter character in party.Characters)
		{
			if (Monsters.Characters.Count == 0 || Heroes.Characters.Count == 0) return;

			await Statics.Console.WriteLine();
			await Statics.Console.WriteLine($"It's {character.Name} turn...");

			IAction action;
			if (party.PlayerInControl == PlayerType.Human)
			{
				action = await PromptForAction(character);
			}
			else
			{
				List<IAction> actions = [
					new DoNothingAction(),
					new AttackAction(GetEnemyPartyFor(character).Characters[0])];

				action = actions[new Random().Next(0, 2)];
			}

			if (action is ITargetedAction targetedAction)
			{
				await targetedAction.SetTarget(character, GetEnemyPartyFor(character), GetPartyFor(character));
			}

			await action.Run(character, this);
		}
	}

	private async Task<IAction> PromptForAction(ICharacter c)
	{
		List<IAction> actions = [new DoNothingAction(), new AttackAction()];

		List<IMenuItem> menuItems = [];
		foreach (IAction action in actions)
		{
			menuItems.Add(action switch
			{
				DoNothingAction => new ActionMenuItem("Do Nothing", true, action),
				AttackAction or _ => new ActionMenuItem("Standard Attack", true, action),
			});
		}

		await Menu.Menu.DisplayMenuItems("Choose action: ", menuItems);
		await Statics.Console.Write("What do you want to do? ");
		return actions[await Menu.Menu.GetUserOption(menuItems.Count)];
	}

	private Party GetPartyFor(ICharacter c) => c.PartyType == PartyType.Heroes ? Heroes : Monsters;

	private Party GetEnemyPartyFor(ICharacter c) => c.PartyType == PartyType.Monsters ? Heroes : Monsters;
}
