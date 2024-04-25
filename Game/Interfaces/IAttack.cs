using System;

namespace Endgame.Game.Interfaces;

public interface IAttack
{
    float Damage { get; }
    string Name { get; }
    ConsoleColor? Color { get; }
}
