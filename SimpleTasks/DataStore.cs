using System;

namespace SimpleTasks
{
    public class DataStore : IDataStore
    {
        public List<Book> books { get; set; }
        public List<User> users { get; set; }

        public DataStore()
        {
            books = new List<Book>();
            users = new List<User>();
        }
    }
}

