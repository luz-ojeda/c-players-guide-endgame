using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class Skeleton : ICharacter
{
	public CharacterType Type => CharacterType.Skeleton;

	public async Task Act()
	{
		await Statics.Console.WriteLine("SKELETON did NOTHING.");
	}
}

