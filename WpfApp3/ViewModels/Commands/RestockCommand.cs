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
            else if (vm.RestockModels.Count == 0)
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

            vm.GetRestockLineModels();
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
                vm.GetRestockModels();
            }

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;

                remove => CommandManager.RequerySuggested -= value;
            }
        }

        public class SaveStoreRestockLineCommand : ICommand
        {
            RestockViewModel _vm;

            public SaveStoreRestockLineCommand(RestockViewModel vm)
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
                vm.SaveRestockLine(vm);
            }

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;

                remove => CommandManager.RequerySuggested -= value;
            }
        }

    

    public class CreateNewRestockModelCommand : ICommand
    {
        RestockViewModel _vm;

        public CreateNewRestockModelCommand(RestockViewModel vm)
        {
           _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
           var vm = (RestockViewModel)parameter;

            if(vm == null)
            {
                return false;
            }
            else if (vm.SelectedRestockModel == null || !vm.SelectedRestockModel.IsTemplate)
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
           vm.CreateNewRestockModel(vm);
        }

        public event EventHandler CanExecuteChanged
        {
           add => CommandManager.RequerySuggested += value;

           remove => CommandManager.RequerySuggested -= value;
        }
    }
}
