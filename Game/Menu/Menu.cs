using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Endgame.Game.Menu;

public class Menu
{
    public static async Task DisplayMenuItems(string prompt, List<IMenuItem> menuItems)
    {
		await Statics.Console.WriteLine(prompt);
        foreach (var (i, item) in menuItems.Select((item, i) => (i, item)))
        {
            await Statics.Console.WriteLine($"{i} - {item.Description}");
        }
    }

    public static async Task<int> GetUserOption(int menuItemsCount)
    {
        int actionIndex = -1;
        while (actionIndex < 0)
        {
            ConsoleKeyInfo pick = await Statics.Console.ReadKey(true);

            if (int.TryParse(pick.KeyChar.ToString(), out int index) && index >= 0 && index < menuItemsCount)
            {
                actionIndex = index;
            }
            else
            {
                await Statics.Console.WriteLine("Please enter a valid number from the menu options.");
            }
        }

        await Statics.Console.WriteLine();

        return actionIndex;
    }
}
