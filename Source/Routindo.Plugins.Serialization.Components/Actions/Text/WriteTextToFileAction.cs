using Routindo.Contract.Attributes;
using Routindo.Contract.Services;
using Routindo.Plugins.Serialization.Components.Actions.Base;

namespace Routindo.Plugins.Serialization.Components.Actions.Text
{
    [PluginItemInfo(ComponentUniqueId, nameof(WriteTextToFileAction),
         "Write Text Content to file, by overwriting or appending to existing file or creating a new one.",
         Category = "Serialization", FriendlyName = "Write Text to file"),
     ExecutionArgumentsClass(typeof(WriteContentToFileActionExecutionArgs)),
    ResultArgumentsClass(typeof(WriteContentToFileActionResultArgs))
    ]
    public class WriteTextToFileAction : WriteContentToFileActionBase
    {
        public const string ComponentUniqueId = "91CA4230-6FA5-4DC1-AEE0-C474A64F95E5";

        public override string Id { get; set; }
        public override ILoggingService LoggingService { get; set; }
        protected override string GetContentSerialized(object content)
        {
            return content.ToString();
        }
    }
}