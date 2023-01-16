using Microsoft.Maps.MapControl.WPF;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApp.Model
{
    [AddINotifyPropertyChangedInterface]

    public class History
    {
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string Driverİmage { get; set; }
        public string DriverName { get; set; }
        public string TripDate { get; set; }
        public double Cash { get; set; }
    }
}
