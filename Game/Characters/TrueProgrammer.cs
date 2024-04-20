using Endgame.Game.Actions;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TrueProgrammer : ICharacter
{
	public string Name { get; set; } = "TOG";
	public CharacterType Type => CharacterType.TrueProgrammer;

	public async Task Act(IAction action)
	{
		await Task.Delay(1000);
		await action.Run(this);
	}
}
