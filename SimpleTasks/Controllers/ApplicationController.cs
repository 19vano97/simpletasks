using System;
namespace SimpleTasks
{
	public class ApplicationController
	{
		private IDataStore _data = new DataStore();
        private UserInterface _ui = new UserInterface();

		public void Run()
		{

		}

        public Book AddNewBook()
        {
            string title = _ui.EnterString("title");
            string author = _ui.EnterString("author");
            _data.books.Add(new Book(title, author));

            return _data.books.Where(b => b.Title == title && b.Author == author).First();
        }

        public void AddNewUser()
        {
            _data.users.Add(new User(_ui.EnterString("name")));
        }

        public void BorrowBook()
        {
            string[] usersString = GetUsersToString();
            string[] booksString = GetBooksDetailsToString(_data.books);
            int userId = _data.users.Where(u => u.Id == int.Parse(usersString[_ui.GetIndexFromList(usersString)].Split(',')[0]))
                                    .Select(u => u.Id).FirstOrDefault();
            int bookToFind = _data.books.Where(b => b.Id == int.Parse(booksString[_ui.GetIndexFromList(booksString)].Split(',')[0]))
                                        .Select(b => b.Id).FirstOrDefault();

            Book bookToBorrow = _data.books.Where(b => b.Id == bookToFind).FirstOrDefault();

            if (!_data.users.Where(u => u.Id == userId).FirstOrDefault().BorrowBook(ref bookToBorrow))
                Console.WriteLine("The book is not valid");
        }

        public void ReturnBook()
        {
            User user = _data.users.Where(u => u.Id == _ui.GetIndexFromList(GetUsersToString(), _data.users)).FirstOrDefault();
            int bookToFind = _ui.GetIndexFromList(GetBooksDetailsToString(user.BorrowedBooks), user.BorrowedBooks);
            Book bookToReturn = _data.books.Where(b => b.Id == bookToFind).FirstOrDefault();

            _data.users.Where(u => u.Id == user.Id).FirstOrDefault().ReturnBook(ref bookToReturn);
        }

        public string[] GetBooksDetailsToString(List<Book> books)
        {
            string[] booksToString = new string[0];

            foreach (var book in books)
            {
                Array.Resize(ref booksToString, booksToString.Length + 1);
                booksToString[booksToString.Length - 1] = book.GetDetails();
            }

            return booksToString;
        }

        public string[] GetUsersToString()
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
}

