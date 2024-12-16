using System;
namespace SimpleTasks
{
	public class MainMenu
	{
		private string[] _menu;


		public MainMenu(params string[] menu)
		{
			_menu = (string[])menu.Clone();
		}

		public string[] Menu
		{
			get => _menu;
		}
	}
}

