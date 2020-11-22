using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Notes.Helper
{
    public class TapCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action execute;
        private bool interact = true;

        public TapCommand(Action action)
        {
            execute = action;
        }

        public bool CanExecute(object parameter)
        {
            return interact;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                interact = false;
                execute.Invoke();
                interact = true;
            }
        }
    }
}
