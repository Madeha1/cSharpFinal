using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_Project
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int InventoryLevel { get; set; }

        public TransactionItem transactionItem { get; set; }


    }
}
