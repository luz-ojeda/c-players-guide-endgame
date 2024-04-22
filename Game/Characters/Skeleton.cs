using Endgame.Game.Attacks;

namespace Endgame.Game.Characters;

public class Skeleton : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "Skeleton";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; set; } = 5;
	public float HP { get; set; }
	public IAttack Attack => new BoneCrunchAttack();
	public string Symbol { get; } = "$";
	public bool CanUseItems { get; set; } = true;

	public Skeleton(Battle battle)
	{
		HP = MaxHP;
		Battle = battle;
	}
}

