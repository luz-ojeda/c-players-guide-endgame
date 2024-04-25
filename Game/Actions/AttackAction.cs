using Endgame.Game.Characters;
using Endgame.Game.Interfaces;
using Endgame.Game.Menu;
using Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class AttackAction : ITargetedAction
{
	public IPartyCharacter? Target { get; set; }
	public IAttack Attack { get; set; }
	public string Description { get; } = "Attack";
	public double? Weight { get; set; } = null;

	public AttackAction(IPartyCharacter? target)
	{
		Target = target;
	}

	public AttackAction() { }

	public async Task Run(IPartyCharacter character, Battle battle)
	{
		Party party = battle.GetPartyFor(character);
		PlayerType playerInControl = party.PlayerInControl;

		await SetTarget(character, battle.GetEnemyPartyFor(character), playerInControl);
		if (Target == null) return;

		await SetAttack(character, playerInControl);

		if (Target.HP - Attack.Damage <= 0)
		{
			Target.HP = 0;
			Target.Die();
		}
		else
		{
			Target.HP -= Attack.Damage;
		}

		await DisplayAttackInformation(character);

		if (Target.HP == 0)
		{
			await Statics.Console.WriteLine($"{Target.Name} has been defeated!");
		}
	}

	private async Task SetAttack(ICharacter character, PlayerType playerInControl)
	{
		// If user has nothing equipped return normal attack
		if (character.GearEquipped == null) Attack = character.Attacks[0];

		int attackIndex;
		if (playerInControl == PlayerType.Human)
		{
			List<IMenuItem> possibleAttacksMenuItems = [];
			foreach (IAttack a in character.Attacks)
			{
				possibleAttacksMenuItems.Add(new MenuItem(a.Name));
			}

			await Menu.Menu.DisplayMenuItems("Choose the attack: ", possibleAttacksMenuItems);
			attackIndex = await Menu.Menu.GetUserOption(possibleAttacksMenuItems.Count);
		}
		else
		{
			// Computer should almost always prefer attacking with equipped weapon
			List<double> attackOptionsWeights = ComputerPlayer.CalculateDynamicWeights(character.Attacks);
			attackIndex = ComputerPlayer.GetWeightedRandomIndex(attackOptionsWeights);
		}
		Attack = character.Attacks[attackIndex];
	}

	private async Task DisplayAttackInformation(ICharacter character)
	{
		await Statics.Console.Write($"{character.Name} used ");
		await DisplayAttackWithColor(Attack);
		await Statics.Console.WriteLine($" on {Target.Name}.");

		await DisplayAttackWithColor(Attack);
		await Statics.Console.WriteLine($" dealt {Attack.Damage} damage.");
		await Statics.Console.WriteLine($"{Target.Name} is now at {Target.HP}/{Target.MaxHP} HP.");
	}

	private async Task DisplayAttackWithColor(IAttack attack)
	{
		await ConsoleHelper.Write($"{Attack.Name}", Attack.Color ?? ConsoleColor.White);
	}

	private async Task SetTarget(IPartyCharacter character, Party enemyParty, PlayerType playerInControl)
	{
		if (enemyParty.Characters.Count > 1)
		{
			int targetIndex;
			if (playerInControl == PlayerType.Human)
			{
				List<IMenuItem> possibleTargets = [];
				foreach (ICharacter enemyCharacter in enemyParty.Characters)
				{
					possibleTargets.Add(new MenuItem(enemyCharacter.Name));
				}

				await Menu.Menu.DisplayMenuItems("Choose the target: ", possibleTargets);
				targetIndex = await Menu.Menu.GetUserOption(possibleTargets.Count);
			}
			else
			{
				targetIndex = new Random().Next(0, enemyParty.Characters.Count);
			}

			Target = enemyParty.Characters[targetIndex];
		}
		else
		{
			Target = enemyParty.Characters[0];
		}
	}
}
