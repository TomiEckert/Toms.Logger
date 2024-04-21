using Spectre.Console;

namespace Toms.Logger
{
    public class LogConfigBuilder
    {
        private LogConfigBuilder()
        {
            Config = new LogConfig
            (
                callback: DefaultCallback,
                fileName: "log.txt",
                writeToFile: false,
                displayLevel: LogLevel.Info,
                useFullClassName: false,
                useColors: true
            );
        }

        private void DefaultCallback(string message) {
            AnsiConsole.MarkupLine(message);
        }

        private LogConfig Config { get; }

        public static LogConfigBuilder Default => new();

        public LogConfigBuilder SetCallback(Action<string> callback)
        {
            Config.Callback = callback;
            return this;
        }

        public LogConfigBuilder SetFilename(string filename)
        {
            Config.WriteToFile = true;
            Config.FileName = filename;
            return this;
        }

        public LogConfigBuilder SaveToFile(bool save)
        {
            Config.WriteToFile = save;
            return this;
        }

        public LogConfigBuilder SetUseFullClassName(bool fullName)
        {
            Config.UseFullClassName = fullName;
            return this;
        }

        public LogConfigBuilder SetDisplayLevel(LogLevel level)
        {
            Config.DisplayLevel = level;
            return this;
        }

        public LogConfig Build()
        {
            return Config;
        }
    }
}