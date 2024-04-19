using System.Threading.Tasks;
using Website;

namespace Endgame;

public class Game
{
    public readonly BlazorConsole Console = new();

    public async Task Run()
    {
        do
        {
            await Console.WriteLine("Name:");
            string name = await Console.ReadLine();
            Console.WriteLine(name);
        } while (true);
    }
}
