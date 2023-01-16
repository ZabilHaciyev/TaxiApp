using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApp.Services
{
    [AddINotifyPropertyChangedInterface]

    public class PolylineService
    {
        Location location1 = new Location();
        Location location2 = new Location();
        public PolylineService(Location whereFrom, Location whereTo)
        {
            location1 = whereFrom;
            location2 = whereTo;
        }

        public int Distance { get; set; }
        public double PriceForDrive { get; set; }
        public double PriceFor1km = 1.5;

        public ObservableCollection<MyPolyLine> MyPolyLines { get; set; } = new ObservableCollection<MyPolyLine>();
        public string  GetPolyLineStr()
        {
            HttpClient client = new HttpClient();

            string longitude1 = location1.Longitude.ToString();
            string latitude1 = location1.Latitude.ToString();
            string longitude2 = location2.Longitude.ToString();
            string latitude2 = location2.Latitude.ToString();

            //longitude1 = longitude1.Replace(",", ".");
            //latitude1 = latitude1.Replace(",", ".");
            //longitude2 = longitude2.Replace(",", ".");
            //latitude2 = latitude2.Replace(",", ".");



            var link = $"https://api.mapbox.com/directions/v5/mapbox/driving/{longitude1},{latitude1};{longitude2},{latitude2}?geometries=geojson&access_token=pk.eyJ1IjoicmFzaGlkYWxpeWV2IiwiYSI6ImNrOXZwdDRrNjAxaDIzaHM2NDF0Mzg5bmYifQ.OfH9UV_0ZD7edb3wPviZtQ";

            dynamic coordinates = JsonConvert.DeserializeObject(client.GetAsync(link).Result.Content.ReadAsStringAsync().Result);

            try
            {
                foreach (var item in coordinates.routes)
                {
                    Distance = item["distance"] / 1000;
                    PriceForDrive = Distance * PriceFor1km;
                    foreach (var items in item.geometry)
                    {
                        foreach (var itemss in items)
                        {
                            foreach (var itemsss in itemss)
                            {

                                string longitude = itemsss[0];
                                string latitude = itemsss[1];

                                longitude = longitude.Replace(".", ",");
                                latitude = latitude.Replace(".", ",");
                                var log = double.Parse(longitude);
                                var lat = double.Parse(latitude);
                                Location loc = new Location(lat, log);

                                var p = new MyPolyLine()
                                {
                                    Location = loc
                                };
                                MyPolyLines.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < MyPolyLines.Count; i++)
            {
                string Lat = (MyPolyLines[i].Location.Latitude.ToString()).Insert(2, ".");
                string Long = (MyPolyLines[i].Location.Longitude.ToString()).Insert(2, ".");

                //string Lat = (PolyLineCoordinates[i].Location.Latitude.ToString());
                //string Long = (PolyLineCoordinates[i].Location.Longitude.ToString());
                //Lat = Lat.Replace(",", ".");
                // Long = Long.Replace(",", ".");

                str.Append(Lat);
                str.Append(",");
                str.Append(Long);
                str.Append(" ");
            }

            return  str.ToString();

        }

    }
}
