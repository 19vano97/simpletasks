using SimpleTasks;

internal class Program
{
    private static void Main(string[] args)
    {
        IDataStore data = new DataStore();

        UserInterface ui = new UserInterface(data);

        MainMenu menu = new MainMenu("Add a new book\t",
                                     "Add a new user\t",
                                     "List all books\t",
                                     "List all  users\t",
                                     "Borrow a book\t",
                                     "Return a Book\t",
                                     "Exit\t");

        ui.GetMainMenu(menu);
    }
}