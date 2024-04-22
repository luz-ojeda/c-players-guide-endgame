using Endgame.Game.Attacks;

namespace Endgame.Game.Characters;

public class TheUncodedOne : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "The Uncoded One";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; } = 15;
	public float HP { get; set; }
	public IAttack Attack => new UnravelingAttack();
	public string Symbol { get; } = "&";
	public bool CanUseItems { get; set; } = true;
	public TheUncodedOne(Battle battle)
	{
		HP = MaxHP;
		Battle = battle;
	}
}
