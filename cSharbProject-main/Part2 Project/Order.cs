using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Part2_Project
{
    public enum OrderStatus {InProgress , Pending, Delivered }
    public class Order:IShoppingBag
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }

        public float Final_Cost { get; set; }

        public ICollection<TransactionItem> transactionItems { get; set; }
        public Customer Customer { get; set; }
        public void AddItem(TransactionItem  item) {
            transactionItems.Add(item);
        }
        public void DeleteItem(TransactionItem item) {
            transactionItems.Remove(item);
        }
        public void UpdateQuantity(TransactionItem item, int quantity) {
            item.Quantity = quantity;
        }

        public string PrintShoppingBag()
        {
            string s = $"ID = {ID} , Date: {Date}, Status: {Status.ToString()}, Total Cost = {Final_Cost}. ";
            return s;
            
        }
           
    }
        
}

