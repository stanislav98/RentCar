using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRent
{
    class Database
    {
        public SqlConnection mydb { set; get; }
        public string strConnectionString = @"Server= DESKTOP-EK4FKT1\STOICHEVSQL ; Database = RentCar ; Trusted_Connection = True ";
        string[] cats_name;
        string names;
        //vmukvane na klient
        public void InsertClient(String name, String address, String phone, String email, decimal money)
        {
            int count = SelectClients(email, phone);
            mydb = new SqlConnection(strConnectionString);
            string query = "INSERT INTO Client (Name,Money,Phone,email,Address) VALUES (@Name,@Money,@Phone,@email,@Address)";
            //first check if client exists
            if (count == 0)
            {
                using (SqlCommand command = new SqlCommand(query, mydb))
                {
                    command.Parameters.AddWithValue("@Name", SqlDbType.NChar).Value = name;
                    command.Parameters.AddWithValue("@Money", SqlDbType.Money).Value = money;
                    command.Parameters.AddWithValue("@Phone", SqlDbType.Int).Value = phone;
                    command.Parameters.AddWithValue("@email", SqlDbType.NChar).Value = email;
                    command.Parameters.AddWithValue("@Address", SqlDbType.NChar).Value = address;
                    mydb.Open();
                    command.ExecuteNonQuery();
                    mydb.Close();
                }
            }
            else
            {
                MessageBox.Show("Вече съществува такъв потребител.Моля върнете се в главното меню");
            }
        }
        public int SelectClients(String email, String phone)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select Count(*) From [Client] where email = @email and Phone = @phone", mydb))
            {
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@email", email);
                int count = Convert.ToInt32(command.ExecuteScalar());
                mydb.Close();
                return count;
            }
        }
        public Client GetClientAfterLog(String email, String phone)
        {
            mydb = new SqlConnection(strConnectionString);
            Client client = (Client)FormatterServices.GetUninitializedObject(typeof(Client));
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Client] where Email= @email and Phone = @phone", mydb))
            {
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@email", email);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        client.name = reader.GetString(1);
                        client.money = Math.Round(reader.GetDecimal(2), 2);
                        client.phone = reader.GetString(3);
                        client.email = reader.GetString(4);
                        client.address = reader.GetString(5);
                    }
                }
            }
            mydb.Close();
            return client;
        }

        public List<Brands> getBrands()
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            var list = new List<Brands>();
            using (SqlCommand command = new SqlCommand("Select * From [Brands]", mydb))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Brands(reader.GetInt32(0),reader.GetString(1), reader.GetString(2), reader.GetString(3)));

                    }
                }
            }
            mydb.Close();
            return list;
        }
        public List<Cars> getCars(int id_brand)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            var list = new List<Cars>();
            using (SqlCommand command = new SqlCommand("Select * From [Cars] where ID_Brand = @id_brand", mydb))
            {
                command.Parameters.AddWithValue("@id_brand", id_brand);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Cars(reader.GetInt32(0),reader.GetInt32(1),reader.GetString(2),reader.GetInt32(3),reader.GetInt32(4),reader.GetInt32(5),reader.GetInt32(6),reader.GetString(7),reader.GetString(8),reader.GetDecimal(9),reader.GetInt32(10)));

                    }
                }
            }
            mydb.Close();
            return list;
        }

        string color;
        public String getColor(int color_id)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Colors] where ID_Color = @color_id", mydb))
            {
                command.Parameters.AddWithValue("@color_id", color_id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        color = reader.GetString(1);
                    }
                }
            }
            mydb.Close();
            return color;
        }
        int colorID;
        public int getColorID(string color_name)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Colors] where Name = @color_name", mydb))
            {
                command.Parameters.AddWithValue("@color_name", color_name);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        colorID = reader.GetInt32(0);
                    }
                }
            }
            mydb.Close();
            return colorID;
        }


        string brandName;
        public String getBrandName(int brand_id)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Brands] where ID_Brand = @id_brand", mydb))
            {
                command.Parameters.AddWithValue("@id_brand", brand_id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        brandName = reader.GetString(1);
                    }
                }
            }
            mydb.Close();
            return brandName;
        }
        int brandID;
        public int getBrandID(string brand_name)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Brands] where Name = @brand_name", mydb))
            {
                command.Parameters.AddWithValue("@brand_name", brand_name);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        brandID = reader.GetInt32(0);
                    }
                }
            }
            mydb.Close();
            return brandID;
        }


        string model;
        public String getModelName(int model_id)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Model] where ID_Model = @id_model", mydb))
            {
                command.Parameters.AddWithValue("@id_model", model_id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model = reader.GetString(1);
                    }
                }
            }
            mydb.Close();
            return model;
        }

        int modelID;
        public int getModelID(string modelName)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Model] where Name = @modelName", mydb))
            {
                command.Parameters.AddWithValue("@modelName", modelName);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        modelID = reader.GetInt32(0);
                    }
                }
            }
            mydb.Close();
            return modelID;
        }

        public List<String> getExtras(string extras)
        {
            mydb = new SqlConnection(strConnectionString);
            for (int i = 0; i < extras.Length; i++)
            {
                cats_name = extras.Split(',').Select(a => a.Trim()).ToArray();
            }
            mydb.Open();
            var extrasList = new List<String>();
            foreach (string s in cats_name)
            {
                using (SqlCommand command = new SqlCommand("Select * From Extras Where Extra_id = @" + s, mydb))
                {
                    command.Parameters.AddWithValue("@" + s, s);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            extrasList.Add(reader.GetString(1));
                        }
                    }
                }
            }
            mydb.Close();
            return extrasList;
        }

        public List<int> getExtrasID(string extras)
        {
            mydb = new SqlConnection(strConnectionString);
            for (int i = 0; i < extras.Length; i++)
            {
                cats_name = extras.Split(',').Select(a => a.Trim()).ToArray();
            }
            mydb.Open();
            var extrasListID = new List<int>();
            foreach (string s in cats_name)
            {
                using (SqlCommand command = new SqlCommand("Select * From Extras Where Extra_name=@s", mydb))
                {
                    command.Parameters.AddWithValue("@s", s);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            extrasListID.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            mydb.Close();
            return extrasListID;
        }

        public void setRent(int car_id)
        {
            mydb = new SqlConnection(strConnectionString);
            string query = "Update Cars Set Rented=@rented Where ID_Car = @car_id";
            using (SqlCommand command = new SqlCommand(query, mydb))
            {
                command.Parameters.AddWithValue("@rented", SqlDbType.Int).Value = 1;
                command.Parameters.AddWithValue("@car_id", SqlDbType.Int).Value = car_id;
                mydb.Open();
                command.ExecuteNonQuery();
                mydb.Close();
            }
        }
        public void updateMoney(string email,decimal newmoney)
        {
            mydb = new SqlConnection(strConnectionString);
            string query = "Update Client Set Money=@newmoney Where email = @email";
            using (SqlCommand command = new SqlCommand(query, mydb))
            {
                command.Parameters.AddWithValue("@newmoney", SqlDbType.Decimal).Value = newmoney;
                command.Parameters.AddWithValue("@email", SqlDbType.NVarChar).Value = email;
                mydb.Open();
                command.ExecuteNonQuery();
                mydb.Close();
            }
        }

        Cars mainCar;
        public Cars getCarById(int car_id)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            using (SqlCommand command = new SqlCommand("Select * From [Cars] where ID_Car = @car_id", mydb))
            {
                command.Parameters.AddWithValue("@car_id", car_id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mainCar = new Cars(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetString(7), reader.GetString(8), reader.GetDecimal(9), reader.GetInt32(10));
                    }
                }
            }
            mydb.Close();
            return mainCar;
        }

        public void FillCheckedListBoxPrice(ref ComboBox chl)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select * from [Cars]", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                chl.Items.Add(dr[9].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }
        public void FillCheckedListBoxExtras(ref CheckedListBox chl)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select * from [Extras]", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                chl.Items.Add(dr[1].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }
        
        public void fillBrandsIntoCB(ref ComboBox cb)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select * from [Brands]", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                cb.Items.Add(dr[1].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }

        public void fillColorsCB(ref ComboBox cb)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select * from [Colors]", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                cb.Items.Add(dr[1].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }

        public void fillYearCB(ref ComboBox cb)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select DISTINCT Year from [Cars]", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                cb.Items.Add(dr[0].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }

        public void fillKMCB(ref ComboBox cb)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select DISTINCT KmTraveled from [Cars]", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                    cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }

        public void fillModelsIntoCB(ref ComboBox cb,string brand_name)
        {
            cb.Items.Clear();
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            Regex.Replace(brand_name, @"\s+", "");
            SqlCommand cmd = new SqlCommand("Select * from [Brands] Where Name=@brand_name", mydb);
            cmd.Parameters.AddWithValue("@brand_name", SqlDbType.NVarChar).Value = brand_name;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                    cats_name = dr[2].ToString().Split(',').Select(a => a.Trim()).ToArray();
                    foreach(var name in cats_name) {
                        String model = getModelName(Int32.Parse(name));
                        cb.Items.Add(model);
                    }
            }
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }

        public List<Cars> getFilteredCars(int id_brand,string extras,decimal price,int y,int c,int k)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            var list = new List<Cars>();
            using (SqlCommand command = new SqlCommand("Select * From [Cars] where ID_Brand = @id_brand AND Price <= @price AND Extras_list LIKE " +
                "\'%" + extras + "%\' AND ID_Color=@c AND Year >= @y AND KmTraveled >= @k", mydb))
            {
                MessageBox.Show("Select * From [Cars] where ID_Brand = " + id_brand + " AND Price <=  " + price + "  AND Extras_list LIKE " +
                "\'%" + extras + "%\' AND ID_Color=  " + c + "  AND Year >=  " + y + "  AND KmTraveled >=  " + k + " ");
                command.Parameters.AddWithValue("@id_brand", id_brand);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@c", c);
                command.Parameters.AddWithValue("@k", k);
                command.Parameters.AddWithValue("@y", y);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Cars(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetString(7), reader.GetString(8), reader.GetDecimal(9), reader.GetInt32(10)));
                    }
                }
            }
            mydb.Close();
            return list;
        }





    }
}
