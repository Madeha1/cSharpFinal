using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Part2_Project
{
    public partial class Admin : Form
    {
        Order orders = new Order();
        CompCtx ctx = new CompCtx();
        public Admin()
        {
            InitializeComponent();
            List<string> orders = new List<string>();
            foreach (Order o in ctx.Orders.ToList())
            {
                string s = o.PrintShoppingBag();
                orders.Add(s);
            }
            ReportlistBox.DataSource = orders;
            //ReportlistBox.DataSource = ctx.Orders.Select(o => o.ID).ToList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = ReportlistBox.SelectedIndex;
            Order order = ctx.Orders.Where(o => o.ID == i + 1).Single();
            if (order.Status == OrderStatus.InProgress)
                MessageBox.Show("You can't deliver the current order it still in progress");
            else
            {
                order.Status = OrderStatus.Delivered;
                MessageBox.Show("The order is delivered");
                ctx.SaveChanges();
                ReportlistBox.DataSource = null;
                ReportlistBox.DataSource = ctx.Orders.Select(o => o.PrintShoppingBag()).ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product product = new Product()
            {
                Name = NameTextBox.Text,
                InventoryLevel = Convert.ToInt32(LevelTextBox.Text),
                Price = Convert.ToInt32(PriceTextBox.Text)
            };
            ctx.Products.Add(product);
            ctx.SaveChanges();
            NameTextBox.Text = "";
            LevelTextBox.Text = "";
            PriceTextBox.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = ctx.Products.Where(p => p.InventoryLevel == 0).Select(p => p.Name).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            float max = ctx.Orders.Select(o => o.Final_Cost).ToList().Max();
            listBox1.DataSource = ctx.Orders.Where(o => o.Final_Cost == max).Select(o => o.Customer.Username).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("number of company users is : "+ctx.Customers.OfType<Company>().Count());
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
    }
}
