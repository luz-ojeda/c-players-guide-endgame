using Endgame.Game.Characters;
using Endgame.Game.Items;
using Endgame.Game.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class UseItemAction : ITargetedAction
{
	public ICharacter? Target { get; set; }
	public IItem Item { get; set; }

	public UseItemAction(ICharacter? target)
	{
		Target = target;
	}

	public async Task Run(ICharacter character, Battle battle)
	{
		Party party = battle.GetPartyFor(character);
		if (party.PlayerInControl == PlayerType.Computer)
		{
			Item = party.Items[0];
		}
		else
		{
			await SetItem(character, party);

		}
		await SetTarget(character, party);

		await Item.Use(Target);
		battle.RemoveItemFromPartyItems(character, Item);
	}

	private async Task SetItem(ICharacter character, Party characterParty)
	{
		List<IMenuItem> possibleItems = [];
		foreach (IItem i in characterParty.Items)
		{
			possibleItems.Add(new MenuItem(i.Name));
		}
		await Menu.Menu.DisplayMenuItems("Choose the item: ", possibleItems);
		int itemIndex = await Menu.Menu.GetUserOption(possibleItems.Count);
		Item = characterParty.Items[itemIndex];
	}

	private async Task SetTarget(ICharacter character, Party characterParty)
	{
		if (characterParty.Characters.Count > 1 && characterParty.PlayerInControl == PlayerType.Human)
		{
			List<IMenuItem> possibleTargets = [];
			foreach (ICharacter c in characterParty.Characters)
			{
				possibleTargets.Add(new MenuItem(c.Name));
			}

			await Menu.Menu.DisplayMenuItems("Choose the target: ", possibleTargets);
			int targetIndex = await Menu.Menu.GetUserOption(possibleTargets.Count);
			Target = characterParty.Characters[targetIndex];
		}
		else
		{
			Target = character;
		}
	}
}
