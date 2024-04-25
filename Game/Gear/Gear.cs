using System;
using Endgame.Game.Interfaces;

namespace Endgame.Game.Gear;

public class Gear : IGearCore
{
	public event Action<IGearCore>? GearEquipped;
	public event Action<IGearCore>? GearUnequipped;

	public void Equip()
	{
		GearEquipped?.Invoke(this);
	}

	public void Unequip()
	{
		GearUnequipped?.Invoke(this);
	}
}
