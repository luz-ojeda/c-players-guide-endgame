using Endgame.Game.Characters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Endgame.Game;

public class Game
{
    public readonly BlazorConsole Console = new();
    public TrueProgrammer Player { get; set; }
    private Party Heroes { get; set; } = new Party(PartyType.Heroes);
    private Party Monsters { get; set; } = new Party(PartyType.Monsters);
	private List<Battle> Battles { get; set; } = [];
    private bool GameOver { get; set; }

    public Game()
    {
        Statics.Console = Console;
        Statics.ConsoleHelper = new(Console);
    }

    public async Task Run()
    {
        string playerName = await AskForPlayerName();
        Player = new(playerName);

        while (Player.HP > 0 && !GameOver)
        {
            Battle battle = new();
			Player.Battle = battle;

            Heroes.Characters.Add(Player);
            Monsters.Characters.Add(new Skeleton(battle));

            battle.Heroes = Heroes;
            battle.Monsters = Monsters;
			Battles.Add(battle);

            foreach(Battle b in Battles)
            {
                GameOver = await b.Run();
            }
        }
    }

    private async Task<string> AskForPlayerName()
    {
        await Console.Write("Enter your character name: ");
		return await Console.ReadLine();
	}
}
