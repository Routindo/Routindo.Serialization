using System.Windows.Input;
using Microsoft.Win32;
using Routindo.Contract.UI;

namespace Routindo.Plugins.Serialization.UI.ViewModels
{
    public abstract class ContentToFileActionViewModel: PluginConfiguratorViewModelBase
    {
        private string _filePath;
        
        private bool _append;
        private bool _newLineBeforeAppend;
        private bool _newLineAfterAppend;

        public ContentToFileActionViewModel()
        {
            SelectOutputFileCommand = new RelayCommand(SelectOutputFile);
        } 

        private void SelectOutputFile()
        {
            SaveFileDialog openFileDialog = new SaveFileDialog() { CheckFileExists = false, Title = "Output File" };
            openFileDialog.Filter = GetOutputFileExtension();
            var result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                this.FilePath = openFileDialog.FileName;
            }
        }

        protected virtual string GetOutputFileExtension()
        {
            return "All files (*.*)|*.*";
        }

        public ICommand SelectOutputFileCommand { get; }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                ClearPropertyErrors();
                ValidateNonNullOrEmptyString(_filePath);
                OnPropertyChanged();
            }
        }

        public bool Append
        {
            get => _append;
            set
            {
                _append = value;
                OnPropertyChanged();
            }
        }

        public bool NewLineBeforeAppend
        {
            get => _newLineBeforeAppend;
            set
            {
                _newLineBeforeAppend = value;
                OnPropertyChanged();
            }
        }

        public bool NewLineAfterAppend
        {
            get => _newLineAfterAppend;
            set
            {
                _newLineAfterAppend = value;
                OnPropertyChanged();
            }
        }

       
    }
}
