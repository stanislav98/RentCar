using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRent
{
    public partial class Vhod : Form
    {
        public Vhod()
        {
            InitializeComponent();
        }
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 main = new Form1();
            main.Closed += (s, args) => this.Close();
            main.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //check for email first
            Regex regex = new Regex(pattern);
            Match match = regex.Match(textBox2.Text);
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please fill all the inputs to continue");
            }
            else if (match.Success)
            {
                Regex phone_regex = new Regex(@"^[0-9]+$");
                Match phone_match = phone_regex.Match(textBox1.Text);
                if (phone_match.Success && textBox1.Text.Length == 10)
                {
                    Database db = new Database();
                    int hasClients = db.SelectClients(textBox2.Text, textBox1.Text);
                    if (hasClients > 0)
                    {
                        Client mainClient = db.GetClientAfterLog(textBox2.Text, textBox1.Text);
                        this.Hide();
                        ListBrands form = new ListBrands(mainClient);
                        form.Closed += (s, args) => this.Close();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Не съществува такъв клиент.Моля проверете отново въведените данни.");
                    }
                }
                else {
                    MessageBox.Show("Моля попълнете коректни данни.");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
