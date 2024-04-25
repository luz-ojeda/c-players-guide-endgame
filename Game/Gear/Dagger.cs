using Endgame.Game.Interfaces;

namespace Endgame.Game.Gear;

public class Dagger : Gear, IPartyGear
{
	public string Name { get; } = "Dagger";
	public float Modifier { get; } = 1;
	public string? AttackName { get; } = "STAB";
}
