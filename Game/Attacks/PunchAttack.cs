using System;

namespace Endgame.Game.Attacks;

public record PunchAttack(
	float Damage = 1,
	string Name = "PUNCH",
	ConsoleColor? Color = ConsoleColor.DarkYellow
	): IAttack;
