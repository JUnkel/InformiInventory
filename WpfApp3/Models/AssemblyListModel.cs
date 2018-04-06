using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformiInventory.Models
{
    public class AssemblyListModel
    {
        public string Store { get; set; }

        public DateTime Dt { get; set; }

        public string Art { get; set; }

        public string AN { get; set; }

        public string GTIN { get; set; }

        public decimal OrderAmnt { get; set; }

        public int UserId { get; set; }
    }
}
