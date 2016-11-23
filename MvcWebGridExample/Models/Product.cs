using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebGridExample.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public string UnitPrice { get; set; }
        public string UnitsInStock { get; set; }
        public string ReorderLevel { get; set; }
        
    }
}