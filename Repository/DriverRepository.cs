using Microsoft.Maps.MapControl.WPF;
using System;

namespace TaxiApp.Repository
{
    public class DriverRepository
    {
        static Random rand = new Random();
        public Location GetRandomLocation()
        {
            //40.409264, 49.86709

            int minlat = 350000;
            int maxlat = 409264;

            int minlong = 800000;
            int maxlong = 867092;


            double lat;
            double longt;

            Double.TryParse(rand.Next(minlat, maxlat).ToString(), out lat);
            Double.TryParse(rand.Next(minlong, maxlong).ToString(), out longt);

            lat = lat / 1000000 + 40;
            longt = longt / 1000000 + 49;

            Location Location = new Location(lat, longt);
            return Location;
        }
    }
}
