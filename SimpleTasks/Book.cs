using System;

namespace SimpleTasks;

public class Book
{
    private int _id;
    private string _title;
    private string _author;
    private bool _isAvailable;

    public Book(string title, string author)
    {
        _id = new Random().Next(0, int.MaxValue);
        _title = title;
        _author = author;
        _isAvailable = true;
    }

    public Book(string title, string author, bool isAvailable)
    {
        _id = new Random().Next(0, int.MaxValue);
        _title = title;
        _author = author;
        _isAvailable = isAvailable;
    }

    public Book(int id,string title, string author, bool isAvailable)
    {
        _id = id;
        _title = title;
        _author = author;
        _isAvailable = isAvailable;
    }

    public Book(Book book)
    { 
        _id = book._id;
        _title = book._title;
        _author = book._author;
        _isAvailable = book._isAvailable;
    }

    public int Id
    {
        get => _id;
    }

    public string Title
    {
        get => _title;
        set => _title = value == null ? "No title" : value;
    }

    public string Author
    {
        get => _author;
        set => _author = value == null ? "No title" : value;
    }

    public bool IsAvailable
    {
        get => _isAvailable;
        set => _isAvailable = value;
    }

    public string GetDetails()
    {
        return string.Format($"{_id}, {_title}, {_author}, {_isAvailable}");
    }
}
