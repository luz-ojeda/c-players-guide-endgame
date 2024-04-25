using System;

namespace Endgame.Game.Attacks;

public record UnravelingAttack(
	string Name = "UNRAVELING ATTACK",
	ConsoleColor? Color = ConsoleColor.Red
	) : Attack, ICharacterAttack
{
	public float Damage => new Random().Next(1, 3);
}

