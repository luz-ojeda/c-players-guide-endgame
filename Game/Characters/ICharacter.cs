using Endgame.Game.Actions;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public interface ICharacter
{
	public string Name { get; set; }
	public CharacterType Type { get; }
	public Task Act(IAction action);
}

public enum CharacterType { Skeleton, TrueProgrammer };
