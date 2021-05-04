using System;
using Routindo.Contract.Arguments;
using Routindo.Plugins.Serialization.Components.Actions.Text;

namespace Routindo.Plugins.Serialization.UI.ViewModels
{
    public class TextToFileActionViewModel: ContentToFileActionViewModel
    {
        public override void Configure()
        {
            this.InstanceArguments = ArgumentCollection.New()
                    .WithArgument(WriteTextToFileActionArgs.FilePath, FilePath)
                    .WithArgument(WriteTextToFileActionArgs.Append, Append)
                    .WithArgument(WriteTextToFileActionArgs.NewLineBeforeAppend, NewLineBeforeAppend)
                    .WithArgument(WriteTextToFileActionArgs.NewLineAfterAppend, NewLineAfterAppend)
                ;
        }

        public override void SetArguments(ArgumentCollection arguments)
        {
            if (arguments.HasArgument(WriteTextToFileActionArgs.FilePath))
                FilePath = arguments.GetValue<string>(WriteTextToFileActionArgs.FilePath);

            if (arguments.HasArgument(WriteTextToFileActionArgs.Append))
                Append = arguments.GetValue<bool>(WriteTextToFileActionArgs.Append);

            if (arguments.HasArgument(WriteTextToFileActionArgs.NewLineBeforeAppend))
                NewLineBeforeAppend = arguments.GetValue<bool>(WriteTextToFileActionArgs.NewLineBeforeAppend);

            if (arguments.HasArgument(WriteTextToFileActionArgs.NewLineAfterAppend))
                NewLineAfterAppend = arguments.GetValue<bool>(WriteTextToFileActionArgs.NewLineAfterAppend);
        }
    }
}
