using System;

namespace Endgame.Game.Attacks;

public class BoneCrunchAttack : IAttack
{
	public float Damage { get; } = new Random().Next(2);
	public string Name { get; } = "BONE CRUNCH";
}
