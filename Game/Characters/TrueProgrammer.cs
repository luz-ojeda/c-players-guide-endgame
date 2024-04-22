using Endgame.Game.Attacks;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TrueProgrammer : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "TOG";
	public PartyType PartyType { get; } = PartyType.Heroes;
	public float MaxHP { get; } = 25;
	public float HP { get; set; }
	public IAttack Attack => new PunchAttack();
	public string Symbol { get; } = "☺";
	public bool CanUseItems { get; set; } = true;
	public TrueProgrammer()
	{
		HP = MaxHP;
	}
	public async Task SetupName()
	{
		await Statics.Console.Write("Enter your character name: ");
		string name = await Statics.Console.ReadLine();
		if (!string.IsNullOrWhiteSpace(name))
		{
			Name = name.Trim();
		}
	}
}
