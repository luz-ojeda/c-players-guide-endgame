using System.Threading.Tasks;

namespace Endgame.Game.Items;

public interface IItem
{
	public string Name { get; }
	public Task Use(object context);
}
