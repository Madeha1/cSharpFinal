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
    public partial class SignUp : Form
    {
        public CompCtx ctx = new CompCtx();
        Customer customer;
        public SignUp()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (UserRadioButton.Checked) {
                panel2.Visible = true;
                panel3.Visible = false;
            }

        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CompanyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CompanyRadioButton.Checked)
            {
                panel2.Visible = false;
                panel3.Visible = true;
            }
          
        }
        public char sexType()
        {
            if(FemaleRadioButton.Checked)
                return 'F';
            return 'M';
        } 
        private void SignUpButton_Click(object sender, EventArgs e)
        {

            if (UserRadioButton.Checked)//user
            {
                customer = new User();
                customer.Username = UserNameTextBox.Text;
                customer.Password = PasswordTextBox.Text;
                ((User)customer).Sex = sexType();
                ctx.Customers.Add(customer);
                ctx.SaveChanges();
            } else {
                customer = new Company();
                customer.Username = UserNameTextBox.Text;
                customer.Password = PasswordTextBox.Text;
                ((Company)customer).CompanyType = TypeTextBox.Text;
                ((Company)customer).Location = LocationTextBox.Text;
                ctx.Customers.Add(customer);
                ctx.SaveChanges();
            }
            MessageBox.Show($"You Have an Acount Now {customer.Username} you can login");
            this.Close();
        }
    }
}
