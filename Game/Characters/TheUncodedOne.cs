using Endgame.Game.Actions;
using Endgame.Game.Attacks;
using System.Threading.Tasks;

namespace Endgame.Game.Characters;

public class TheUncodedOne : ICharacter
{
	public Battle Battle { get; set; }
	public string Name { get; set; } = "The Uncoded One";
	public PartyType PartyType { get; } = PartyType.Monsters;
	public float MaxHP { get; set; } = 15;
	public float HP { get; set; }
	public IAttack Attack => new PunchAttack();

	public TheUncodedOne(Battle battle)
	{
		HP = MaxHP;
		Battle = battle;
	}

	public async Task Act(IAction action)
	{
		await Task.Delay(1000);
		await action.Run(this, Battle);
	}
}
