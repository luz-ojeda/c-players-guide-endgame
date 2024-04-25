using System;
using Endgame.Game.Interfaces;

namespace Endgame.Game.Characters;

public class Character : ICharacterCore
{
	public float HP { get; set; }
	public Battle Battle { get; set; }
	public event Action<ICharacterCore>? CharacterDied;

	public void Die()
	{
		CharacterDied?.Invoke(this);
	}
}
