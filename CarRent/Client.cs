using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Client
    {
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public decimal money { get; set; }
        public Client(string n, string addr, string ph, string em, decimal m)
        {
            name = n;
            address = addr;
            phone = ph;
            email = em;
            money = m;
        }
    }
}
