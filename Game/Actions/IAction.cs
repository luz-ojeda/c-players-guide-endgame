﻿using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public interface IAction
{
	Task Run(IPartyCharacter character, Battle battle);
}

