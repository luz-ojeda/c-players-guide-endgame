using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Actions;

internal interface ITargetedAction : IAction
{
	Task SetTarget(ICharacter character, Party enemyParty, Party party);
}