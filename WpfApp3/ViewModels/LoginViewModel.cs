using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Telerik.Windows.Controls;
using InformiInventory;
using System.Windows;

namespace InformiInventory.ViewModels
{
    public class LoginViewModel : InformiInventory.ViewModels.ViewModelBase.ViewModelBase
    {
        private ICommand _loginCommand = null;

        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new InformiInventory.ViewModels.Commands.LoginCommand(this));

        string _username;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        DateTime _dt = DateTime.Now;

        public DateTime Dt
        {
            get { return _dt; }

            set { SetProperty(ref _dt, value); }
        }

        public void LogIn()
        {
            //Abfrage Datenbank

            if (Password == "prosoft" && Username == "admin")
            {
                Application curApp = Application.Current;

                curApp.MainWindow.Content = new InformiInventory.Views.MenuView();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Anmeldung fehlgeschlagen.");
            }
        }
    }
}
