using System.Collections.Generic;
using System.Threading.Tasks;

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
	public async Task EvaluatePartyState()
	{
		foreach (var c in Characters)
		{
			if (c.HP == 0)
			{
				Characters.Remove(c);
				await Statics.Console.WriteLine($"{c.Name} has been defeated!");
			}
		}
	}
}

public enum PartyType { Heroes, Monsters };
public enum PlayerType { Computer, Human };