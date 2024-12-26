using System;
using SimpleTasks.Delegates;

namespace SimpleTasks
{
    public class DataStore : IDataStore
    {
        public List<Book> books { get; }
        public List<User> users { get; }
        private string[] _booksString;
        private string[] _usersString;
        private event UpdateStringArrayDeletage _updateStringsBooks;
        private event UpdateStringArrayDeletage _updateStringsUsers;

        public DataStore()
        {
            books = new List<Book>();
            users = new List<User>();
            _booksString = new string[0];
            _usersString = new string[0];
            _updateStringsBooks += DataStore_updateStringsBooks;
            _updateStringsUsers += DataStore_updateStringsUsers;
        }

        public string[] BooksToString
        {
            get => _booksString;
        }

        public string[] UsersToString
        {
            get => _usersString;
        }

        public bool AddNewBook(Book book)
        {
            return AddingOperation(book);
        }

        public bool AddNewUser(User user)
        {
            return AddingOperation(user);
        }

        public bool BorrowBook(User user, Book bookToBorrow)
        {
            return PerformBookOperation(user, bookToBorrow,
                (user, book) => users.Where(u => u.Id == user.Id).FirstOrDefault().BorrowBook(ref book));
        }

        public bool ReturnBook(User user, Book bookToBeReturned)
        {
            return PerformBookOperation(user, bookToBeReturned,
                (user, book) => users.Where(u => u.Id == user.Id).FirstOrDefault().ReturnBook(ref book));
        }

        private string[] GetBooksDetailsToString()
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

            foreach (var user in users)
            {
                Array.Resize(ref booksToString, booksToString.Length + 1);
                booksToString[booksToString.Length - 1] = string.Format($"{user.Id},{user.Name}\t");
            }

            return booksToString;
        }

        public Book CheckBookInLibrary(Book book)
        {
            return books.Where(b => b.Id == book.Id && b.Title == book.Title && b.Author == book.Author).FirstOrDefault();
        }

        public User CheckUserInList(User user)
        {
            return users.Where(u => u.Id == user.Id && u.Name == user.Name).FirstOrDefault();
        }

        private bool PerformBookOperation(User user, Book book, Action<User, Book> bookOperation)
        {
            var bookToBeChekedInList = CheckBookInLibrary(book);
            var userToBeCheckedInList = CheckUserInList(user);

            if (bookToBeChekedInList == null || userToBeCheckedInList == null)
                return false;

            try
            {
                bookOperation(userToBeCheckedInList, bookToBeChekedInList);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            UpdateBooksStringsEvent(this);

            return true;
        }

        private bool AddingOperation<T>(T item)
        {
            try
            {
                if (typeof(T) == typeof(Book))
                    books.Add(new Book(item as Book));
                else if (typeof(T) == typeof(User))
                    users.Add(new User(item as User));

                UpdateBooksStringsEvent(this);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public int GetIdFromArray(string[] data, int position)
        {
            return int.Parse(data[position].Split(',')[0]);
        }

        protected virtual void UpdateBooksStringsEvent(object sender)
        {
            _updateStringsBooks.Invoke(this);
            _updateStringsUsers.Invoke(this);
        }

        private void DataStore_updateStringsUsers(object sender)
        {
            _usersString = GetUsersToString();
        }

        private void DataStore_updateStringsBooks(object sender)
        {
            _booksString = GetBooksDetailsToString();
        }
    }
}

