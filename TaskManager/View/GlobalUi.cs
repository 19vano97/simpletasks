using System;

namespace TaskManager.View;

public static class GlobalUi
{
    public static string EnterString(string message)
    {
        System.Console.Write($"Enter {message}: ");
        var str = Console.ReadLine();

        return str == null ? EnterString(message) : str;
    }

    public static void DisplayMessage(string message)
    {
        Console.Clear();

        System.Console.WriteLine(message);
    }
}
