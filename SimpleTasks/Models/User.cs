using System;
using System.Numerics;

namespace SimpleTasks;

public class User
{
    private int _id;
    private string _name;
    private List<Book> _borrowedBooks;

    public User(string name)
    {
        _id = new Random().Next(0, int.MaxValue);
        _name = name;
        _borrowedBooks = new List<Book>();
    }

    public User(int id, string name)
    {
        _id = id;
        _name = name;
        _borrowedBooks = new List<Book>();
    }

    public User(int id, string name, List<Book> borrowedBooks)
    {
        _id = id;
        _name = name;
        _borrowedBooks = borrowedBooks;
    }

    public User(User user) : this(user._id, user._name, user._borrowedBooks)
    {
    }

    public int Id
    {
        get => _id;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == null)
            {
                new InvalidOperationException("Name cannot be null");
            }

            _name = value;
        }
    }

    public List<Book> BorrowedBooks
    {
        get => _borrowedBooks;
    }

    public bool BorrowBook(ref Book book)
    {

        if (!book.IsAvailable)
            return false;

        _borrowedBooks.Add(new Book(book.Id, book.Title, book.Author, false));
        book.IsAvailable = false;

        return true;
    }

    public void ReturnBook(ref Book book)
    {
        int bookId = book.Id;
        //_borrowedBooks.RemoveAll(b => b.Id == bookId);
        var bookItem = _borrowedBooks.Where(b => b.Id == bookId).FirstOrDefault();
        _borrowedBooks.Remove(bookItem!);
        book.IsAvailable = true;
    }
}
