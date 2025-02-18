using System;
using SimpleTasks.EventHandlers;

namespace SimpleTasks.Handlers
{
	public class MessageHandler
	{
		private UserInterface _ui;

		public MessageHandler(UserInterface ui)
		{
			_ui = ui;
		}

		public void Subscribe(Application controller)
		{
			controller.MessageDisplayed += HandleMessageDisplayed;
		}

        public void Unubscribe(Application controller)
        {
            controller.MessageDisplayed -= HandleMessageDisplayed;
        }

        private void HandleMessageDisplayed(object? sender, ShowMessageEventArgs e)
        {
			_ui.ShowMessage(e.Message);
        }
    }
}

