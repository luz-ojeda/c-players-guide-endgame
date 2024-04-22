using Endgame.Game.Attacks;
using Endgame.Game.Characters;
using Endgame.Game.Menu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class AttackAction : ITargetedAction
{
	public ICharacter? Target { get; set; }

	public AttackAction(ICharacter? target)
	{
		Target = target;
	}

	public AttackAction() { }

	public async Task Run(ICharacter character, Battle battle)
	{
		await SetUp(character, battle.GetEnemyPartyFor(character), battle.GetPartyFor(character));
		if (Target == null) return;

		IAttack attack = character.Attack;
		float damage = attack.Damage;
		if (Target.HP - damage <= 0)
		{
			Target.HP = 0;
		}
		else
		{
			Target.HP -= damage;
		}

		await Statics.Console.Write($"{character.Name} used ");
		await ConsoleHelper.Write($"{attack.Name}", attack.Color ?? ConsoleColor.White);
		await Statics.Console.WriteLine($" on {Target.Name}.");

		await Statics.Console.WriteLine($"{attack.Name} dealt {damage} damage.");
		await Statics.Console.WriteLine($"{Target.Name} is now at {Target.HP}/{Target.MaxHP} HP.");

		if (Target.HP == 0)
		{
			await Statics.Console.WriteLine($"{Target.Name} has been defeated!");
			battle.RemoveCharacterFromParty(Target);
		}
	}

	public async Task SetUp(ICharacter character, Party enemyParty, Party party)
	{
		if (enemyParty.Characters.Count > 1)
		{
			int targetIndex;
			if (party.PlayerInControl == PlayerType.Human)
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
