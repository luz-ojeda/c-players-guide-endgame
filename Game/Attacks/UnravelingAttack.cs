using System;

namespace Endgame.Game.Attacks;

public record UnravelingAttack(
	string Name = "UNRAVELING ATTACK",
	ConsoleColor? Color = ConsoleColor.Red
	) : IAttack
{
	public float Damage { get; } = new Random().Next(1, 3);
}

