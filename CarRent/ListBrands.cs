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
    public partial class ListBrands : Form
    {
        Client mainClient;
        public ListBrands(Client client)
        {
            InitializeComponent();
            mainClient = client;
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void X_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ListBrands_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            int top = 300;
            int left = 20;
            int labelLeft = 30;
            int labelTop = 120;
            int panelLabelTop = 350;
            List<Button> buttons = new List<Button>();
            List<PictureBox> pictures = new List<PictureBox>();
            List<Label> labels = new List<Label>();
            Database db = new Database();
            List<Brands> res = db.getBrands();
            int i = 0;
            foreach (var br in res) {
                Label lab = new Label();
                lab.Size = new Size(500, 20);
                lab.Text =  br.name;
                lab.Location = new Point(15, panelLabelTop);
                lab.Font = new Font(lab.Font.FontFamily, 12, FontStyle.Regular);
                lab.ForeColor = Color.AntiqueWhite;
                labels.Add(lab);
                panel1.Controls.Add(lab);
                panelLabelTop += 25;
            }
            foreach (var listBoxItem in res)
            {
                if (i % 2 != 0)
                {
                    left += 650;
                    labelLeft += 650;
                }
                if (i % 2 == 0 && i != 0)
                {
                    top += 310; left -= 650;
                    labelTop += 310; labelLeft -= 650;
                }
                Label lab = new Label();
                lab.Size = new Size(500, 30);
                lab.Text = "Каталог : " + listBoxItem.name;
                lab.Location = new Point(labelLeft, labelTop);
                lab.Font = new Font(lab.Font.FontFamily, 12, FontStyle.Bold);
                labels.Add(lab);
                panel3.Controls.Add(lab);

                PictureBox pic = new PictureBox();
                pic.Size = new Size(500, 100);
                pic.Location = new Point(labelLeft, labelTop + 50);
                pic.Image = Image.FromFile(listBoxItem.image);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pictures.Add(pic);
                panel3.Controls.Add(pic);


                Button newButton = new Button();
                newButton.Name = listBoxItem.brand_id.ToString();
                newButton.Size = new Size(500, 50);
                newButton.Text = "Разгледайте автомобилите";
                newButton.FlatStyle = FlatStyle.Flat;
                newButton.BackColor = Color.FromArgb(41, 39, 40);
                newButton.ForeColor = Color.GhostWhite;
                newButton.Font = new Font(lab.Font.FontFamily, 10);
                newButton.Location = new Point(left, top);
                newButton.Click += new EventHandler(button_Click);
                buttons.Add(newButton);
                panel3.Controls.Add(newButton);
                i++;

            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Database db = new Database();
            List<Cars> res = db.getCars(Int32.Parse(button.Name));
            this.Hide();
            ListModels order = new ListModels(mainClient, res);
            order.Closed += (s, args) => this.Close();
            order.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Search main = new Search(mainClient);
            main.Closed += (s, args) => this.Close();
            main.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
