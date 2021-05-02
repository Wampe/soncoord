using Prism.Commands;
using Prism.Mvvm;

namespace Soncoord.Client.WPF.Modules.Shell
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel()
        {
            MyCommand = new DelegateCommand(CommandLoadExecute, CanCommandLoadExecute);
            ButtonContent = "Hello, click me!";
        }

        public DelegateCommand MyCommand { get; set; }

        private bool CanCommandLoadExecute()
        {
            return true;
        }

        private void CommandLoadExecute()
        {
            ButtonContent = "Clicked!";
        }

        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set => SetProperty(ref _buttonContent, value);
        }
    }
}
