using System;
using TaskManager.Interfaces;
using TaskManager.Models.Account;
using TaskManager.Services.Implementation;
using TaskManager.View;

namespace TaskManager.Controllers;

public class AccountController
{
    private AccountModel _account;

    public AccountController()
    {
    }

    public async Task<AccountModel> DoesUserExist(ILogger logger)
    {
        var userName = GlobalUi.EnterString("user name");

        bool userExist = Authorization.DoesUserExist(userName);

        if (userExist)
            logger.Write($"{userName} is logged in");
        else
            logger.Write($"A user with {userName} nickname tried to login");

        _account = new AccountModel(userExist, userName);

        return _account;
    }

    public AccountModel Account
    {
        get => _account;
    }
}
