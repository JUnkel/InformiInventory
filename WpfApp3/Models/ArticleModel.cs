using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace informiInventory.Models
{
    public class Article
    {
        public string ArtNo { get; set; }

        public string Desc { get; set; }

        public int Amt { get; set; }

        public string Storage { get; set; }
    }
}
