using System.Threading.Tasks;

namespace Endgame.Game.Interfaces;

public interface IAction : IWeightedOption
{
    Task Run(IPartyCharacter character, Battle battle);
    string Description { get; }
}

