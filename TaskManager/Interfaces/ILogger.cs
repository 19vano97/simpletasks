using System;

namespace TaskManager.Interfaces;

public interface ILogger
{
    public Task Write(string message);
    public void Dispose();
}
