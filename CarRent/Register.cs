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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        Client client;
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 main = new Form1();
            main.Closed += (s, args) => this.Close();
            main.Show();
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check for email first
            Regex regex = new Regex(pattern);
            Match match = regex.Match(textBox4.Text);
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) ||
                String.IsNullOrEmpty(textBox4.Text) || String.IsNullOrEmpty(textBox5.Text)
                )
            {
                MessageBox.Show("Моля попълнете всички полета за да продължите");
            }
            else
            {
                if (match.Success)
                {
                    //check for phone number 
                    Regex phone_regex = new Regex(@"^[0-9]+$");
                    Match phone_match = phone_regex.Match(textBox3.Text);
                    if (phone_match.Success && textBox3.Text.Length == 10)
                    {
                        if (IsNumeric(textBox5.Text) || Int32.TryParse(textBox5.Text, out int val))
                        {
                            decimal dec = Math.Round(Decimal.Parse(textBox5.Text), 2);
                            this.Hide();
                            client = new Client(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, dec);
                            Database db = new Database();
                            db.InsertClient(client.name, client.address, client.phone, client.email, client.money);
                            var form = new ListBrands(client);
                            form.Closed += (s, args) => this.Close();
                            form.Show();
                        }
                        else
                        {
                            MessageBox.Show("Моля въведете правилно полето за налични пари.\n" + "Пример : 123,23 / 123");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Моля въведете правилно полето за телефонен номер");
                    }
                }
                else
                {
                    MessageBox.Show("Моля въведете правилно полето за Имейл");
                }
            }
        }
        private bool IsNumeric(string s) => decimal.TryParse(s, out var value);

    }
}
