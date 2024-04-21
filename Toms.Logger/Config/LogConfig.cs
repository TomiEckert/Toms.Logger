namespace Toms.Logger
{
    public class LogConfig(Action<string> callback, string fileName, bool writeToFile, LogLevel displayLevel, bool useFullClassName, bool useColors)
    {
        internal Action<string> Callback { get; set; } = callback;
        internal string FileName { get; set; } = fileName;
        internal bool WriteToFile { get; set; } = writeToFile;
        internal LogLevel DisplayLevel { get; set; } = displayLevel;
        internal bool UseFullClassName { get; set; } = useFullClassName;
        internal bool UseColors { get; set; } = useColors;
    }
}