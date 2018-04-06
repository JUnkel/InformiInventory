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
    public class ExcelCommand : ICommand
    {
        private ExcelViewModel _excelViewModel;

        public ExcelCommand(ExcelViewModel vm)
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
}
