using Endgame.Game.Interfaces;
using Endgame.Game.Items;
using Game.Enums;
using System.Collections.Generic;
namespace Endgame.Game.Characters;

public class Party
{
	public PartyType Type { get; set; }
	public List<IPartyCharacter> Characters { get; set; } = [];
	public PlayerType PlayerInControl { get; set; }
	public List<IPartyItem> Items { get; set; } = [];
	public List<IPartyGear> Gear { get; set; }

	public Party(
		PartyType type,
		PlayerType playerInControl = PlayerType.Computer,
		List<IPartyGear>? gear = null)
	{
		Type = type;
		PlayerInControl = playerInControl;
		Gear = gear ?? [];

		if (Type == PartyType.Heroes)
		{
			Items = [new HealthPotion(), new HealthPotion(), new HealthPotion()];
		}
		else
		{
			Items = [new HealthPotion()];
		}

		AttachEvents();
	}

	public void OnCharacterDied(ICharacterCore characterCore)
	{
		if (characterCore is IPartyCharacter character && Characters.Contains(character))
		{
			Characters.Remove(character);
		}
	}

	public void OnItemUsed(IItemCore itemCore)
	{
		if (itemCore is IPartyItem item && Items.Contains(item))
		{
			Items.Remove(item);
		}
	}

	public void OnGearEquipped(IGearCore gearCore)
	{
		if (gearCore is IPartyGear gear && Gear.Contains(gear))
		{
			Gear.Remove(gear);
		}
	}

	public void OnGearUnequipped(IGearCore gearCore)
	{
		if (gearCore is IPartyGear gear)
		{
			Gear.Add(gear);
		}
	}

	private void AttachEvents()
	{
		foreach (IPartyCharacter character in Characters)
		{
			character.CharacterDied += OnCharacterDied;
		}

		foreach (IPartyItem item in Items)
		{
			item.ItemUsed += OnItemUsed;
		}

		foreach (IPartyGear gear in Gear)
		{
			gear.GearEquipped += OnGearEquipped;
		}

		foreach (IPartyGear gear in Gear)
		{
			gear.GearUnequipped += OnGearUnequipped;
		}
	}
}
