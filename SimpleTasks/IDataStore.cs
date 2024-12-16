using System;
namespace SimpleTasks
{
	public interface IDataStore
	{
		public List<Book> books { get; set; }
		public List<User> users { get; set; }
	}
}

