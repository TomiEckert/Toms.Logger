using System.Diagnostics;
using System.Reflection;

namespace Toms.Logger;

public class Logger
{
    private LogLevel displayLevel;
    private bool useColors;
    private bool useFullClassName;

    public Logger() {
        var config = LogConfigBuilder.Default.Build();
        displayLevel = config.DisplayLevel;
        useFullClassName = config.UseFullClassName;
        useColors = config.UseColors;
    }

    public Logger(LogConfig config) {
        LogService.Instance.Configure(config);
        displayLevel = config.DisplayLevel;
        useFullClassName = config.UseFullClassName;
        useColors = config.UseColors;
    }

    public void SaveToFile(string filename)
    {
        LogService.Instance.SaveToFile(filename);
    }

    public void Debug(string message)
    {
        if (displayLevel > LogLevel.Debug) return;
        var entry = LogEntry.Debug(GetSender(), message, useColors);
        LogService.Instance.Add(entry);
    }

    public void Info(string message)
    {
        if (displayLevel > LogLevel.Info) return;
        var entry = LogEntry.Info(GetSender(), message, useColors);
        LogService.Instance.Add(entry);
    }

    public void Warning(string message)
    {
        if (displayLevel > LogLevel.Warning) return;
        var entry = LogEntry.Warning(GetSender(), message, useColors);
        LogService.Instance.Add(entry);
    }

    public void Error(string message)
    {
        if (displayLevel > LogLevel.Error) return;
        var entry = LogEntry.Error(GetSender(), message, useColors);
        LogService.Instance.Add(entry);
    }

    private string GetSender()
    {
        var mb = new StackTrace().GetFrame(2)?.GetMethod();
        var mi = mb as MethodInfo;
        if (mi == null) return mb?.Name ?? "Unknown";
        var c = useFullClassName ? mi.DeclaringType?.Name + "." : "";
        return c + mi.Name;
    }
}
