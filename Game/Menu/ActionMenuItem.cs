using Endgame.Game.Interfaces;

namespace Endgame.Game.Menu;

public record ActionMenuItem(string Description, bool IsEnabled, IAction Action) : IMenuItem;
