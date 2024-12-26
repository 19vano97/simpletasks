using System;
namespace SimpleTasks
{
	public class MainMenu
	{
		private List<(MenuEnumeration, string)> _menu;

		public MainMenu()
		{
			GenerateMenuFromEnum();
        }

		private void GenerateMenuFromEnum()
		{
			_menu = new List<(MenuEnumeration, string)>();
			_menu.Add(new (MenuEnumeration.AddANewBook, "Add a new book"));
            _menu.Add(new (MenuEnumeration.AddANewUser, "Add a new user"));
            _menu.Add(new (MenuEnumeration.ListAllBooks, "List all books"));
            _menu.Add(new (MenuEnumeration.ListAllUsers, "List all  users"));
            _menu.Add(new (MenuEnumeration.BorrowABook, "Borrow a book"));
            _menu.Add(new (MenuEnumeration.ReturnABook, "Return a Book"));
            _menu.Add(new (MenuEnumeration.Exit, "Exit"));
        }

		public List<(MenuEnumeration, string)> Menu
		{
			get => _menu;
		}

		public string[] MenuStrings
		{
			get => GetMenuStrings();
		}

		public string GetCurrentPosition(int i)
		{
			return _menu.Where(m => (int)m.Item1 == i).Select(m => m.Item2).FirstOrDefault();
		}

		private string[] GetMenuStrings()
		{
			string[] menu = new string[_menu.Count()];

			int index = 0;

			foreach (var item in _menu)
			{
				menu[index] = item.Item2;
				index++;
			}

			return menu;
		}
    }
}

