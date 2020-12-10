using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part2_Project
{
    public partial class LogIn : Form
    {
        public int id;
        public CompCtx ctx = new CompCtx();

        private Admin admin;
        //private LogIn logIn;
        private customerForm customerform;
        private SignUp sign;
        public LogIn()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void clear()
        {
            UserName.Text = "";
            PasswordTextBox.Text = "";
        }
        public bool correctPass(string name , string pass)
        {
            Customer customer  = new Customer();
            if (ctx.Customers.Count(n => n.Username == name) ==  0)
                MessageBox.Show("Invalid user name!");
            else
            {
                customer = ctx.Customers.Where(c => c.Username == name).Single();
                if (customer.Password == pass)
                {
                    id = customer.ID;
                    return true;
                }
                else
                    MessageBox.Show("In correct password!");
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (UserName.Text == "admin" && PasswordTextBox.Text == "admin") {
                admin = new Admin();
                admin.Show();
            } else {
                if (correctPass(UserName.Text, PasswordTextBox.Text)) {//if the password is correct
                    customerform = new customerForm(id);
                    customerform.Show();
                }
            }
            clear();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sign = new SignUp();
            sign.Show();
        }
    }
}
