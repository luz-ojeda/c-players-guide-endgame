using Endgame.Game.Characters;

namespace Endgame.Game.Actions;

public interface ITargetedAction : IAction
{
	IPartyCharacter? Target { get; set; }
}