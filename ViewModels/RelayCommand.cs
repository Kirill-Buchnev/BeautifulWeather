using System.Windows.Input;

namespace BeautifulWeather.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action<object>? _execute;
        private Func<object, bool>? _canExecute;
        private ICommand? signInCommand;
        private object canSignInCommand;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(ICommand? signInCommand, object canSignInCommand)
        {
            this.signInCommand = signInCommand;
            this.canSignInCommand = canSignInCommand;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
