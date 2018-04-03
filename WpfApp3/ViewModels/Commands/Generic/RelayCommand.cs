using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformiInventory.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _action;

        private Func<bool> _func;

        public RelayCommand(Action<object> action, Func<bool> func)
        {
            _action = action;
            _func = func;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}

