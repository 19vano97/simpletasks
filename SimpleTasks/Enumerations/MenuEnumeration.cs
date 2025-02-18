using System;
namespace SimpleTasks
{
    [Flags]
	public enum MenuEnumeration : int
	{
		AddANewBook = 0, 
        AddANewUser = 1, 
        ListAllBooks = 2, 
        ListAllUsers = 3, 
        BorrowABook = 4, 
        ReturnABook = 5,
        Import = 6,
        Exit = 7
	}
}

