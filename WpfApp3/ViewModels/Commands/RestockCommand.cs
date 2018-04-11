using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformiInventory.ViewModels.Commands
{
    public class GetRestockLineModelsCommand : ICommand
    {
        RestockViewModel _vm;

        public GetRestockLineModelsCommand(RestockViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (RestockViewModel)parameter;

            if (vm == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            var vm = (RestockViewModel)parameter;
            _vm.GetRestockLineModels(vm);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

    }
        public class GetRestockModelsCommand : ICommand
        {
            RestockViewModel _vm;

            public GetRestockModelsCommand(RestockViewModel vm)
            {
                _vm = vm;
            }

            public bool CanExecute(object parameter)
            {
                var vm = (RestockViewModel)parameter;

                if (vm == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public void Execute(object parameter)
            {
                var vm = (RestockViewModel)parameter;
                _vm.GetRestockModels(vm);
            }

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;

                remove => CommandManager.RequerySuggested -= value;
            }
        }
}
