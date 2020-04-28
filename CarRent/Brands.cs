using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class Brands
    {
        public int brand_id { get; set; }
        public string name { get; set; }
        public string model_id { get; set; }
        public string image { get; set; }
        public Brands(string n)
        {
            name = n;
        }
        //@override
        public Brands(int brand,string n,string model,string img)
        {
            brand_id = brand;
            name = n;
            model_id = model;
            image = img;
        }
    }
}
