using Microsoft.Maps.MapControl.WPF;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PropertyChanged;
using TaxiApp.Models.Abstract;

namespace TaxiApp.Model
{
    public enum Rating
    {
        None,
        Angry,
        Upset,
        Neutral,
        Happy,
        Excited
    }
    [AddINotifyPropertyChangedInterface]
    public class Driver:Entity
    {
    
        public string Visibility { get; set; } = "Visible";
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Location Location { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public Rating Rating { get; set; }
        public string Image { get; set; }
        public Car Car { get; set; }

        public override string ToString()
        {
            return $"{Firstname} {Lastname}";
        }

    }
}
