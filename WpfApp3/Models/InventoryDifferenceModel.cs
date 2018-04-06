using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace informiInventory.Models
{
    public class InventoryDifference
    {
        public string Art { get; set; }

        public string An { get; set; }

        public string GTIN { get; set; }

        public decimal Deviation { get; set; }
    }
}
