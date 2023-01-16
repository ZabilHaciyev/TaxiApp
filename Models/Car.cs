using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApp.Model
{
    public class Car
    {
        public string Vendor { get; set; }  
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public string Image { get; set; }

        public override string ToString()
        {
            return $"{Vendor} {Model} {Year}";
        }
    }
}
