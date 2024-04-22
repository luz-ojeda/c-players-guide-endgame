using System;

namespace Endgame.Game.Attacks;

public class UnravelingAttack : IAttack
{
	public float Damage { get; } = new Random().Next(1, 3);
	public string Name { get; } = "UNRAVELING ATTACK";
	public ConsoleColor? Color { get; } = ConsoleColor.Red;
}

