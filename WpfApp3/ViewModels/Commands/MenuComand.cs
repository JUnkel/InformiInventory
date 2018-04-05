using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformiInventory
{
    public class NavigationCommand : ICommand
    {
        private NavigationViewModel _navigationViewModel;

        public NavigationCommand(NavigationViewModel vm)
        {
            _navigationViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var usercontrol = (UserControl)parameter;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
