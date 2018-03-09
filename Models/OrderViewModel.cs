using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THA.WEB
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
                
        public DateTime? OrderDate { get; set; }

        public string CustomerID { get; set; }

        [MaxLength(40)]
        public string ShipName { get; set; }

        [MaxLength(60)]
        public string ShipAddress { get; set; }
        
        [MaxLength(15)]
        public string ShipCity { get; set; }
    }
}