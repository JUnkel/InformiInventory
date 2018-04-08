﻿using informiInventory;
using InformiInventory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformiInventory.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }

        }

        string _storeName;

        public string StoreName
        {
            get { return _storeName; }
            set { SetProperty(ref _storeName, value);}
        }

        public ICommand DifferenceListCommand { get; set; }

        public ICommand InventoryCommand { get; set; }

        public ICommand NavigationCommand { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand AssemblyListViewCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand ExcelViewCommand { get; set; }

        private object _selectedViewModel;

        public object SelectedViewModel
        {
            get { return _selectedViewModel; }

            set { SetProperty(ref _selectedViewModel, value); }
        }

        public NavigationViewModel()
        {
            DifferenceListCommand = new BaseCommand(OpenDifferenceList);

            InventoryCommand = new BaseCommand(OpenInventory);

            NavigationCommand = new BaseCommand(OpenNavigationCommand);

            AssemblyListViewCommand = new BaseCommand(OpenAssemblyListViewCommand);

            LoginCommand = new BaseCommand(OpenLogin);

            MenuCommand = new BaseCommand(OpenMenu);

            CloseCommand = new BaseCommand(CloseApp);

            ExcelViewCommand = new BaseCommand(OpenExcelView); 
        }

        private void OpenExcelView(object obj)
        {
            MainWindow.Instance.MainWindowContentControl.Content = new ExcelListView();
        }

        private void OpenMenu(object obj)
        {
            MainWindow.Instance.MainWindowContentControl.Content = new MenuView();
        }

        private void OpenDifferenceList(object obj)
        {
            MainWindow.Instance.MainWindowContentControl.Content = new DifferenceListView();
        }

        private void OpenInventory(object obj)
        {
            MainWindow.Instance.MainWindowContentControl.Content = new InventoryView();
        }

        private void OpenLogin(object obj)
        {
            MainWindow.Instance.MainWindowContentControl.Content = new LoginView();
        }

        private void OpenNavigationCommand(object obj)
        {
            MainWindow.Instance.MainWindowContentControl.Content = new MenuView();
        }

        private void OpenAssemblyListViewCommand(object obj)
        {
            MainWindow.Instance.MainWindowContentControl.Content = new AssemblyListView();
        }

        private void CloseApp(object obj)
        {
            MainWindow.Instance.Close();
        }
    }

    public class BaseCommand: ICommand
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
