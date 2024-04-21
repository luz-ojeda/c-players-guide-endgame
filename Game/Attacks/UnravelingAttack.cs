using System;

namespace Endgame.Game.Attacks;

public class UnravelingAttack : IAttack
{
	public float Damage { get; } = new Random().Next(3);
	public string Name { get; } = "UNRAVELING ATTACK";
}

