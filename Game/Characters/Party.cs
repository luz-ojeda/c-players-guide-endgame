﻿using System.Collections.Generic;

namespace Endgame.Game.Characters;

public class Party
{
	public PartyType Type { get; set; }
	public List<ICharacter> Characters { get; set; } = [];
	public PlayerType PlayerInControl { get; set; } = PlayerType.Computer;

	public Party(PartyType type)
	{
		Type = type;
		if (type == PartyType.Monsters)
		{
			Characters.Add(new Skeleton());
		}
	}
	public static Party HeroesParty { get; } = new Party(PartyType.Heroes);
	public static Party MonstersParty { get; } = new Party(PartyType.Monsters);
}

public enum PartyType { Heroes, Monsters };
public enum PlayerType { Computer, Human };