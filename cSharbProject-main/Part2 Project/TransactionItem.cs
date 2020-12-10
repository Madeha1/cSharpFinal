using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_Project
{
    public class TransactionItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }

        public float CostPerItem { get; set; }

        public Order Order { get; set; }
        
        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
