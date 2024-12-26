using System;
namespace SimpleTasks
{
	public interface IDataStore
	{
		public List<Book> books { get; }
		public List<User> users { get; }
		public bool AddNewBook(Book book);
		public bool AddNewUser(User user);
		public bool BorrowBook(User user, Book bookToBorrow);
		public bool ReturnBook(User user, Book bookToBeReturned);
		public string[] BooksToString { get; }
		public string[] UsersToString { get; }
		public int GetIdFromArray(string[] data, int position);
    }
}

