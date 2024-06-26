﻿using Endgame.Game.Interfaces;
using System;
using System.Threading.Tasks;

namespace Endgame.Game.Items;

public class HealthPotion : Potion
{
	public override string Name { get; } = "Health potion";
	private readonly int _healingAmount = new Random().Next(7,14);

	public async override Task Use(object context)
	{
		if (context is ICharacter character)
		{
			await Statics.Console.Write($"{character.Name} used a ");
			await ConsoleHelper.WriteLine($"health potion", ConsoleColor.Red);
			var effectiveHealingAmount = character.HP + _healingAmount > character.MaxHP ? character.MaxHP - character.HP : _healingAmount;

			character.HP += effectiveHealingAmount;

			await Statics.Console.WriteLine($"Healed {effectiveHealingAmount} HP points.");
		}
	}
}
