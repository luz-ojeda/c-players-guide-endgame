using Endgame.Game.Attacks;
using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class AttackAction : IAction
{
	private readonly ICharacter _target;

	public AttackAction(ICharacter target)
	{
		_target = target;
	}

	public async Task Run(ICharacter character, Battle battle)
	{
		IAttack attack = character.Attack;
		float damage = attack.Damage;
		if (_target.HP - damage <= 0)
		{
			_target.HP = 0;
			// ded
		}
		else
		{
			_target.HP -= damage;
		}

		await Statics.Console.WriteLine($"{character.Name} used {attack.Name} on {_target.Name}.");
		await Statics.Console.WriteLine($"{attack.Name} dealt {damage} damage to {_target.Name}.");
		await Statics.Console.WriteLine($"{_target.Name} is now at {_target.HP}/{_target.MaxHP}.");
		if (_target.HP == 0) await Statics.Console.WriteLine($"{_target.Name} is dead!");

	}
}
