using System;
using SimpleTasks.Delegates;

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
		public event UpdateStringArrayDeletage UpdateStringsBooks;
		public event UpdateStringArrayDeletage UpdateStringsUsers;
		public void UpdateStringUsers(object sender);
		public void UpdateStringBooks(object sender);
		public string[] GetBooksDetailsToString(List<Book> booksDetails);
		public string[] GetUsersDetailsToString(List<User> userDetails);
		public List<Book> GetAvailabeBooks();
    }
}

