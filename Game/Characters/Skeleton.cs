using Endgame.Game.Actions;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class Skeleton : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "Skeleton";
	public CharacterType Type => CharacterType.Skeleton;
	public string AttackName { get; } = "BONE CRUNCH";
	public PartyType PartyType { get; } = PartyType.Monsters;

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

