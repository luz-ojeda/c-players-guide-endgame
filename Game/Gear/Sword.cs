using Endgame.Game.Interfaces;

namespace Endgame.Game.Gear;

public class Sword : Gear, IPartyGear
{
	public string Name { get; } = "Sword";
	public float Modifier { get; } = 2;
	public string? AttackName { get; } = "SLASH";
}
