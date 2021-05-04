using System;
using System.IO;
using System.Linq;
using System.Text;
using Routindo.Contract.Actions;
using Routindo.Contract.Arguments;
using Routindo.Contract.Attributes;
using Routindo.Contract.Exceptions;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Serialization.Components.Actions
{
    [PluginItemInfo(ComponentUniqueId, nameof(WriteContentToFileAction),
         "Write Content to file, by overwriting or appending to existing file or creating a new one.", 
         Category = "Text", FriendlyName = "Write content to file"),
     ExecutionArgumentsClass(typeof(WriteTextActionExecutionArgs))
    ]
    public class WriteContentToFileAction: IAction
    {
        public const string ComponentUniqueId = "91CA4230-6FA5-4DC1-AEE0-C474A64F95E5";
        public string Id { get; set; }
        public ILoggingService LoggingService { get; set; }

        [Argument(WriteTextActionArgs.FilePath, true)] public string FilePath { get; set; }

        [Argument(WriteTextActionArgs.Append)] public bool Append { get; set; }

        [Argument(WriteTextActionArgs.NewLineBeforeAppend)] public bool NewLineBeforeAppend { get; set; }

        [Argument(WriteTextActionArgs.NewLineAfterAppend)] public bool NewLineAfterAppend { get; set; }

        public ActionResult Execute(ArgumentCollection arguments)
        { 
            try
            {
                if (arguments == null || !arguments.Any())
                    throw new MissingArgumentsException($"Arguments are mandatory to execute this action");

                if (!arguments.HasArgument(WriteTextActionExecutionArgs.Content))
                    throw new MissingArgumentException(WriteTextActionExecutionArgs.Content);

                var content = arguments.GetValue<object>(WriteTextActionExecutionArgs.Content);
                if (content == null)
                    throw new Exception($"Text is null");
                string text = content.ToString();

                if (string.IsNullOrWhiteSpace(FilePath))
                    throw new MissingArgumentException(WriteTextActionArgs.FilePath);

                bool append = File.Exists(FilePath) && Append;

                if (append)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (NewLineBeforeAppend)
                    {
                        stringBuilder.AppendLine();
                    }
                    if (NewLineAfterAppend)
                        stringBuilder.AppendLine(text);
                    else stringBuilder.Append(text);
                    text = stringBuilder.ToString();
                    File.AppendAllText(FilePath, text);
                }
                else
                    File.WriteAllText(FilePath, text);

                return ActionResult.Succeeded();
            }
            catch (Exception exception)
            {
                LoggingService.Error(exception);
                return ActionResult.Failed(exception);
            }
        }
    }
}