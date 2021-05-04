using System;
using System.IO;
using System.Linq;
using System.Text;
using Routindo.Contract.Actions;
using Routindo.Contract.Arguments;
using Routindo.Contract.Attributes;
using Routindo.Contract.Exceptions;
using Routindo.Contract.Services;

namespace Routindo.Plugins.Serialization.Components.Actions.Base
{
    public abstract class WriteContentToFileActionBase : IAction
    {
        public abstract string Id { get; set; }
        public abstract ILoggingService LoggingService { get; set; }

        [Argument(WriteContentToFileActionArgs.FilePath, true)]
        public string FilePath { get; set; }

        [Argument(WriteContentToFileActionArgs.Append)]
        public bool Append { get; set; }

        [Argument(WriteContentToFileActionArgs.NewLineBeforeAppend)]
        public bool NewLineBeforeAppend { get; set; }

        [Argument(WriteContentToFileActionArgs.NewLineAfterAppend)]
        public bool NewLineAfterAppend { get; set; }

        public ActionResult Execute(ArgumentCollection arguments)
        {
            string text = null;
            try
            {
                if (arguments == null || !arguments.Any())
                    throw new MissingArgumentsException($"Arguments are mandatory to execute this action");

                if (!arguments.HasArgument(WriteContentToFileActionExecutionArgs.Content))
                    throw new MissingArgumentException(WriteContentToFileActionExecutionArgs.Content);

                var content = arguments[WriteContentToFileActionExecutionArgs.Content];
                if (content == null)
                    throw new Exception($"Content is null");
                text = GetContentSerialized(content);

                if (string.IsNullOrWhiteSpace(FilePath))
                    throw new MissingArgumentException(WriteContentToFileActionArgs.FilePath);

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

                return ActionResult.Succeeded().WithAdditionInformation(ArgumentCollection.New()
                    .WithArgument(WriteContentToFileActionResultArgs.SerializedContent, text)
                    .WithArgument(WriteContentToFileActionResultArgs.OutputFilePath, FilePath)
                );
            }
            catch (Exception exception)
            {
                LoggingService.Error(exception);
                return ActionResult.Failed(exception).WithAdditionInformation(ArgumentCollection.New()
                    .WithArgument(WriteContentToFileActionResultArgs.SerializedContent, text)
                    .WithArgument(WriteContentToFileActionResultArgs.OutputFilePath, FilePath)
                );
            }

        }

        protected abstract string GetContentSerialized(object content);
    }
}
