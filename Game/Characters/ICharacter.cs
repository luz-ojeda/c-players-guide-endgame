using Endgame.Game.Attacks;

namespace Endgame.Game.Characters;

public interface ICharacter
{
	public string Name { get; set; }
	public PartyType PartyType { get; }
	public float MaxHP {  get; }
	public float HP {  get; set; }
	public IAttack Attack { get; }
	public string Symbol{ get; }
	public bool CanUseItems { get; set; }
}

public enum CharacterType { Skeleton, TrueProgrammer };
