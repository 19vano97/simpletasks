using System;
using TaskManager.Controllers;
using TaskManager.View;

namespace TaskManager.Handlers;

public class MessageDisplayHandler
{
    public MessageDisplayHandler()
    {
    }

    public void Subscribe(AppController controller)
	{
		controller.MessageDisplayed += HandleMessageDisplayed;
	}

    public void Unubscribe(AppController controller)
    {
        controller.MessageDisplayed -= HandleMessageDisplayed;
    }

    private void HandleMessageDisplayed(object? sender, ShowMessageEventArgs e)
    {
		GlobalUi.DisplayMessage(e.Message);
    }
}
