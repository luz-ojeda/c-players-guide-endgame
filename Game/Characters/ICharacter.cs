using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public interface ICharacter
{
	public CharacterType Type { get; }
	public Task Act();
}

public enum CharacterType { Skeleton };
