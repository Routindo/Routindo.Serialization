namespace Routindo.Plugins.Serialization.Components.Actions.Json
{
    public static class WriteJsonToFileActionArgs
    {  
        public const string FilePath = nameof(FilePath);
        public const string Append = nameof(Append);
        public const string NewLineBeforeAppend = nameof(NewLineBeforeAppend);
        public const string NewLineAfterAppend = nameof(NewLineAfterAppend);
        public const string WriteIndented = nameof(WriteIndented);
    }
}