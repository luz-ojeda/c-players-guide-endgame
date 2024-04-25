namespace Endgame.Game.Interfaces;

public interface IGear
{
    string Name { get; }
    string? AttackName { get; }
    float Modifier { get; }
}
