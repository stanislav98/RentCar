using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRent
{
    public partial class Search : Form
    {
        Client mainClient;
        public Search(Client cl)
        {
            InitializeComponent();
            mainClient = cl;
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            db.fillBrandsIntoCB(ref comboBox1);
            db.FillCheckedListBoxExtras(ref checkedListBox1);
            db.FillCheckedListBoxPrice(ref comboBox2);
            db.fillColorsCB(ref comboBox3);
            db.fillYearCB(ref comboBox4);
            db.fillKMCB(ref comboBox5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1 && checkedListBox1.CheckedIndices.Count > 0 && comboBox2.SelectedIndex > -1
                && comboBox3.SelectedIndex > -1 && comboBox4.SelectedIndex > -1 && comboBox5.SelectedIndex > -1)
            {
                Database db = new Database();
                String extra_names = "";
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    extra_names+=item.ToString()+",";
                }
                extra_names = extra_names.Remove(extra_names.Length - 1);
                extra_names = extra_names.Replace("\t", " ");
                while (extra_names.IndexOf("  ") >= 0)
                {
                    extra_names = extra_names.Replace("  ", " ");
                }
                int brandID = db.getBrandID(comboBox1.Text);
                decimal mainPrice = Decimal.Parse(comboBox2.Text);
                int year = Int32.Parse(comboBox4.Text);
                int colorID = db.getColorID(comboBox3.Text);
                int km = Int32.Parse(comboBox5.Text);
                List<int>extra_ids = db.getExtrasID(extra_names);
                String extras_query = "";
                foreach(var extr in extra_ids) {
                    extras_query += extr.ToString()+",";
                }
                extras_query = extras_query.Remove(extras_query.Length - 1);
                List<Cars> res = db.getFilteredCars(brandID, extras_query, mainPrice,year,colorID,km);
                this.Hide();
                ListModels order = new ListModels(mainClient, res);
                order.Closed += (s, args) => this.Close();
                order.Show();
            }
            else
            {
                MessageBox.Show("Моля попълнете всички критерии");
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
