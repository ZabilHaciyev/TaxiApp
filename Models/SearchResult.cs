using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApp.Models
{
    public class SearchResult 
    {
        public Location Location { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public double Rating { get; set; }

        public SearchResult()
        {

        }
    }
}
