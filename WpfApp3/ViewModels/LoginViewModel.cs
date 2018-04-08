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
using InformiInventory.Views;
using InformiInventory.Commands;

namespace InformiInventory.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Properties
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

        #endregion

        #region Commands
        private ICommand _loginCommand = null;

        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new LoginCommand(this));

        public void LogIn()
        {
            using (var db = new PetaPoco.Database("db"))
            {
                var user = new User();

                try
                {
                    user = db.FirstOrDefault<User>("SELECT u.UserId AS UserId, u.UserName as UserName, u.StoreId as StoreId FROM Users u LEFT JOIN Stores s ON s.StoreId = u.StoreId WHERE KeyCode = @0 AND Username = @1", Password, Username);

                    if (user == null)
                    {
                        MessageBox.Show("Anmeldung fehlgeschlagen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var currentStore = db.ExecuteScalar<string>("SELECT StoreName FROM Stores WHERE StoreId = @0",user.StoreId);

                        var naviContext = (NavigationViewModel)MainWindow.Instance.NavigationPanel.DataContext;

                        naviContext.CurrentUser = user;

                        naviContext.StoreName = currentStore;

                        MainWindow.Instance.MainWindowContentControl.Content = new MenuView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Daten konnten nicht abgerufen werden:\n\n" + ex.Message), "Fehler");
                }
            }
        }
        #endregion
    }
}
