using Endgame.Game.Actions;
using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game;
public class Battle
{
	public Party Heroes { get; set; }
	public Party Monsters { get; set; }
	public PartyType Turn { get; set; }
	public bool BattleOver { get; set; } = false;

	public async Task Run()
	{
		while (!BattleOver)
		{
			await PlayTurn(Heroes);
			await PlayTurn(Monsters);
		}
	}

	private async Task PlayTurn(Party party)
	{
		await Statics.Console.WriteLine();
		
		foreach (ICharacter character in party.Characters)
		{
			await Statics.Console.WriteLine($"It's {character.Name} turn...");

			IAction action;
			if (party.PlayerInControl == PlayerType.Human)
			{
				action = await PromptForAction(character);
			}
			else
			{
				action = new DoNothingAction();
			}

			await character.Act(action);
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
			"do nothing" or _ => new DoNothingAction(),
		};
	}

	private async Task<IAction> PromptForTarget(ICharacter c)
	{
		await Statics.Console.WriteLine("The possible targets are:");
		foreach (ICharacter enemyCharacter in GetEnemyPartyFor(c).Characters)
		{
			await Statics.Console.WriteLine(enemyCharacter.Name);
		}

		ICharacter target = GetEnemyPartyFor(c).Characters[0];
		return new AttackAction(target);
	}

	private Party GetPartyFor(ICharacter c) => c.PartyType == PartyType.Heroes ? Heroes : Monsters;

	private Party GetEnemyPartyFor(ICharacter c) => c.PartyType == PartyType.Monsters ? Heroes : Monsters;
}
