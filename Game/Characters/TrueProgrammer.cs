using Endgame.Game.Attacks;
using Endgame.Game.Gear;
using Endgame.Game.Interfaces;
using Game.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TrueProgrammer : Character, IPartyCharacter
{
	public string Name { get; set; } = "TOG";
	public PartyType PartyType { get; } = PartyType.Heroes;
	public float MaxHP { get; } = 25;
	public List<ICharacterAttack> Attacks { get; set; } = [new PunchAttack()];
	public string Symbol { get; } = "☺";
	public bool CanUseItems { get; set; } = true;
	public IPartyGear? GearEquipped { get; set; }

	public TrueProgrammer()
	{
		HP = MaxHP;
		GearEquipped = new Sword();
		Attacks.Add(
			new GearAttack() with { Name = GearEquipped.AttackName, Damage = GearEquipped.Modifier }
			);
	}

	public async Task SetupName()
	{
		await Statics.Console.Write("Enter your character name: ");
		Statics.Console.ForegroundColor = ConsoleColor.Cyan;
		string name = await Statics.Console.ReadLine();
		Statics.Console.ResetColor();

		if (!string.IsNullOrWhiteSpace(name))
		{
			Name = name.Trim();
		}
		await Statics.Console.Clear();
	}
}
