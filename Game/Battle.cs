using Endgame.Game.Actions;
using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game;
public class Battle
{
	public Party HeroesParty { get; set; }
	public Party MonstersParty { get; set; }
	public PartyType Turn { get; set; }
	public bool BattleOver { get; set; } = false;

	public async Task Run()
	{
		while (!BattleOver)
		{
			await PlayTurn(HeroesParty);
			await PlayTurn(MonstersParty);
		}
	}

	private async Task PlayTurn(Party party)
	{
		await Statics.Console.WriteLine();
		
		foreach (ICharacter c in party.Characters)
		{
			await Statics.Console.WriteLine($"It's {c.Name} turn...");

			IAction action;
			if (party.PlayerInControl == PlayerType.Human)
			{
				action = await PromptForAction(c);
			}
			else
			{
				action = new DoNothing();
			}

			await c.Act(action);
		}
	}

	private async Task<IAction> PromptForAction(ICharacter c)
	{
		await Statics.Console.WriteLine("Choose action: ");
		await Task.Delay(500);
		//string pick = await Statics.Console.ReadLine();
		string pick = "attack";

		return pick.ToLower() switch
		{
			"attack" => await PromptForTarget(c),
			"do nothing" or _ => new DoNothing(),
		};
	}

	private async Task<IAction> PromptForTarget(ICharacter c)
	{
		await Statics.Console.WriteLine("The possible targets are:");
		foreach (ICharacter enemyCharacter in GetEnemyPartyFor(c).Characters)
		{
			await Statics.Console.WriteLine(enemyCharacter.Name);
		}

		ICharacter character = GetEnemyPartyFor(c).Characters[0];
		return new Attack(character);
	}

	private Party GetPartyFor(ICharacter c)
	{
		if (c.PartyType == PartyType.Heroes) return HeroesParty;
		return MonstersParty;
	}

	private Party GetEnemyPartyFor(ICharacter c)
	{
		if (c.PartyType == PartyType.Monsters) return HeroesParty;
		return MonstersParty;
	}
}
