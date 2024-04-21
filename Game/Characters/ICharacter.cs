using Endgame.Game.Actions;
using Endgame.Game.Attacks;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public interface ICharacter
{
	public string Name { get; set; }
	public CharacterType Type { get; }
	public Task Act(IAction action);
	public PartyType PartyType { get; }
	public float MaxHP {  get; set; }
	public float HP {  get; set; }
	public IAttack Attack { get; }
}

public enum CharacterType { Skeleton, TrueProgrammer };
