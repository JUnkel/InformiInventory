using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InformiInventory.ViewModels.INotifyPropertyChanged;

namespace InformiInventory.ViewModels
{
        public class NavigationViewModel : ViewModelBase.ViewModelBase
        {
            public ICommand DifferenceListCommand { get; set; }

            public ICommand InventoryCommand { get; set; }

            private object _selectedViewModel;

            public object SelectedViewModel
            {
                get { return _selectedViewModel; }
                set     { SetProperty(ref _selectedViewModel, value); }
            }

            public NavigationViewModel()
            {
                DifferenceListCommand = new BaseCommand(OpenDifferenceList);

                InventoryCommand = new BaseCommand(OpenInventory);
            }

            private void OpenDifferenceList(object obj)
            {
                SelectedViewModel = new DifferenceListViewModel();
            }
            private void OpenInventory(object obj)
            {
                SelectedViewModel = new InventoryViewModel();
            }
        }

        public class BaseCommand : ICommand
        {
            private Predicate<object> _canExecute;
            private Action<object> _method;
            public event EventHandler CanExecuteChanged;

            public BaseCommand(Action<object> method)
                : this(method, null)
            {
            }

            public BaseCommand(Action<object> method, Predicate<object> canExecute)
            {
                _method = method;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                if (_canExecute == null)
                {
                    return true;
                }

                return _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _method.Invoke(parameter);
            }
        }

}
