using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApp
{
    [AddINotifyPropertyChangedInterface]
    public class MyPolyLine
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public int Distance { get; set; }
        public string cc { get; set; }
        public string Country { get; set; }

    } 


}
