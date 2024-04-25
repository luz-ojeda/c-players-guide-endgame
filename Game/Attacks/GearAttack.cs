using System;

namespace Endgame.Game.Attacks;

public record GearAttack(
	string Name = "ATTACK WITH WEAPON",
	ConsoleColor? Color = ConsoleColor.DarkGray
	) : Attack, ICharacterAttack
{
	public float Damage { get; set; }
	public override double? Weight { get; set; } = 0.85;
}
