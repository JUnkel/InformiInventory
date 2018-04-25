//using InformiInventory.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace InformiInventory.ViewModels.Commands
//{

//    public class GetRestockModelsCommand : ICommand
//    {
//        RestockViewModel _vm;

//        public GetRestockModelsCommand(RestockViewModel vm)
//        {
//            _vm = vm;
//        }

//        public bool CanExecute(object parameter)
//        {
//            var vm = (RestockViewModel)parameter;

//            if (vm == null)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        public void Execute(object parameter)
//        {
//            _execute.invoke();
//        }

//        public event EventHandler CanExecuteChanged
//        {
//            add => CommandManager.RequerySuggested += value;

//            remove => CommandManager.RequerySuggested -= value;
//        }
//    }

//    public class SaveStoreRestockLineCommand : ICommand
//    {
//        RestockViewModel _vm;

//        public PropertyChangedEventHandler PropertyChanged { get; private set; }

//        public SaveStoreRestockLineCommand(RestockViewModel vm)
//        {
//            _vm = vm;
//        }

//        public bool CanExecute(object parameter)
//        {

//            var vm = (RestockViewModel)parameter;

//            if (vm == null) return false;

//            else if (vm.SelectedRestockModel == null) return false;

//            else if (vm.SelectedRestockModel.IsProcd || vm.SelectedRestockModel.TemplateId == null) return false;

//            else if (vm.SelectedRestockLineModel == null) return false;



//            else return true;
//        }

//        public void Execute(object parameter)
//        {
//            var vm = (RestockViewModel)parameter;

//            vm.SaveRestockLine(vm);
//        }

//        public event EventHandler CanExecuteChanged
//        {
//            add => CommandManager.RequerySuggested += value;

//            remove => CommandManager.RequerySuggested -= value;
//        }
//    }

//    public class CreateNewRestockModelCommand : ICommand
//    {
//        RestockViewModel _vm;

//        public CreateNewRestockModelCommand(RestockViewModel vm)
//        {
//            _vm = vm;
//        }

//        public bool CanExecute(object parameter)
//        {
//            var vm = (RestockViewModel)parameter;

//            if (vm == null)
//            {
//                return false;
//            }

//            else if (vm.RestockModels.Any(x => x.TemplateId != null && x.IsProcd == false)) return false;

//            else
//            {
//                return true;
//            }
//        }

//        public void Execute(object parameter)
//        {
//            var vm = (RestockViewModel)parameter;
//            vm.CreateNewRestockModel(vm);
//        }

//        public event EventHandler CanExecuteChanged
//        {
//            add => CommandManager.RequerySuggested += value;

//            remove => CommandManager.RequerySuggested -= value;
//        }
//    }

//    public class DeleteRestockModelCommand : ICommand
//    {
//        RestockViewModel _vm;

//        public DeleteRestockModelCommand(RestockViewModel vm)
//        {
//            _vm = vm;
//        }

//        public bool CanExecute(object parameter)
//        {
//            var vm = (RestockViewModel)parameter;

//            if (vm == null)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        public void Execute(object parameter)
//        {
//            var vm = (RestockViewModel)parameter;
//            vm.DeleteRestockModel(vm);
//        }

//        public event EventHandler CanExecuteChanged
//        {
//            add => CommandManager.RequerySuggested += value;

//            remove => CommandManager.RequerySuggested -= value;
//        }
//    }
//}
