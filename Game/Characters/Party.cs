using System.Collections.Generic;

namespace Endgame.Game.Characters;

public class Party
{
	public PartyType Type { get; set; }
	public List<ICharacter> Characters { get; set; } = [];
	public PlayerType PlayerInControl { get; set; } = PlayerType.Human;

	public Party(PartyType type)
	{
		Type = type;
	}
}

public enum PartyType { Heroes, Monsters };
public enum PlayerType { Computer, Human };