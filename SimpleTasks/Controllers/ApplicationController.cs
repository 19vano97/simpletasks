using System;
using SimpleTasks.Delegates;
using SimpleTasks.Enumerations;
using SimpleTasks.EventHandlers;

namespace SimpleTasks
{
	public class ApplicationController
	{
		private IDataStore _data = new DataStore();
        private UserInterface _ui = new UserInterface();
        private MainMenu _mainMenu = new MainMenu();
        public event EventHandler<ShowMessageEventArgs> message;

        public void Run()
		{
            message += ApplicationController_Message;;
            GetIndexFromList(_mainMenu.MenuStrings, MenuStatusEnumeration.MainMenu);
		}

        private void ApplicationController_Message(object? sender, ShowMessageEventArgs e)
        {
            _ui.ShowMessage(e.Message);
        }

        public bool GetCaseMenu(int menuSelect, MenuStatusEnumeration menuStatus)
		{
            if (menuStatus.HasFlag(MenuStatusEnumeration.MainMenu))
                return GetCaseMainMenu(menuSelect);

            return false;
        }

        private bool GetCaseMainMenu(int menuSelect)
        {
            bool isRunning = true;

            switch (menuSelect)
            {
                case (int)MenuEnumeration.AddANewBook:
                    var bookToAdd = _ui.AddNewBookUI();
                    if (_data.AddNewBook(bookToAdd))
                        SendMessage(this, new ShowMessageEventArgs("The book has been added"));
                    break;
                case (int)MenuEnumeration.AddANewUser:
                    var userToAdd = _ui.AddNewUserUI();
                    if (_data.AddNewUser(userToAdd))
                        SendMessage(this, new ShowMessageEventArgs("The book has been added"));
                    break;
                case (int)MenuEnumeration.ListAllBooks:
                    _ui.ShowAllBooks(_data.books);
                    break;
                case (int)MenuEnumeration.ListAllUsers:
                    _ui.ShowAllUsers(_data.users);
                    break;
                case (int)MenuEnumeration.BorrowABook:
                    if (_data.BorrowBook(GetUserFromList(), GetBookFromList()))
                        SendMessage(this, new ShowMessageEventArgs("The book has been borrowed"));
                    break;
                case (int)MenuEnumeration.ReturnABook:
                    if (_data.ReturnBook(GetUserFromList(), GetBookFromList()))
                        SendMessage(this, new ShowMessageEventArgs("The book has been returned"));
                    break;
                case (int)MenuEnumeration.Exit: // exit
                    isRunning = false;
                    break;
            }

            return isRunning;
        }

        public int GetIndexFromList(string[] menuToString, MenuStatusEnumeration menuStatus)
        {
            int menuSelect = 0;

            bool isRunning = true;

            do
            {
                Console.Clear();
                Console.CursorVisible = false;

                for (int i = 0; i < menuToString.Length; i++)
                {
                    Console.WriteLine((i == menuSelect ? "* " : "") + menuToString[i] + (i == menuSelect ? "\t<--" : ""));
                }

                var keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menuToString.Length - 1)
                    menuSelect++;
                else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
                    menuSelect--;
                else if (keyPressed.Key == ConsoleKey.Enter)
                    isRunning = GetCaseMenu(menuSelect, menuStatus);
            } while (isRunning);

            return menuSelect;
        }

        private User GetUserFromList()
        {
            string[] usersData = _data.GetUsersToString();
            int userPosition = GetIndexFromList(usersData, MenuStatusEnumeration.SelectWithClosing);

            return _data.users.Where(u => u.Id == _data.GetIdFromArray(usersData, userPosition)).FirstOrDefault();
        }

        private Book GetBookFromList()
        {
            string[] booksData = _data.GetBooksDetailsToString();
            int bookPostion = GetIndexFromList(booksData, MenuStatusEnumeration.SelectWithClosing);

            return _data.books.Where(b => b.Id == _data.GetIdFromArray(booksData, bookPostion)).FirstOrDefault();
        }

        protected virtual void SendMessage(object? sender, ShowMessageEventArgs e)
        {
            message?.Invoke(sender, e);
        }
    }
}

