using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformiInventory.ViewModels.Commands
{
    public class LoginCommand : ICommand
    {
        private Inventory.ViewModels.LoginViewModel _loginViewModel;

        public LoginCommand(Inventory.ViewModels.LoginViewModel vm)
        {
            _loginViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (Inventory.ViewModels.LoginViewModel)parameter;
            
            if(vm.Password != null && vm.Username != null)
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

        public event EventHandler CanExecuteChanged;
    }
}
