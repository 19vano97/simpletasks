using System;
namespace SimpleTasks
{
	public class MainMenu
	{
		private string[] _menu = new string[] {"Add a new book\t",
                                     "Add a new user\t",
                                     "List all books\t",
                                     "List all  users\t",
                                     "Borrow a book\t",
                                     "Return a Book\t",
                                     "Exit\t"};

		public string[] Menu
		{
			get => _menu;
		}
	}
}

