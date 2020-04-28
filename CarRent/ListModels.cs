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
    public partial class ListModels : Form
    {
        Client mainClient;
        List<Cars> cars;
        public ListModels(Client cl,List<Cars>res)
        {
            InitializeComponent();
            mainClient = cl;
            cars = res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Search main = new Search(mainClient);
            main.Closed += (s, args) => this.Close();
            main.Show();
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ListModels_Load(object sender, EventArgs e)
        {
            
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            int top = 400;
            int left = 20;
            int labelLeft = 30;
            int labelTop = 120;
            int panelLabelTop = 350;
            List<Button> buttons = new List<Button>();
            List<PictureBox> pictures = new List<PictureBox>();
            List<Label> labels = new List<Label>();
            Database db = new Database();
            int i = 0;
            int extraKey = 0;
            if (!cars.Any())
            {
                Label lab = new Label();
                lab.Size = new Size(750, 180);
                lab.Text = "Съжаляваме ,но не открихме резултати за вашето търсене.";
                lab.Location = new Point(250, 150);
                lab.Font = new Font(lab.Font.FontFamily, 14, FontStyle.Bold);
                labels.Add(lab);
                panel3.Controls.Add(lab);
            }
            else
            {
                foreach (var br in cars)
                {
                    Label lab = new Label();
                    lab.Size = new Size(500, 20);
                    lab.Text = db.getBrandName(br.id_brand) + "" + db.getModelName(br.model_id);
                    lab.Location = new Point(15, panelLabelTop);
                    lab.Font = new Font(lab.Font.FontFamily, 12, FontStyle.Regular);
                    lab.ForeColor = Color.AntiqueWhite;
                    labels.Add(lab);
                    panel1.Controls.Add(lab);
                    panelLabelTop += 25;
                }
                foreach (var listBoxItem in cars)
                {
                    if (i % 2 != 0)
                    {
                        left += 650;
                        labelLeft += 650;
                    }
                    if (i % 2 == 0 && i != 0)
                    {
                        top += 410; left -= 650;
                        labelTop += 410; labelLeft -= 650;
                    }
                    Label lab = new Label();
                    lab.Size = new Size(500, 180);
                    string label = "Модел : " + db.getBrandName(listBoxItem.id_brand) + "" + db.getModelName(listBoxItem.model_id) + "\nГодина на производство : " + listBoxItem.year + " - " + listBoxItem.kmTraveled + "км. \n" +
                    "Цвят : " + db.getColor(listBoxItem.color) + "\n" + "Екстри : ";
                    foreach (var extra in db.getExtras(listBoxItem.extras_list))
                    {
                        if (extraKey % 2 == 0)
                        {
                            label += extra + ",";
                        }
                        else
                        {
                            label += extra + ",";
                        }
                        extraKey++;
                    }
                    label = label.Remove(label.Length - 1);
                    label += "\nЦена на ден : " + listBoxItem.price + "лв.";
                    label = label.Replace("\t", " ");
                    while (label.IndexOf("  ") >= 0)
                    {
                        label = label.Replace("  ", " ");
                    }
                    lab.Text = label;
                    lab.Location = new Point(labelLeft, labelTop);
                    lab.Font = new Font(lab.Font.FontFamily, 12, FontStyle.Bold);
                    labels.Add(lab);
                    panel3.Controls.Add(lab);

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(500, 100);
                    pic.Location = new Point(labelLeft, labelTop + 170);
                    pic.Image = Image.FromFile(listBoxItem.imagePath);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pictures.Add(pic);
                    panel3.Controls.Add(pic);


                    Button newButton = new Button();
                    newButton.Name = listBoxItem.id_car.ToString();
                    newButton.Size = new Size(500, 50);
                    newButton.Text = "Наеми";
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
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Database db = new Database();
            Cars mainCar = db.getCarById(Int32.Parse(button.Name));
            if (mainCar.rented == 0)
            {
                if (mainClient.money >= mainCar.price)
                {
                    decimal newmoney = mainClient.money - mainCar.price;
                    db.updateMoney(mainClient.email, newmoney);
                    mainClient.money = newmoney;
                    db.setRent(mainCar.id_car);
                    MessageBox.Show("Вие успешно наехте колата.Благорим ви!");
                }
                else
                {
                    MessageBox.Show("Съжаляваме но нямате достатъчно пари във вас!Моля изберете си друга кола");
                }
            }
            else
            {
                MessageBox.Show("Колата вече е наета.Моля изберете си друга.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListBrands main = new ListBrands(mainClient);
            main.Closed += (s, args) => this.Close();
            main.Show();
        }
    }
}
