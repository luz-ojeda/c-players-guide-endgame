using Endgame.Game.Attacks;

namespace Endgame.Game.Characters;

public class TheUncodedOne : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "The Uncoded One";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; set; } = 15;
	public float HP { get; set; }
	public IAttack Attack => new UnravelingAttack();

	public TheUncodedOne(Battle battle)
	{
		HP = MaxHP;
		Battle = battle;
	}
}
