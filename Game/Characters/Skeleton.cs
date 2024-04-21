using Endgame.Game.Actions;
using Endgame.Game.Attacks;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class Skeleton : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "Skeleton";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; set; } = 5;
	public float HP { get; set; }
	public IAttack Attack => new BoneCrunchAttack();

	public Skeleton(Battle battle)
	{
		HP = MaxHP;
		Battle = battle;
	}
}

