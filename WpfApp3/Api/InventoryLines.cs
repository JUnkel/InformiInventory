using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace informiInventory
{
    public class InventoryLine
    {
        public string Store { get; set; }

        public int Index { get; set; }

        public string Art { get; set; }

        public string AN { get; set; }

        public string GTIN { get; set; }

        public decimal ActualStock { get; set; }

        public decimal TargetStock { get; set; }
    }
}
