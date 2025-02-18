using System;
using TaskManager.Data;
using TaskManager.Enums;
using TaskManager.Models.Menu;

namespace TaskManager.Providers;

public class MenuProvider
{
    private List<MenuModel> _mainMenu;
    private string[] _menuString;

    public MenuProvider()
    {
        _mainMenu = new Menu().MainMenu;
    }

    public List<MenuModel> MainMenu
    {
        get => _mainMenu;
    }

    public string[] GetMenuString()
    {
        if (_menuString == null)
        {
            return GetMenuStrings();
        }

        return _menuString;
    }

    private string[] GetMenuStrings()
    {
        _menuString = new string[0];

        int index = 0;

        foreach (var item in _mainMenu)
        {
            _menuString[index] = item.MainMenu.Item2;
            index++;
        }

        return _menuString;
    }
}
