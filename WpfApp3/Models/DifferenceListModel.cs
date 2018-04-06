using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformiInventory.Models
{
    public class DifferenceListModel 
    {
        public string Store { get; set; }

        public DateTime Dt { get; set; }

        public string Art { get; set; }

        public string AN { get; set; }

        public string GTIN { get; set; }

        public decimal Difference { get; set; }

        public int UserId { get; set; }

    }
}
