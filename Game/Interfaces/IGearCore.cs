using System;

namespace Endgame.Game.Interfaces;

public interface IGearCore
{
    event Action<IGearCore>? GearEquipped;
    event Action<IGearCore>? GearUnequipped;
    void Equip();
    void Unequip();
}
