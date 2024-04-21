using Endgame.Game.Actions;
using Endgame.Game.Attacks;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TrueProgrammer : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "TOG";
	public CharacterType Type => CharacterType.TrueProgrammer;
	public PartyType PartyType { get; } = PartyType.Heroes;
	public float MaxHP { get; set; } = 25;
	public float HP { get; set; } = 25;
	public IAttack Attack => new PunchAttack();

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
