using System;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Enums;
using TaskManager.Handlers;
using TaskManager.Interfaces;
using TaskManager.Providers;
using TaskManager.Services.Logger;
using TaskManager.View;

namespace TaskManager.Controllers;

public class AppController : IDisposable
{
    private ILogger _logger;
    private AccountController _accountController;
    private EventHandler<ShowMessageEventArgs> _showMesssage;
    private MenuProvider _menuProvider;

    public AppController()
    {
        string fileName = DateTime.Now.ToString("yyyy-MM-dd’T’HH:mm:ss");
        _logger = new Logger($"log_{fileName}");
        _accountController = new AccountController();
        var messageDisplayed = new MessageDisplayHandler();
        messageDisplayed.Subscribe(this);
        _menuProvider = new MenuProvider();
    }

    public event EventHandler<ShowMessageEventArgs> MessageDisplayed
    {
        add => _showMesssage += value;
        remove => _showMesssage -= value;
    }

    public async Task Run()
    {
        if (!await AuthorizeUser())
        {
            Exit();
            return;
        }

        OnMessageDisplayed(this, new ShowMessageEventArgs($"Hello {_accountController.Account.Name}!"));

        await GetMenu();
    }

    private async Task<bool> AuthorizeUser()
    {
        return (await _accountController.DoesUserExist(_logger)).DoesAccountExist;
    }

    private async Task GetMenu()
    {
        MenuUI.GetIndexFromList(_menuProvider.GetMenuString(), MenuTree.MainMenu);
    }

    public bool GetCaseMenu(int selectedPoint, MenuTree menuTree)
    {
        if (menuTree.HasFlag(MenuTree.MainMenu))
            return GetCaseMainMenu(selectedPoint);
        else if (menuTree.HasFlag(MenuTree.Import))
            return false;
        else if (menuTree.HasFlag(MenuTree.Export))
            return false;
        else if (menuTree.HasFlag(MenuTree.View))
            return false;

        return false;
    }

    private bool GetCaseMainMenu(int selectedPoint)
    {
        bool isRunning = true;

        switch (selectedPoint)
        {
            case (int)MainMenuEnum.CreateTask:
                //create
                OnMessageDisplayed(this, new ShowMessageEventArgs($"Create!!!!!"));
                break;
            case (int)MainMenuEnum.ModifyTask:
                //modify
                break;
            case (int)MainMenuEnum.DeleteTask:
                //delete
                break;
            case (int)MainMenuEnum.ExportTasks:
                //export
                break;
            case (int)MainMenuEnum.ImportTasks:
                //impory
                break;
            case (int)MainMenuEnum.ViewTasks:
                //viewTasks
                break;
            case (int)MainMenuEnum.ViewLogs:
                //viewLgs
                break;
            case (int)MainMenuEnum.Exit:
                isRunning = Exit();
                break;
        }

        return isRunning;
    }

    protected virtual void OnMessageDisplayed(object? sender, ShowMessageEventArgs e)
    {
        _showMesssage?.Invoke(sender, e);
    }

    private static bool Exit()
    {
        System.Console.WriteLine("Exit...");
        return false;
    }

    public void Dispose()
    {
        _logger.Dispose();
    }
}
