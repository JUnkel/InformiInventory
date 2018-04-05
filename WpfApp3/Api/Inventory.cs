using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace informiInventory
{
    public class Inventory
    {
        public string Store { get; set; }

        public DateTime Dt { get; set; }

        public int Id  { get; set; }

        public int CrUserId { get; set; }

        public int InventoryUserId { get; set; }
    }
}
