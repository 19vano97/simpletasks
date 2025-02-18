using System;
using SimpleTasks.Controllers;
using SimpleTasks.Delegates;
using SimpleTasks.Enumerations;
using SimpleTasks.EventHandlers;
using SimpleTasks.Handlers;
using static System.Reflection.Metadata.BlobBuilder;

namespace SimpleTasks
{
	public class Application
	{
		private IDataStore _data = new DataStore();
        private UserInterface _ui = new UserInterface();
        private MainMenu _mainMenu = new MainMenu();
        private EventHandler<ShowMessageEventArgs>? _messageDisplayed;

        public event EventHandler<ShowMessageEventArgs> MessageDisplayed
        {
            add => _messageDisplayed += value;
            remove => _messageDisplayed -= value;
        }

        public void Run()
		{
            var messageHandler = new MessageHandler(_ui);
            messageHandler.Subscribe(this);
            GetIndexFromList(_mainMenu.MainMenuStrings, MenuStatusEnumeration.MainMenu);
            messageHandler.Unubscribe(this);
		}

        public bool GetCaseMenu(int menuSelect, MenuStatusEnumeration menuStatus)
		{
            if (menuStatus.HasFlag(MenuStatusEnumeration.MainMenu))
                return GetCaseMainMenu(menuSelect);
            else if (menuStatus.HasFlag(MenuStatusEnumeration.Import))
                return GetCaseImportMenu(menuSelect);

            return false;
        }

        private bool GetCaseMainMenu(int menuSelect)
        {
            bool isRunning = true;

            switch (menuSelect)
            {
                case (int)MenuEnumeration.AddANewBook:
                    AddNewBook();
                    break;
                case (int)MenuEnumeration.AddANewUser:
                    AddNewUser();
                    break;
                case (int)MenuEnumeration.ListAllBooks:
                    ShowAllAvailableBooks(); ;
                    break;
                case (int)MenuEnumeration.ListAllUsers:
                    ShowAllUsers();
                    break;
                case (int)MenuEnumeration.BorrowABook:
                    BorrowBook();
                    break;
                case (int)MenuEnumeration.ReturnABook:
                    ReturnBook();
                    break;
                case (int)MenuEnumeration.Import:
                    ImportMenuCall();
                    break;
                case (int)MenuEnumeration.Exit:
                    isRunning = Exit();
                    break;
            }

            return isRunning;
        }

        private bool GetCaseImportMenu(int menuSelect)
        {
            bool isRunning = true;

            switch (menuSelect)
            {
                case (int)ImportMenuEnumeration.ImportBooksFromJSON:
                    ImportBooksJson();
                    break;

                case (int)ImportMenuEnumeration.ImportBooksFromCSV:

                    break;

                case (int)ImportMenuEnumeration.Back:
                    isRunning = Back();
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

        private User GetUserFromList(List<User> users)
        {
            string[] usersInfo = _data.GetUsersDetailsToString(users);
            int userPosition = GetIndexFromList(usersInfo, MenuStatusEnumeration.SelectWithClosing);

            return users.Where(u => u.Id == _data.GetIdFromArray(usersInfo, userPosition)).FirstOrDefault();
        }

        private Book GetBookFromList(List<Book> books)
        {
            string[] booksInfo = _data.GetBooksDetailsToString(books);
            int bookPostion = GetIndexFromList(booksInfo, MenuStatusEnumeration.SelectWithClosing);

            return books.Where(b => b.Id == _data.GetIdFromArray(booksInfo, bookPostion)).FirstOrDefault();
        }

        protected virtual void OnMessageDisplayed(object? sender, ShowMessageEventArgs e)
        {
            _messageDisplayed?.Invoke(sender, e);
        }

        #region SharedMenuSwitchFunctions

        private bool Back()
        {
            return false;
        }

        #endregion

        #region MainMenuSwitchFunctions

        private void AddNewBook()
        {
            var bookToAdd = _ui.AddNewBookUI();
            if (_data.AddNewBook(bookToAdd))
                OnMessageDisplayed(this, new ShowMessageEventArgs("The book has been added"));
        }

        private void AddNewUser()
        {
            var userToAdd = _ui.AddNewUserUI();
            if (_data.AddNewUser(userToAdd))
                OnMessageDisplayed(this, new ShowMessageEventArgs("The book has been added"));
        }

        private void ShowAllAvailableBooks()
        {
            _ui.ShowAllBooks(_data.GetAvailabeBooks());
        }

        private void ShowAllUsers()
        {
            _ui.ShowAllUsers(_data.users);
        }

        private void BorrowBook()
        {
            var user = GetUserFromList(_data.users);
            var book = GetBookFromList(_data.GetAvailabeBooks());

            if (_data.BorrowBook(user, book))
                OnMessageDisplayed(this, new ShowMessageEventArgs("The book has been borrowed"));
        }

        private void ReturnBook()
        {
            var user = GetUserFromList(_data.users);
            var book = GetBookFromList(user.BorrowedBooks);

            if (_data.ReturnBook(user, book))
                OnMessageDisplayed(this, new ShowMessageEventArgs("The book has been returned"));
        }

        private void ImportMenuCall()
        {
            GetIndexFromList(_mainMenu.ImportMenuStrings, MenuStatusEnumeration.Import);
        }

        private bool Exit()
        {
            return false;
        }

        #endregion

        #region ImportMenuSwitchFunctions

        private void ImportBooksJson()
        {
            //string fileName = _ui.EnterString("file name");
            var importBooks = new ImportBookJson().Import("../../../books.json");

            foreach (var item in importBooks)
            {
                _data.AddNewBook(item);
            }

            OnMessageDisplayed(this, new ShowMessageEventArgs("The books has been imported to the library"));
        }

        #endregion
    }
}

