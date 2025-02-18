using TaskManager.Controllers;

internal class Program
{
    private static void Main(string[] args)
    {
        var test = new AppController();
        test.Run();
    }
}