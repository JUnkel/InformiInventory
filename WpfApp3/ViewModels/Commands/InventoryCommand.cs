using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformiInventory.ViewModels.Commands
{
    public class InventoryCommand : ICommand
    {
        private InventoryViewModel _vm;

        public InventoryCommand(InventoryViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            var userControl = (UserControl)parameter;

            if (userControl == null)
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
            _vm.GetInventories();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
