using Endgame.Game.Attacks;
using Endgame.Game.Characters;
using Endgame.Game.Interfaces;
using Endgame.Game.Menu;
using Game.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class EquipAction : IAction
{
	public IPartyCharacter Target { get; set; }
	public IPartyGear Gear { get; set; }
	public string Description { get; } = "Equip gear";
	public double? Weight { get; set; } = 0.4;

	public EquipAction(IPartyCharacter target)
	{
		Target = target;
	}

	public async Task Run(IPartyCharacter character, Battle battle)
	{
		Party party = battle.GetPartyFor(character);

		if (character.GearEquipped != null)
		{
			character.GearEquipped.Unequip();
		}

		await SetGear(character, party);
		character.GearEquipped = Gear;
		character.Attacks.Add(
			new GearAttack() with { Name = Gear.AttackName, Damage = Gear.Modifier }
			);
		Gear.Equip();

		await Statics.Console.WriteLine($"{character.Name} equipped {Gear.Name}.");
	}

	private async Task SetGear(ICharacter character, Party characterParty)
	{
		if (characterParty.Gear.Count > 1 && characterParty.PlayerInControl == PlayerType.Human)
		{
			List<IMenuItem> possibleGear = [];
			foreach (IPartyGear g in characterParty.Gear)
			{
				possibleGear.Add(new MenuItem(g.Name));
			}
			await Menu.Menu.DisplayMenuItems("Choose the gear: ", possibleGear);
			int itemIndex = await Menu.Menu.GetUserOption(possibleGear.Count);

			Gear = characterParty.Gear[itemIndex];
		}
		else
		{
			Gear = characterParty.Gear[0];
		}	
	}
}
