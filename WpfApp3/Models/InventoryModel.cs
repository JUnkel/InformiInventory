using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformiInventory.Models
{
    public class InventoryModel : INotifyPropertyChanged
    {
        public string Store { get; set; }

        public DateTime Dt { get; set; }

        public int Id { get; set; }
        
        public int CrUserId { get; set; }

        public int InventoryUserId { get; set; }
    }
}
