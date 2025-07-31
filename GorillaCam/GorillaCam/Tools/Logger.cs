using BepInEx.Logging;

namespace GorillaCam.Tools
{
    public static class Logger
    {
        public static ManualLogSource LogSource;

        public static void Log(object message) => Write(message);
        public static void Warn(object message) => Write(message, LogLevel.Warning);
        public static void Error(object message) => Write(message, LogLevel.Error);
        public static void Fatal(object message) => Write(message, LogLevel.Fatal);

        private static void Write(object message, LogLevel level = LogLevel.Info) => LogSource?.Log(level, message?.ToString());
    }
}