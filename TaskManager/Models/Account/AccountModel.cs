using System;

namespace TaskManager.Models.Account;

public class AccountModel
{
    public bool DoesAccountExist { get; set; }
    public string Name { get; set; }

    public AccountModel(bool exist, string name)
    {
        DoesAccountExist = exist;
        Name = name;
    }
}
