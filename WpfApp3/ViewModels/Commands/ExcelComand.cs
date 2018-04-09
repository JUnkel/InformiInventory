using InformiInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformiInventory.Commands
{
    public class ImportExcelRestockLinesCommand : ICommand
    {
        private ExcelViewModel _excelViewModel;

        public ImportExcelRestockLinesCommand(ExcelViewModel vm)
        {
            _excelViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (ExcelViewModel)parameter;

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
            var vm = (ExcelViewModel)parameter;

            vm.ImportExcel();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class SaveImportedExcelRestockLinesCommand : ICommand
    {
        private ExcelViewModel _excelViewModel;

        public SaveImportedExcelRestockLinesCommand(ExcelViewModel vm)
        {
            _excelViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (ExcelViewModel)parameter;

            if (vm == null)
            {
                return false;
            }
            else if(vm.RestockModelLines.Count == 0)
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
            var vm = (ExcelViewModel)parameter;

            vm.SaveImportedExcelRestockLines(vm);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class DeleteImportedExcelRestockLinesCommand : ICommand
    {
        private ExcelViewModel _excelViewModel;

        public DeleteImportedExcelRestockLinesCommand(ExcelViewModel vm)
        {
            _excelViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (ExcelViewModel)parameter;

            if (vm == null)
            {
                return false;
            }
            else if (vm.RestockModelLines.Count == 0)
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
            var vm = (ExcelViewModel)parameter;

            vm.DeleteImportedExcelRestockLines();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
