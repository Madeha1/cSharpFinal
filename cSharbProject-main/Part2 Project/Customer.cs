using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_Project
{
    public class Customer
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Order> OrderList { get; set; }
    }
}
