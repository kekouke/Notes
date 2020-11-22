using Notes.ViewModels;
using System;
using System.Windows.Input;

namespace Notes.Helper
{
    public class TapCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action execute_with_no_param;
        private Action<object> execute_with_param;
        private NotesListViewModel ViewModel { get; set; }

        private TapCommand(NotesListViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public TapCommand(Action action, NotesListViewModel viewModel) : this(viewModel)
        {
            execute_with_no_param = action;

        }
        public TapCommand(Action<object> action, NotesListViewModel viewModel) : this(viewModel)
        {
            execute_with_param = action;
        }

        public bool CanExecute(object parameter) => ViewModel.IsButtonEnabled;

        public void Execute(object parameter = null)
        {
            if (CanExecute(parameter))
            {
                execute_with_no_param?.Invoke();
                execute_with_param?.Invoke(parameter);
            }
        }
    }
}
