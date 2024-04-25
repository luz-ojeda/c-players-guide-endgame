using Endgame.Game.Attacks;
using Game.Enums;
using System.Collections.Generic;

namespace Endgame.Game.Interfaces;

public interface ICharacter
{
    string Name { get; set; }
    PartyType PartyType { get; }
    float MaxHP { get; }
    float HP { get; set; }
    List<ICharacterAttack> Attacks { get; set; }
    string Symbol { get; }
    bool CanUseItems { get; set; }
    IPartyGear? GearEquipped { get; set; }
    string CurrentHP => $"({HP}/{MaxHP})";
}
