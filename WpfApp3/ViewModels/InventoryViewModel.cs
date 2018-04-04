using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformiInventory.ViewModels
{
    public class InventoryViewModel : ViewModelBase.ViewModelBase
    {
        public void Navigate(string View)
        {
            Application curApp = Application.Current;

            curApp.MainWindow.Content = new InformiInventory.Views.MenuView();
        }

    }
}
