using System;
using TaskManager.Enums;
using TaskManager.Models.Menu;

namespace TaskManager.Data;

public class Menu
{
    private List<MenuModel> _mainMenu;
    private string[] _mainMenuString;

    public Menu()
    {
        _mainMenu = new List<MenuModel>()
        {
            {new MenuModel (MainMenuEnum.CreateTask, "Create task")},
            {new MenuModel (MainMenuEnum.ModifyTask, "Modify task")},
            {new MenuModel (MainMenuEnum.DeleteTask, "Delete task")},
            {new MenuModel (MainMenuEnum.ExportTasks, "Export tasks")},
            {new MenuModel (MainMenuEnum.ImportTasks, "Import tasks")},
            {new MenuModel (MainMenuEnum.ViewTasks, "View all tasks")},
            {new MenuModel (MainMenuEnum.ViewLogs, "View logs")},
            {new MenuModel (MainMenuEnum.Exit, "Exit")},
        };
    }

    public List<MenuModel> MainMenu
    {
        get => _mainMenu;
    }
}
