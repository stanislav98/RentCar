using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Cars
    {
        public int id_car;
        public int id_brand;
        public string numberPlate;
        public int year;
        public int color;
        public int kmTraveled;
        public int rented;
        public string imagePath;
        public string extras_list;
        public decimal price;
        public int model_id;

        public Cars(int id_c, int id_b, string number, int y, int c, int km, int rent, string path, string extras, decimal p,int model) {
            id_car = id_c;
            id_brand = id_b;
            numberPlate = number;
            year = y;
            color = c;
            kmTraveled = km;
            rented = rent;
            imagePath = path;
            extras_list = extras;
            price = p;
            model_id = model;
        }


    }
}
