using System.Text.Json;
using System.Text.Json.Serialization;
using Routindo.Contract.Attributes;
using Routindo.Contract.Services;
using Routindo.Plugins.Serialization.Components.Actions.Base;

namespace Routindo.Plugins.Serialization.Components.Actions.Json
{
    [PluginItemInfo(ComponentUniqueId, nameof(WriteJsonToFileAction),
         "Write Json from  to file, by overwriting or appending to existing file or creating a new one.",
         Category = "Text", FriendlyName = "Write JSon to file"),
     ExecutionArgumentsClass(typeof(WriteContentToFileActionExecutionArgs)),
        ResultArgumentsClass(typeof(WriteContentToFileActionResultArgs))
    ]
    public class WriteJsonToFileAction: WriteContentToFileActionBase
    {
        public const string ComponentUniqueId = "6D45DC1F-FBA7-4F2B-9593-4F614973B931";

        public WriteJsonToFileAction()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            _options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        [Argument(WriteJsonToFileActionArgs.WriteIndented)] public bool WriteIndented { get; set; }

        public override string Id { get; set; }
        public override ILoggingService LoggingService { get; set; }

        protected override string GetContentSerialized(object content)
        {
            return ToJsonString(content, WriteIndented);
        }

        private readonly JsonSerializerOptions _options;

        public string ToJsonString(object obj, bool useFormat = true)
        { 
            if (useFormat)
                return JsonSerializer.Serialize(obj, _options);
            return JsonSerializer.Serialize(obj);
        }
    }
}
