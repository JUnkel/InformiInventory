using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Inventory.ViewModels
{
    class LoginViewModel : InformiInventory.ViewModels.Commands.ViewModelBase
    {
        public LoginViewModel()
        {

        }

        public DelegateCommand LoginCommand { get; set; }

        string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username,value); }
        }


        string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password,value); }
        }

        DateTime _dt = DateTime.Now;

        public DateTime Dt
        {
            get { return _dt; }

            set { SetProperty(ref _dt,value); }
        }
    }
}
