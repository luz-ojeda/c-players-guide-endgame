using System;

namespace Endgame.Game.Items;

public class Item : IItemCore
{
	public event Action<IItemCore>? ItemUsed;

	public void RemoveFromItems()
	{
		ItemUsed?.Invoke(this);
	}
}
