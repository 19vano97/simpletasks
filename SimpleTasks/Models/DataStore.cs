using System;
using SimpleTasks.Delegates;
using SimpleTasks.Events;

namespace SimpleTasks
{
    public class DataStore : IDataStore
    {
        public List<Book> books { get; }
        public List<User> users { get; }
        private string[] _booksString;
        private string[] _usersString;
        private UpdateStringArrayDeletage _updateStringsBooks;
        private UpdateStringArrayDeletage _updateStringsUsers;

        public DataStore()
        {
            books = new List<Book>();
            users = new List<User>();
            _booksString = new string[0];
            _usersString = new string[0];
            var updateStringBooks = new UpdateStringArrayBooksHandler(this);
            var updateStringUsers = new UpdateStringArrayUsersHandler(this);
            updateStringBooks.Subscribe();
            updateStringUsers.Subscribe();
        }

        public string[] BooksToString
        {
            get => _booksString;
        }

        public string[] UsersToString
        {
            get => _usersString;
        }

        public event UpdateStringArrayDeletage UpdateStringsBooks
        {
            add => _updateStringsBooks += value;
            remove => _updateStringsBooks -= value;
        }

        public event UpdateStringArrayDeletage UpdateStringsUsers
        {
            add => _updateStringsUsers += value;
            remove => _updateStringsUsers -= value;
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

        public string[] GetBooksDetailsToString(List<Book> booksDetails)
        {
            string[] booksToString = new string[0];

            foreach (var book in booksDetails)
            {
                Array.Resize(ref booksToString, booksToString.Length + 1);
                booksToString[booksToString.Length - 1] = book.GetDetails();
            }

            return booksToString;
        }

        public string[] GetUsersDetailsToString(List<User> userDetails)
        {
            string[] usersToString = new string[0];

            foreach (var user in userDetails)
            {
                Array.Resize(ref usersToString, usersToString.Length + 1);
                usersToString[usersToString.Length - 1] = string.Format($"{user.Id},{user.Name}\t");
            }

            return usersToString;
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

            OnUpdateStringBooks(this);
            OnUpdateStringUsers(this);

            return true;
        }

        private bool AddingOperation<T>(T item)
        {
            try
            {
                if (typeof(T) == typeof(Book))
                {
                    books.Add(new Book(item as Book));
                    OnUpdateStringBooks(this);
                }
                else if (typeof(T) == typeof(User))
                {
                    users.Add(new User(item as User));
                    OnUpdateStringUsers(this);
                }
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

        protected virtual void OnUpdateStringBooks(object sender)
        {
            _updateStringsBooks.Invoke(this);
        }

        protected virtual void OnUpdateStringUsers(object sender)
        {
            _updateStringsUsers.Invoke(this);
        }

        public void UpdateStringUsers(object sender)
        {
            _usersString = GetUsersDetailsToString(users);
        }

        public void UpdateStringBooks(object sender)
        {
            _booksString = GetBooksDetailsToString(books);
        }

        public List<Book> GetAvailabeBooks()
        {
            return books.Where(b => b.IsAvailable == true).ToList<Book>();
        }
    }
}

