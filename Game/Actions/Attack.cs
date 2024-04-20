using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class Attack : IAction
{
	private readonly ICharacter _target;

	public Attack(ICharacter target)
	{
		_target = target;
	}

	public async Task Run(ICharacter character, Battle battle)
	{
		await Statics.Console.WriteLine($"{character.Name} used {character.AttackName} on {_target.Name}.");
	}
}
