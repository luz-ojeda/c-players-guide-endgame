using Endgame.Game.Attacks;
using Game.Enums;

namespace Endgame.Game.Characters;

public interface ICharacter
{
	string Name { get; set; }
	PartyType PartyType { get; }
	float MaxHP { get; }
	float HP { get; set; }
	IAttack Attack { get; }
	string Symbol { get; }
	bool CanUseItems { get; set; }
	string CurrentHP => $"({HP}/{MaxHP})";
}
