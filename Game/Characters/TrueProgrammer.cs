using Endgame.Game.Actions;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TrueProgrammer : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "TOG";
	public CharacterType Type => CharacterType.TrueProgrammer;
	public string AttackName { get; } = "PUNCH";
	public PartyType PartyType { get; } = PartyType.Heroes;

	public TrueProgrammer(string name)
	{
		Name = name;
	}

	public async Task Act(IAction action)
	{
		await Task.Delay(1000);
		await action.Run(this, Battle);
	}
}
