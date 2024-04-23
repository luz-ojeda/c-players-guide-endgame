using Endgame.Game.Attacks;
using Game.Enums;

namespace Endgame.Game.Characters;

public class Skeleton : Character, IPartyCharacter
{
	public string Name { get; set; } = "Skeleton";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; set; } = 5;
	public IAttack Attack => new BoneCrunchAttack();
	public string Symbol { get; } = "$";
	public bool CanUseItems { get; set; } = true;

	public Skeleton(Battle battle)
	{
		HP = MaxHP;
		Battle = battle;
	}
}

