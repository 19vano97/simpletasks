using System;
namespace SimpleTasks
{
    [Flags]
	public enum MenuEnumeration : int
	{
		None = 0,
		AddANewBook = 1, 
        AddANewUser = 2, 
        ListAllBooks = 3, 
        ListAllUsers = 4, 
        BorrowABook = 5, 
        ReturnABook = 6 , 
        Exit = 7
	}
}

