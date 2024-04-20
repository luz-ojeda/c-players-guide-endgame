using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public interface ICharacter
{
	public string Name { get; set; }
	public CharacterType Type { get; }
	public Task Act();
}

public enum CharacterType { Skeleton, TrueProgrammer };
