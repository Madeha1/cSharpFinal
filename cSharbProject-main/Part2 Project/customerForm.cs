using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace Part2_Project
{
    public partial class customerForm : Form
    {
        public CompCtx ctx = new CompCtx();
        int id;
        Order newOrder;

        public customerForm()
        {
            InitializeComponent();
        }
        public customerForm(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        //calfinal cost
        float cal_cost(TransactionItem t)
        {
            float price = t.Quantity * t.Product.Price;
            MessageBox.Show("final cost calc" + price);

            return price; 
        }

        private void customerForm_Load(object sender, EventArgs e)
        {
            string userName = ctx.Customers.Where(c => c.ID == id).Select(c => c.Username).Single();
            label1.Text = "Welcome " + userName + " select the product you want for your order :";
            ProductListBox.DataSource = ctx.Products.Select(p => p.Name).ToList();
            if(ctx.Orders.Where(o => o.Customer.ID == id).Count() != 0)
                OrdersListBox.DataSource = ctx.Orders.Where(o => o.Customer.ID == id).Select(o => o.ID).ToList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = "Add Qantity for " + ProductListBox.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int productID = ctx.Products.Where(p => p.Name == ProductListBox.SelectedItem.ToString()).Select(p => p .ID).Single();
            int quantity = Convert.ToInt32(QuantityTextBox.Text);
            Product p = ctx.Products.Where(p => p.ID == productID).Single();
            //check if there is enough quantity
            int availableQ = p.InventoryLevel;
            if (quantity <= availableQ)
            {
                TransactionItem TransactionI = new TransactionItem()
                {
                    ProductID = productID,
                    Quantity = quantity,
                    CostPerItem = p.Price,
                    Order = newOrder
                };
                p.InventoryLevel -= quantity;
                // update data in transation and order lists
                ctx.TransactionItems.Add(TransactionI);
                newOrder.AddItem(TransactionI);
                newOrder.Final_Cost += cal_cost(TransactionI);
                ctx.SaveChanges();
                //update data in list box
                TransactionitemsListBox.DataSource = null;
                TransactionitemsListBox.DataSource = ctx.TransactionItems.Where(t => t.Order.Customer.ID == id).Select(t => t.ID).ToList();
            }
            else
                MessageBox.Show($"There are no enough items, the available items are {availableQ}");
            QuantityTextBox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add new order
            Customer c = ctx.Customers.Where(c1 => c1.ID == id).Single();
            newOrder = new Order()
            {
                Status = OrderStatus.InProgress,
                Date = DateTime.Now,
                Customer = c                
            };
            newOrder.Final_Cost = 0;
            ctx.Orders.Add(newOrder);
            ctx.SaveChanges();
            MessageBox.Show("Your Order is created succesfully NOW you can add transaction Items");
            //update order list box 
            OrdersListBox.DataSource = null;
            OrdersListBox.DataSource = ctx.Orders.Where(o=> o.Customer.ID == id).Select(o => o.ID).ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void QuantityTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //update the quantity
            if (QuantityTextBox.Text == "")
            {
                MessageBox.Show("please add the new quantity in the quantity box : ");
            }
            else
            {
                TransactionItem transAction = ctx.TransactionItems.Where(t => t.ID == Convert.ToInt32(TransactionitemsListBox.SelectedItem.ToString())).Single();
                //MessageBox.Show("");
                int quantity = Convert.ToInt32(QuantityTextBox.Text);
                Product p = ctx.Products.Where(p => p.ID == transAction.ProductID).Single();
                //check if there is enough quantity
                p.InventoryLevel += transAction.Quantity;
                int availableQ = p.InventoryLevel;
                if (quantity <= availableQ)
                {
                    transAction.Order.Final_Cost -= cal_cost(transAction);
                    transAction.Quantity = quantity;
                    p.InventoryLevel -= availableQ;
                    transAction.Order.Final_Cost += cal_cost(transAction);
                    ctx.SaveChanges();
                }
                else
                {
                    MessageBox.Show($"There are no enough items, the available items are {availableQ}");
                }
            }
        }

            private void button5_Click(object sender, EventArgs e)
        {
            //delete transaction item 
            TransactionItem transAction = ctx.TransactionItems.Where(T => T.ID == Convert.ToInt32(TransactionitemsListBox.SelectedItem.ToString())).Single();
            ctx.TransactionItems.Remove(transAction);
            ctx.SaveChanges();
            //update data in box
            TransactionitemsListBox.DataSource = null;
            TransactionitemsListBox.DataSource = ctx.TransactionItems.Where(t => t.Order.Customer.ID == id).Select(t => t.ID).ToList();
        }

        private void OrdersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransactionitemsListBox.DataSource = ctx.TransactionItems.Where(t => t.Order.ID == Convert.ToInt32(OrdersListBox.SelectedItem.ToString())).Select(t => t.ID).ToList();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            newOrder.Status = OrderStatus.Pending;
            ctx.SaveChanges();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}