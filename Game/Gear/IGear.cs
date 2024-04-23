using Endgame.Game.Characters;
using System.Threading.Tasks;

namespace Endgame.Game.Gear;

public interface IGear
{
	string Name { get; }
	float Effect { get; }
	Task Equip(ICharacter character);
}
