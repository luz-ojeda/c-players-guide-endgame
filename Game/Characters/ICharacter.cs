using Endgame.Game.Actions;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public interface ICharacter
{
	public string Name { get; set; }
	public CharacterType Type { get; }
	public string AttackName { get; }
	public Task Act(IAction action);
	public PartyType PartyType { get; }
}

public enum CharacterType { Skeleton, TrueProgrammer };
