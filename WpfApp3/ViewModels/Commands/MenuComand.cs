using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;




namespace InformiInventory.ViewModels.Commands
{
    //public class MenuCommand : ICommand
    //{
    //    private MenuViewModel  _menuViewModel;

    //    public MenuCommand(MenuViewModel vm)
    //    {
    //        _menuViewModel = vm;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        var viewName = (string)parameter;
            
    //        if(string.IsNullOrWhiteSpace(viewName))
    //        {
    //            return false;
    //        }
    //        else
    //        {
    //            return true;
    //        }
    //    }

    //    public void Execute(object parameter)
    //    {
    //        var usercontrol = (UserControl)parameter;
    //    }

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add => CommandManager.RequerySuggested += value;

    //        remove => CommandManager.RequerySuggested -= value;
    //    }
    //}
}
