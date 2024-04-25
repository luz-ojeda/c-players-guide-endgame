using Endgame.Game.Interfaces;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class DoNothingAction : IAction
{
	public string Description { get; } = "Do nothing";
	public double? Weight { get; set; } = null;

	public async Task Run(IPartyCharacter character, Battle? battle)
	{
		await Statics.Console.WriteLine($"{character.Name} did NOTHING.");
	}
}
