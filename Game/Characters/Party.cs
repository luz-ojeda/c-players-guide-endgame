using System.Collections.Generic;

namespace Endgame.Game.Characters;

public class Party
{
	public PartyType Type { get; set; }
	public List<ICharacter> Characters { get; set; } = [];
	public PlayerType PlayerInControl { get; set; }

	public Party(PartyType type, PlayerType playerInControl = PlayerType.Computer)
	{
		Type = type;
		PlayerInControl = playerInControl;
	}
}

public enum PartyType { Heroes, Monsters };
public enum PlayerType { Computer, Human };