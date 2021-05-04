using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Routindo.Contract.Actions;
using Routindo.Contract.Arguments;
using Routindo.Contract.Attributes;
using Routindo.Contract.Exceptions;
using Routindo.Contract.Helpers;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Serialization.Components.Actions.Json
{
    [PluginItemInfo(ComponentUniqueId, nameof(WriteJsonToFileAction),
         "Write Json from  to file, by overwriting or appending to existing file or creating a new one.",
         Category = "Text", FriendlyName = "Write JSon to file"),
     ExecutionArgumentsClass(typeof(WriteJsonToFileActionExecutionArgs))
    ]
    public class WriteJsonToFileAction: IAction
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

        public string Id { get; set; }
        public ILoggingService LoggingService { get; set; }

        [Argument(WriteJsonToFileActionArgs.FilePath, true)] public string FilePath { get; set; }

        [Argument(WriteJsonToFileActionArgs.WriteIndented)] public bool WriteIndented { get; set; }

        [Argument(WriteJsonToFileActionArgs.Append)] public bool Append { get; set; }

        [Argument(WriteJsonToFileActionArgs.NewLineBeforeAppend)] public bool NewLineBeforeAppend { get; set; }

        [Argument(WriteJsonToFileActionArgs.NewLineAfterAppend)] public bool NewLineAfterAppend { get; set; }

        public ActionResult Execute(ArgumentCollection arguments)
        {
            try
            {
                if (arguments == null || !arguments.Any())
                    throw new MissingArgumentsException($"Arguments are mandatory to execute this action");

                if (!arguments.HasArgument(WriteJsonToFileActionExecutionArgs.Content))
                    throw new MissingArgumentException(WriteJsonToFileActionExecutionArgs.Content);

                var content = arguments[WriteJsonToFileActionExecutionArgs.Content];
                if (content == null)
                    throw new Exception($"Content is null");

                string text = ToJsonString(content, WriteIndented);
                // text = content.ToJsonString();
                
                if (string.IsNullOrWhiteSpace(FilePath))
                    throw new MissingArgumentException(WriteJsonToFileActionArgs.FilePath);

                bool append = File.Exists(FilePath) && Append;

                if (append)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (NewLineBeforeAppend)
                    {
                        stringBuilder.AppendLine();
                    }

                    if (NewLineAfterAppend)
                    {
                        stringBuilder.AppendLine(text);
                    }
                    else stringBuilder.Append(text);
                    text = stringBuilder.ToString();

                    //if (WriteIndented)
                    //    // Force Format
                    //    File.AppendAllLines(FilePath, text.Split('\n'));
                    //else 
                        File.AppendAllText(FilePath, text);

                }
                else
                {
                    //if(WriteIndented)
                    // Force Format
                    //File.WriteAllLines(FilePath, text.Split('\n'));
                    //else 
                    File.WriteAllText(FilePath, text);

                }

                return ActionResult.Succeeded();
            }
            catch (Exception exception)
            {
                LoggingService.Error(exception);
                return ActionResult.Failed(exception);
            }
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
