using System;

namespace Endgame.Game.Interfaces;

public interface ICharacterCore
{
    Battle Battle { get; set; }

    // This method will raise the CharacterDied event
    void Die();
    event Action<ICharacterCore>? CharacterDied;
}
