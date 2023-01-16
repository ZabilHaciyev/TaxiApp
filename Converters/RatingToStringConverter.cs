using TaxiApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TaxiApp.Converters
{
    class RatingToStringConverter : IValueConverter
    {
        public Rating rating { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rating = value as Rating?;

            return rating.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value.ToString();
            rating = new Rating();
            switch (type)
            {
                case "Angry": rating = Rating.Angry; break;
                case "Upset": rating = Rating.Upset; break;
                case "Neutral": rating = Rating.Neutral; break;
                case "Happy": rating = Rating.Happy; break;
                case "Excited": rating = Rating.Excited; break;
                default: rating = Rating.None; break;
            }
            return rating;
        }
    }
}
