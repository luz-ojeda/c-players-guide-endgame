using Endgame.Game.Interfaces;

namespace Endgame.Game.Attacks;

public record Attack() : IWeightedOption
{
	public virtual double? Weight { get; set; } = 0.15;
}
