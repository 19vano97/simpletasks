using System;
namespace SimpleTasks.EventHandlers
{
	public class ShowMessageEventArgs
	{
		private string _message;

		public ShowMessageEventArgs(string message)
		{
			_message = message;
		}

		public string Message
		{
			get => _message;
		}
	}
}

