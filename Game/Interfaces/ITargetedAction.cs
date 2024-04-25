namespace Endgame.Game.Interfaces;

public interface ITargetedAction : IAction
{
    IPartyCharacter? Target { get; set; }
}