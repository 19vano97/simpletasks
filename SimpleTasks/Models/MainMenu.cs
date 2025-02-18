using System;
using SimpleTasks.Enumerations;
using SimpleTasks.Resources;

namespace SimpleTasks
{
	public class MainMenu
	{
		private List<(MenuEnumeration, string)> _mainMenu;
        private List<(ImportMenuEnumeration, string)> _importMenu;

        public MainMenu()
		{
			GenerateMainMenu();
			GenerateImportMenu();
        }

		private void GenerateMainMenu()
		{
            _mainMenu = new List<(MenuEnumeration, string)>();
            _mainMenu.Add(new (MenuEnumeration.AddANewBook, Menu.AddNewBook));
            _mainMenu.Add(new (MenuEnumeration.AddANewUser, Menu.AddNewUser));
            _mainMenu.Add(new (MenuEnumeration.ListAllBooks, Menu.ListAllAvailableBooks));
            _mainMenu.Add(new (MenuEnumeration.ListAllUsers, Menu.ListAllUsers));
            _mainMenu.Add(new (MenuEnumeration.BorrowABook, Menu.BorrowBook));
            _mainMenu.Add(new (MenuEnumeration.ReturnABook, Menu.ReturnBook));
            _mainMenu.Add(new(MenuEnumeration.Import, Menu.Import));
            _mainMenu.Add(new (MenuEnumeration.Exit, Menu.Exit));
        }

        private void GenerateImportMenu()
        {
            _importMenu = new List<(ImportMenuEnumeration, string)>();
            _importMenu.Add(new(ImportMenuEnumeration.ImportBooksFromJSON, Menu.ImportBookJson));
            _importMenu.Add(new(ImportMenuEnumeration.ImportBooksFromCSV, Menu.ImportBookCsv));
            _importMenu.Add(new(ImportMenuEnumeration.Back, Menu.Back));
        }

        public List<(MenuEnumeration, string)> MainMenuList
		{
			get => _mainMenu;
		}

		public string[] MainMenuStrings
		{
			get => GetMenuStrings(_mainMenu);
		}

        public string[] ImportMenuStrings
        {
            get => GetMenuStrings(_importMenu);
        }

		private string[] GetMenuStrings<T1, T2>(List<(T1, T2)> list)
		{
			string[] menu = new string[list.Count()];

			int index = 0;

			foreach (var item in list)
			{
				menu[index] = item.Item2 as string;
				index++;
			}

			return menu;
		}
    }
}

