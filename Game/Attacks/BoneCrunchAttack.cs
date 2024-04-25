using System;

namespace Endgame.Game.Attacks;

public record BoneCrunchAttack(
	string Name = "BONE CRUNCH",
	ConsoleColor? Color = ConsoleColor.DarkGray
	) : Attack, ICharacterAttack
{
	public float Damage => new Random().Next(3);
}
