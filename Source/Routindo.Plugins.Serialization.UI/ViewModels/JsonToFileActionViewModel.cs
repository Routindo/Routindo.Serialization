using Routindo.Contract.Arguments;
using Routindo.Plugins.Serialization.Components.Actions.Base;
using Routindo.Plugins.Serialization.Components.Actions.Json;

namespace Routindo.Plugins.Serialization.UI.ViewModels
{
    public class JsonToFileActionViewModel: ContentToFileActionViewModel
    {
        private bool _writeIndented;

        protected override string GetOutputFileExtension()
        {
            return "Json files (*.json)|*.json|All files (*.*)|*.*";
        }

        public bool WriteIndented
        {
            get => _writeIndented;
            set
            {
                _writeIndented = value;
                OnPropertyChanged();
            }
        }

        public override void Configure()
        {
            this.InstanceArguments = ArgumentCollection.New()
                    .WithArgument(WriteContentToFileActionArgs.FilePath, FilePath)
                    .WithArgument(WriteContentToFileActionArgs.Append, Append)
                    .WithArgument(WriteContentToFileActionArgs.NewLineBeforeAppend, NewLineBeforeAppend)
                    .WithArgument(WriteContentToFileActionArgs.NewLineAfterAppend, NewLineAfterAppend)
                    .WithArgument(WriteJsonToFileActionArgs.WriteIndented, WriteIndented)
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

            if (arguments.HasArgument(WriteJsonToFileActionArgs.WriteIndented))
                WriteIndented = arguments.GetValue<bool>(WriteJsonToFileActionArgs.WriteIndented);
        }
    }
}
