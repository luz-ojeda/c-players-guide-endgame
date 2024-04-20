using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

public interface IAction
{
	Task Run(ICharacter character, Battle battle);
}

