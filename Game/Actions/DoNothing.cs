using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class DoNothing : IAction
{
	public async Task Run(ICharacter character)
	{
		await Statics.Console.WriteLine($"{character.Name} did NOTHING.");
	}
}
