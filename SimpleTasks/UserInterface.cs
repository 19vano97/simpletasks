using System;
namespace SimpleTasks;

public class UserInterface
{
    public const int DELAY = 500;

    private IDataStore _data;

    public UserInterface(IDataStore data)
    {
        _data = data;
    }

    public void GetMainMenu(MainMenu menu)
    {
        int menuSelect = 0;
        bool isRunning = true;

        do
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Hello and welcome! Please choose type of library actions:");

            for (int i = 0; i < menu.Menu.Length; i++)
            {
                Console.WriteLine((i == menuSelect ? "* " : "") + menu.Menu[i] + (i == menuSelect ? "<--" : ""));
            }

            var keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menu.Menu.Length - 1)
            {
                menuSelect++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
            {
                menuSelect--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                switch (menuSelect)
                {
                    case 0: //add new book
                        Console.Clear();
                        AddNewBook();
                        System.Console.WriteLine("New book has been added");
                        Thread.Sleep(DELAY);
                        break;
                    case 1: //add new user
                        Console.Clear();
                        AddNewUser();
                        System.Console.WriteLine("New user has been added");
                        Thread.Sleep(DELAY);
                        break;
                    case 2: //show all books
                        Console.Clear();
                        ShowAllBooks();
                        Thread.Sleep(DELAY);
                        break;
                    case 3: //show all users
                        Console.Clear();
                        ShowAllUsers();
                        Thread.Sleep(DELAY);
                        break;
                    case 4: //borrow a book
                        BorrowBook();
                        break;
                    case 5: //return a book
                        ReturnBook();
                        break;
                    case 6: // exit
                        isRunning = false;
                        break;
                }
            }
        } while (isRunning);
    }

    private void AddNewBook()
    {
        _data.books.Add(new Book(EnterString("title"), EnterString("author")));
    }

    private void AddNewUser()
    {
        _data.users.Add(new User(EnterString("name")));
    }

    private void ShowAllBooks()
    {
        foreach (var book in _data.books)
        {
            System.Console.WriteLine(book.GetDetails());
        }
    }

    private void ShowAllUsers()
    {
        foreach (var user in _data.users)
        {
            System.Console.WriteLine(user.Name);

            foreach (var book in user.BorrowedBooks)
            {
                Console.WriteLine($"\t{book.GetDetails()}");
            }
        }
    }

    private void BorrowBook()
    {
        int userId = GetIdFromList(GetUsersToString(), _data.users);
        int bookToFind = GetIdFromList(GetBooksDetailsToString(_data.books), _data.books);
        
        Book bookToBorrow = _data.books.Where(b => b.Id == bookToFind).FirstOrDefault();

        if (!_data.users.Where(u => u.Id == userId).FirstOrDefault().BorrowBook(ref bookToBorrow))
            Console.WriteLine("The book is not valid");
    }

    private void ReturnBook()
    {
        User user = _data.users.Where(u => u.Id == GetIdFromList(GetUsersToString(), _data.users)).FirstOrDefault();
        int bookToFind = GetIdFromList(GetBooksDetailsToString(user.BorrowedBooks), user.BorrowedBooks);
        Book bookToReturn = _data.books.Where(b => b.Id == bookToFind).FirstOrDefault();

        _data.users.Where(u => u.Id == user.Id).FirstOrDefault().ReturnBook(ref bookToReturn);
    }

    private int GetIdFromList<T>(string[] booksToString, List<T> lists)
    {
        int menuSelect = 0;
        bool isRunning = true;
        int id = -1;

        do
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Please choose a book:");

            for (int i = 0; i < booksToString.Length; i++)
            {
                Console.WriteLine((i == menuSelect ? "* " : "") + booksToString[i] + (i == menuSelect ? "<--" : ""));
            }

            var keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != booksToString.Length - 1)
            {
                menuSelect++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
            {
                menuSelect--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                switch (menuSelect)
                {
                    case var value when value == menuSelect:
                        if (typeof(T) == typeof(Book))
                        {
                            id = (lists as List<Book>).Where(b => b.Id == int.Parse(booksToString[menuSelect].Split(',')[0]))
                                              .Select(b => b.Id).FirstOrDefault();
                        }
                        else if (typeof(T) == typeof(User))
                        {
                            id = (lists as List<User>).Where(u => u.Id == int.Parse(booksToString[menuSelect].Split(',')[0]))
                                              .Select(u => u.Id).FirstOrDefault();
                        }
                        isRunning = false;
                        break;
                }
            }
        } while (isRunning);

        return id;
    }

    private string EnterString(string message)
    {
        System.Console.WriteLine($"Enter {message}: ");
        return Console.ReadLine();
    }

    private int EnterInt(string message)
    {
        System.Console.WriteLine($"Enter {message}: ");
        return int.Parse(Console.ReadLine());
    }

    private string[] GetBooksDetailsToString(List<Book> books)
    {
        string[] booksToString = new string[0];

        foreach (var book in books)
        {
            Array.Resize(ref booksToString, booksToString.Length + 1);
            booksToString[booksToString.Length - 1] = book.GetDetails();
        }

        return booksToString;
    }

    private string[] GetUsersToString()
    {
        string[] booksToString = new string[0];

        foreach (var user in _data.users)
        {
            Array.Resize(ref booksToString, booksToString.Length + 1);
            booksToString[booksToString.Length - 1] = string.Format($"{user.Id},{user.Name}\t");
        }

        return booksToString;
    }
}