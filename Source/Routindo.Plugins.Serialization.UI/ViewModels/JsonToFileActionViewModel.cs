using Routindo.Contract.Arguments;
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
                    .WithArgument(WriteJsonToFileActionArgs.FilePath, FilePath)
                    .WithArgument(WriteJsonToFileActionArgs.Append, Append)
                    .WithArgument(WriteJsonToFileActionArgs.NewLineBeforeAppend, NewLineBeforeAppend)
                    .WithArgument(WriteJsonToFileActionArgs.NewLineAfterAppend, NewLineAfterAppend)
                    .WithArgument(WriteJsonToFileActionArgs.WriteIndented, WriteIndented)
                ;
        }

        public override void SetArguments(ArgumentCollection arguments)
        {
            if (arguments.HasArgument(WriteJsonToFileActionArgs.FilePath))
                FilePath = arguments.GetValue<string>(WriteJsonToFileActionArgs.FilePath);

            if (arguments.HasArgument(WriteJsonToFileActionArgs.Append))
                Append = arguments.GetValue<bool>(WriteJsonToFileActionArgs.Append);

            if (arguments.HasArgument(WriteJsonToFileActionArgs.NewLineBeforeAppend))
                NewLineBeforeAppend = arguments.GetValue<bool>(WriteJsonToFileActionArgs.NewLineBeforeAppend);

            if (arguments.HasArgument(WriteJsonToFileActionArgs.NewLineAfterAppend))
                NewLineAfterAppend = arguments.GetValue<bool>(WriteJsonToFileActionArgs.NewLineAfterAppend);

            if (arguments.HasArgument(WriteJsonToFileActionArgs.WriteIndented))
                WriteIndented = arguments.GetValue<bool>(WriteJsonToFileActionArgs.WriteIndented);
        }
    }
}
