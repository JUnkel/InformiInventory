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
        private ExcelImportViewModel _excelViewModel;

        public ImportExcelRestockLinesCommand(ExcelImportViewModel vm)
        {
            _excelViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (ExcelImportViewModel)parameter;

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
            var vm = (ExcelImportViewModel)parameter;

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
        private ExcelImportViewModel _excelViewModel;

        public SaveImportedExcelRestockLinesCommand(ExcelImportViewModel vm)
        {
            _excelViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (ExcelImportViewModel)parameter;

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
            var vm = (ExcelImportViewModel)parameter;

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
        private ExcelImportViewModel _excelViewModel;

        public DeleteImportedExcelRestockLinesCommand(ExcelImportViewModel vm)
        {
            _excelViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = (ExcelImportViewModel)parameter;

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
            var vm = (ExcelImportViewModel)parameter;

            vm.DeleteImportedExcelRestockLines();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
