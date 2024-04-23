using System.Threading.Tasks;

namespace Endgame.Game.Items;

public abstract class Potion : Item, IPartyItem
{
	public virtual string Name { get; } = "Potion";

	public async virtual Task Use(object context)
	{
		await Statics.Console.WriteLine("The potion had no effect...");
	}
}
