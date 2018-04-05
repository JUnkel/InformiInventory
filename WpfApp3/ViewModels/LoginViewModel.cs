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
using informiInventory;
using System.Configuration;

namespace InformiInventory
{
    public class LoginViewModel : ViewModelBase
    {
        private ICommand _loginCommand = null;

        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new LoginCommand(this));

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
            var connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (var db = new PetaPoco.Database(connectionString))
            {
                var user = db.FirstOrDefault<User>("SELECT 1 UID, Username FROM Users WHERE KeyCode = @0 AND Username = @1", Password, Username);

                if (user == null)
                {
                    MessageBox.Show("Anmeldung fehlgeschlagen.", "Informi Inventory", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MainWindow.Instance.Content = new MenuView();
                }
            }
        }
    }
}
