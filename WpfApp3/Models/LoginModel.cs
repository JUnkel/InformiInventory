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

namespace InformiInventory.Models 
{
    public class LoginModel : ViewModelBase 
    {
        protected virtual bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
