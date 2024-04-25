using Endgame.Game.Attacks;
using Endgame.Game.Interfaces;
using Game.Enums;
using System.Collections.Generic;

namespace Endgame.Game.Characters;

public class TheUncodedOne : Character, IPartyCharacter
{
	public string Name { get; set; } = "The Uncoded One";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; } = 15;
	public List<ICharacterAttack> Attacks { get; set; } = [new UnravelingAttack()];
	public string Symbol { get; } = "&";
	public bool CanUseItems { get; set; } = true;
	public IPartyGear GearEquipped { get; set; }

	public TheUncodedOne(Battle battle)
	{
		HP = MaxHP;
		Battle = battle;
	}
}
