using System;
using SimpleTasks.Delegates;

namespace SimpleTasks;

public class UserInterface
{
    public const int DELAY = 1000;

    public Book AddNewBookUI()
    {
        return new Book(EnterString("title"), EnterString("author"));
    }

    public User AddNewUserUI()
    {
        return new User(EnterString("name"));
    }

    public void ShowAllBooks(List<Book> books)
    {
        foreach (var book in books)
        {
            System.Console.WriteLine(book.GetDetails());
        }
    }

    public void ShowAllUsers(List<User> users)
    {
        foreach (var user in users)
        {
            System.Console.WriteLine(user.Name);

            foreach (var book in user.BorrowedBooks)
            {
                Console.WriteLine($"\t{book.GetDetails()}");
            }
        }
    }

    public string EnterString(string message)
    {
        System.Console.WriteLine($"Enter {message}: ");
        return Console.ReadLine();
    }

    public int EnterInt(string message)
    {
        System.Console.WriteLine($"Enter {message}: ");
        return int.Parse(Console.ReadLine());
    }

    public void ShowMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Thread.Sleep(DELAY);
    }
}
