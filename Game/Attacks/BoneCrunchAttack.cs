using System;

namespace Endgame.Game.Attacks;

public record BoneCrunchAttack(
	string Name = "BONE CRUNCH",
	ConsoleColor? Color = ConsoleColor.DarkGray
	) : IAttack
{
	public float Damage { get; } = new Random().Next(3);
}
