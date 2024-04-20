using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TrueProgrammer : ICharacter
{
	public CharacterType Type => CharacterType.TrueProgrammer;
	public string Name { get; set; } = "TOG";

	public async Task Act()
	{
		await Statics.Console.WriteLine($"{Name} did NOTHING.");
	}
}
