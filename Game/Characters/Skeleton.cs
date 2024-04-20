using Endgame.Game.Actions;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class Skeleton : ICharacter
{
	public string Name { get; set; } = "Skeleton";
	public CharacterType Type => CharacterType.Skeleton;

	public async Task Act(IAction action)
	{
		await Task.Delay(1000);
		await action.Run(this);
	}
}

