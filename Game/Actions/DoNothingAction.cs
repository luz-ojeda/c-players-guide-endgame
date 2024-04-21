﻿using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public class DoNothingAction : IAction
{
	public async Task Run(ICharacter character, Battle? battle)
	{
		await Statics.Console.WriteLine($"{character.Name} did NOTHING.");
	}
}