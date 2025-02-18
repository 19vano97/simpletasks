using System;
using TaskManager.Interfaces;

namespace TaskManager.Services.Logger;

public class Logger : ILogger, IDisposable
{
    private string _logFileName;
    private object _objSync = new object();
    private Stream _logOutStream;
    private TextWriter _logWriter;

    public Logger(string logFileName)
    {
        _logFileName = string.Format($"./Logs/{logFileName}");
        var logStream = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
        _logWriter = new StreamWriter(logStream) { AutoFlush = true };
    }

    public void Dispose()
    {
        if (_logOutStream != null)
        {
            _logWriter.Flush();
            _logWriter.Dispose();
            _logOutStream.Dispose();
        }
    }

    public async Task Write(string message)
    {
        await _logWriter.WriteLineAsync(string.Format($"{DateTime.Now.ToString("yyyy-MM-dd’T’HH:mm:ss")}: {message}\n"));
    }
}