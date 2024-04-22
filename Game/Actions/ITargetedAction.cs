using Endgame.Game.Characters;

namespace Endgame.Game.Actions;

public interface ITargetedAction : IAction
{
	public ICharacter? Target { get; set; }
}