using System;

namespace Endgame.Game.Attacks;

public interface IAttack
{
	public float Damage { get; }
	public string Name { get; }
	public ConsoleColor? Color { get; }
}
