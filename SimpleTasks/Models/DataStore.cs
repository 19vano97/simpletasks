using System;

namespace SimpleTasks
{
    public class DataStore : IDataStore
    {
        public List<Book> books { get; }
        public List<User> users { get; }

        public DataStore()
        {
            books = new List<Book>();
            users = new List<User>();
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

        public string[] GetBooksDetailsToString()
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

            foreach (var user in users)
            {
                Array.Resize(ref booksToString, booksToString.Length + 1);
                booksToString[booksToString.Length - 1] = string.Format($"{user.Id},{user.Name}\t");
            }

            return booksToString;
        }

        public Book CheckBookInLib(Book book)
        {
            return books.Where(b => b.Id == book.Id && b.Title == book.Title && b.Author == book.Author).FirstOrDefault();
        }

        public User CheckUserInList(User user)
        {
            return users.Where(u => u.Id == user.Id && u.Name == user.Name).FirstOrDefault();
        }

        private bool PerformBookOperation(User user, Book book, Action<User, Book> bookOperation)
        {
            var bookToBeChekedInList = CheckBookInLib(book);
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
    }
}

