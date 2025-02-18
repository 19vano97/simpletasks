using System;
using TaskManager.Enums;
using TaskManager.Providers;

namespace TaskManager.View;

public class MenuUI
{
    private MenuProvider _menuProvider;

    public MenuUI()
    {
        _menuProvider = new MenuProvider();
    }
    
    public int GetIndexFromList(MenuTree menuStatus)
    {
        int count = 0;
        

        if (menuStatus.HasFlag(MenuTree.MainMenu))
        {
            count = _menuProvider.MainMenu.Count();
        }

        int menuSelect = 0;
        bool isRunning = true;
        do
        {
            Console.Clear();
            Console.CursorVisible = false;

            foreach (var item in collection)
            {
                Console.WriteLine((i == menuSelect ? "* " : "") + menuToString[i] + (i == menuSelect ? "\t<--" : ""));
            }

            var keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != count - 1)
                menuSelect++;
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
                menuSelect--;
            else if (keyPressed.Key == ConsoleKey.Enter)
                isRunning = GetCaseMenu(menuSelect);

        } while (isRunning);
        
        return menuSelect;
    }

    private static bool Exit()
    {
        return false;
    }
}
