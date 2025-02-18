using System;

namespace TaskManager.Data;

public class AllowedUsers
{
    private List<string> _users;

    public AllowedUsers()
    {
        _users = new List<string>()
        {
            {"van"},
            {"user"},
            {"ttt"}
        };
    }

    public List<string> UserData
    {
        get => _users;
    }
}
