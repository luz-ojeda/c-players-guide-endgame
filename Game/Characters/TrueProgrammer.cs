using Endgame.Game.Attacks;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TrueProgrammer : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "TOG";
	public PartyType PartyType { get; } = PartyType.Heroes;
	public float MaxHP { get; set; } = 25;
	public float HP { get; set; }
	public IAttack Attack => new PunchAttack();

	public TrueProgrammer()
	{
		HP = MaxHP;
	}
	public async Task SetupName()
	{
		await Statics.Console.Write("Enter your character name: ");
		Name = await Statics.Console.ReadLine();
	}
}
