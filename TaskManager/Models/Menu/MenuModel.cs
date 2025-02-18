using System;
using TaskManager.Enums;

namespace TaskManager.Models.Menu;

public class MenuModel
{
    public (MainMenuEnum, string) MainMenu { get; set; }

    public MenuModel(MainMenuEnum mainMenuEnum, string menuString)
    {
        MainMenu = new (mainMenuEnum, menuString);
    }
}
