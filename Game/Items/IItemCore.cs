using System;

namespace Endgame.Game.Items;

public interface IItemCore
{
	event Action<IItemCore>? ItemUsed;
	void RemoveFromItems();
}
