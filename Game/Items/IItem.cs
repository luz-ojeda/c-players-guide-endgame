using System.Threading.Tasks;

namespace Endgame.Game.Items;

public interface IItem
{
	string Name { get; }
	Task Use(object context);
}
