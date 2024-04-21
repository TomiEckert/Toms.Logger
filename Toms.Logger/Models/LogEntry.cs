using System;
using Spectre.Console;

namespace Toms.Logger
{
    internal readonly struct LogEntry : IEquatable<LogEntry>
    {
        private string ObjectName { get; }
        private string Message { get; }
        private TimeSpan Time { get; }
        internal LogLevel Level { get; }
        private bool useColors { get; }

        private LogEntry(string objectName, string message, LogLevel level, bool useColors)
        {
            ObjectName = objectName;
            Message = message;
            Time = DateTime.Now.TimeOfDay;
            Level = level;
            this.useColors = useColors;
        }

        internal static LogEntry Debug(string sender, string message, bool useColors = false)
        {
            return new LogEntry(sender, message, LogLevel.Debug, useColors);
        }

        internal static LogEntry Info(string sender, string message, bool useColors = false)
        {
            return new LogEntry(sender, message, LogLevel.Info, useColors);
        }

        internal static LogEntry Warning(string sender, string message, bool useColors = false)
        {
            return new LogEntry(sender, message, LogLevel.Warning, useColors);
        }

        internal static LogEntry Error(string sender, string message, bool useColors = false)
        {
            return new LogEntry(sender, message, LogLevel.Error, useColors);
        }

        public override string ToString()
        {
            var timeBlock = Markup.Escape("[" + Time.ToString(@"hh\:mm\:ss") + "]");
            var levelBlock = Markup.Escape(("[" + Level + "]").PadRight(9));
            var senderBlock = Markup.Escape("[" + ObjectName + "]");
            if (useColors)
            {
                var levelColor = "[white]";
                switch (Level)
                {
                    case LogLevel.Debug:
                        levelColor = "[aqua]";
                        break;
                    case LogLevel.Info:
                        levelColor = "[grey]";
                        break;
                    case LogLevel.Warning:
                        levelColor = "[yellow]";
                        break;
                    case LogLevel.Error:
                        levelColor = "[red]";
                        break;
                }
                return $"{timeBlock} {levelColor}{levelBlock}[/] {senderBlock} [grey]{Message}[/]";
            }
            return $"{timeBlock} {levelBlock} {senderBlock} {Message}";
        }

        public bool Equals(LogEntry other)
        {
            return ObjectName == other.ObjectName &&
                   Message == other.Message &&
                   Time.Equals(other.Time) &&
                   Level == other.Level;
        }

        public override bool Equals(object? obj)
        {
            return obj is LogEntry other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ObjectName, Message, Time, (int)Level);
        }
    }
}