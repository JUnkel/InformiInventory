using InformiInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformiInventory.Commands
{
    public class LoginCommand : ICommand
    {
        private LoginViewModel _loginViewModel;

        public LoginCommand(LoginViewModel vm)
        {
            _loginViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (LoginViewModel)parameter;
            
            if(vm == null)
            {
                return false;
            }
            else if(!string.IsNullOrWhiteSpace(vm.Password)  && !string.IsNullOrWhiteSpace(vm.Username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            _loginViewModel.LogIn();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
