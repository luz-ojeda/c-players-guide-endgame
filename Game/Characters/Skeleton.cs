using Endgame.Game.Attacks;
using Endgame.Game.Interfaces;
using Game.Enums;
using System.Collections.Generic;

namespace Endgame.Game.Characters;

public class Skeleton : Character, IPartyCharacter
{
	public string Name { get; set; } = "Skeleton";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; set; } = 5;
	public List<ICharacterAttack> Attacks { get; set; } = [new BoneCrunchAttack()];
	public string Symbol { get; } = "$";
	public bool CanUseItems { get; set; } = true;
	public IPartyGear? GearEquipped { get; set; }

	public Skeleton(Battle battle, IPartyGear? gear = null)
	{
		HP = MaxHP;
		Battle = battle;
		GearEquipped = gear;
		if (GearEquipped != null)
		{
			Attacks.Add(
				new GearAttack() with { Name = GearEquipped.AttackName, Damage = GearEquipped.Modifier }
			);
		}
	}
}

