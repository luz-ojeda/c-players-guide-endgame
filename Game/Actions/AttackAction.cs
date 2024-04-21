using Endgame.Game.Attacks;
using Endgame.Game.Characters;
using Endgame.Game.Menu;
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

		await Statics.Console.WriteLine($"{character.Name} used {attack.Name} on {Target.Name}.");
		await Statics.Console.WriteLine($"{attack.Name} dealt {damage} damage to {Target.Name}.");
		await Statics.Console.WriteLine($"{Target.Name} is now at {Target.HP}/{Target.MaxHP}.");

		if (Target.HP == 0)
		{
			await Statics.Console.WriteLine($"{Target.Name} has been defeated!");
			battle.RemoveCharacterFromParty(Target);
		}
	}

	public async Task SetTarget(ICharacter character, Party enemyParty, Party party)
	{
		if (enemyParty.Characters.Count > 1)
		{
			List<IMenuItem> possibleTargets = [];
			foreach (ICharacter enemyCharacter in enemyParty.Characters)
			{
				possibleTargets.Add(new MenuItem(enemyCharacter.Name));
			}

			await Menu.Menu.DisplayMenuItems("Choose the target: ", possibleTargets);
			int targetIndex = await Menu.Menu.GetUserOption(possibleTargets.Count);

			Target = enemyParty.Characters[targetIndex];
		}
		else
		{
			Target = enemyParty.Characters[0];
		}
	}
}
