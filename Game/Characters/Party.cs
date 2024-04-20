using System.Collections.Generic;

namespace Endgame.Game.Characters;

public class Party
{
    public PartyType Type { get; set; }
    public List<ICharacter> Characters { get; set; } = [];

    public Party(PartyType type)
    {
        Type = type;
        Characters.Add(new Skeleton());
    }
    public static Party HeroesParty { get; } = new Party(PartyType.Heroes);
    public static Party MonstersParty { get; } = new Party(PartyType.Monsters);
}

public enum PartyType { Heroes, Monsters };