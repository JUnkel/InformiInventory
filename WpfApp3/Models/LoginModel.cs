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
    public class LoginModel 
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime Dt { get; set; }
    }
}
