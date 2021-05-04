using System;
using Routindo.Contract.Arguments;
using Routindo.Plugins.Serialization.Components.Actions.Base;
using Routindo.Plugins.Serialization.Components.Actions.Text;

namespace Routindo.Plugins.Serialization.UI.ViewModels
{
    public class TextToFileActionViewModel: ContentToFileActionViewModel
    {
        public override void Configure()
        {
            this.InstanceArguments = ArgumentCollection.New()
                    .WithArgument(WriteContentToFileActionArgs.FilePath, FilePath)
                    .WithArgument(WriteContentToFileActionArgs.Append, Append)
                    .WithArgument(WriteContentToFileActionArgs.NewLineBeforeAppend, NewLineBeforeAppend)
                    .WithArgument(WriteContentToFileActionArgs.NewLineAfterAppend, NewLineAfterAppend)
                ;
        }

        public override void SetArguments(ArgumentCollection arguments)
        {
            if (arguments.HasArgument(WriteContentToFileActionArgs.FilePath))
                FilePath = arguments.GetValue<string>(WriteContentToFileActionArgs.FilePath);

            if (arguments.HasArgument(WriteContentToFileActionArgs.Append))
                Append = arguments.GetValue<bool>(WriteContentToFileActionArgs.Append);

            if (arguments.HasArgument(WriteContentToFileActionArgs.NewLineBeforeAppend))
                NewLineBeforeAppend = arguments.GetValue<bool>(WriteContentToFileActionArgs.NewLineBeforeAppend);

            if (arguments.HasArgument(WriteContentToFileActionArgs.NewLineAfterAppend))
                NewLineAfterAppend = arguments.GetValue<bool>(WriteContentToFileActionArgs.NewLineAfterAppend);
        }
    }
}
