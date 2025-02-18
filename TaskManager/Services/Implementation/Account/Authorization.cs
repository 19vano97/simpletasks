using System;
using TaskManager.Data;

namespace TaskManager.Services.Implementation;

public static class Authorization
{
    public static bool DoesUserExist(string userName)
    {
        if (new AllowedUsers().UserData.Any(u => u == userName))
        {
            return true;
        }

        return false;
    }
}
