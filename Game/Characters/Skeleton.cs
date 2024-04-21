using Endgame.Game.Actions;
using Endgame.Game.Attacks;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class Skeleton : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "Skeleton";
	public CharacterType Type => CharacterType.Skeleton;
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; set; } = 5;
	public float HP { get; set; } = 5;
	public IAttack Attack => new BoneCrunchAttack();

	public Skeleton(Battle battle)
	{
		Battle = battle;
	}

	public async Task Act(IAction action)
	{
		await Task.Delay(1000);
		await action.Run(this, Battle);
	}
}

