using System;
using SimpleTasks.Delegates;

namespace SimpleTasks;

public class UserInterface
{
    public const int DELAY = 1000;

    public Book AddNewBookUI()
    {
        Console.Clear();
        return new Book(EnterString("title"), EnterString("author"));
    }

    public User AddNewUserUI()
    {
        Console.Clear();
        return new User(EnterString("name"));
    }

    public void ShowAllBooks(List<Book> books)
    {
        Console.Clear();

        foreach (var book in books)
        {
            System.Console.WriteLine(book.GetDetails());
        }

        Thread.Sleep(DELAY);
    }

    public void ShowAllUsers(List<User> users)
    {
        Console.Clear();

        foreach (var user in users)
        {
            System.Console.WriteLine(user.Name);

            foreach (var book in user.BorrowedBooks)
            {
                Console.WriteLine($"\t{book.GetDetails()}");
            }
        }

        Thread.Sleep(DELAY);
    }

    public string EnterString(string message)
    {
        System.Console.WriteLine($"Enter {message}: ");
        return Console.ReadLine();
    }

    public void ShowMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Thread.Sleep(DELAY);
    }
}
