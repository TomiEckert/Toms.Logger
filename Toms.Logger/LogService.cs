namespace Toms.Logger;

internal class LogService
{    
    internal static readonly LogService Instance = new();
    internal bool UseColors {get; set;}

    private LogService()
    {
        var config = LogConfigBuilder.Default.Build();
        entries = [];
        callback = config.Callback;
        writeToFile = config.WriteToFile;
        filename = config.FileName;
        displayLevel = config.DisplayLevel;
        UseColors = config.UseColors;
    }

    internal void Configure(LogConfig config)
    {
        entries = [];
        callback = config.Callback;
        writeToFile = config.WriteToFile;
        filename = config.FileName;
        displayLevel = config.DisplayLevel;
        UseColors = config.UseColors;
    }

    private readonly object _fileLock = new object();

    private readonly object _listLock = new object();
    private Action<string> callback;
    private List<LogEntry> entries;
    private string filename;
    private bool writeToFile;
    private LogLevel displayLevel;

    private Action<string> Callback => callback;
    private List<LogEntry> Entries => entries;
    private string Filename => filename;
    private bool WriteToFile => writeToFile;
    private LogLevel DisplayLevel => displayLevel;
    internal void Add(LogEntry entry)
    {
        lock (_listLock)
        {
            Entries.Add(entry);
        }

        if (entry.Level >= DisplayLevel)
            Callback?.Invoke(entry.ToString());

        if (!WriteToFile) return;
        lock (_fileLock)
        {
            File.AppendAllText(Filename, entry + Environment.NewLine);
        }
    }

    internal void SaveToFile(string filename, bool append = true)
    {
        string content;
        lock (_listLock)
        {
            content = string.Join(Environment.NewLine, Entries);
        }

        lock (_fileLock)
        {
            if (!File.Exists(filename)) File.Create(filename).Close();
            else if (append) content = File.ReadAllText(filename) + Environment.NewLine + content;
            File.WriteAllText(filename, content);
        }
    }
}