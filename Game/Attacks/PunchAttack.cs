using System;

namespace Endgame.Game.Attacks;

public record PunchAttack(
	float Damage = 5,
	string Name = "PUNCH",
	ConsoleColor? Color = ConsoleColor.Cyan
	): IAttack;
