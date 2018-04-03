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
using InformiInventory.ViewModels.Commands.Generic;

namespace Inventory.ViewModels
{
    public class LoginViewModel : InformiInventory.ViewModels.Commands.ViewModelBase
    {
        public LoginViewModel()
        {
            _loginCommand = new InformiInventory.ViewModels.Commands.LoginCommand(this);
        }

        private InformiInventory.ViewModels.Commands.LoginCommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                return new InformiInventory.ViewModels.Commands.LoginCommand(this);
            }
        }

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

            if (Password == "prosoft")
            {
                System.Windows.Forms.MessageBox.Show("Anmeldung erfolgreich.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Anmeldung fehlgeschlagen.");
            }
        }
    }
}
