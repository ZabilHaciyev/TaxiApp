using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaxiApp.View
{
    /// <summary>
    /// Interaction logic for RatingControl.xaml
    /// </summary>
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

    public partial class RatingControl : UserControl
    {
        public Rating Rating { get; set; } = Rating.None;

        public RatingControl()
        {
         
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            var btn = sender as Button;
            if (btn.Name == "s1")
            {
                if (s1star.Source.ToString() == "pack://application:,,,/dstar.png")
                {
                    ImageSource source = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s1star.Source = source;
                    ratetext.Foreground = Brushes.Red;
                    ratetext.Content = "Angry";
                    Rating = Rating.Angry;
                }
                else
                {
                    Rating = Rating.None;
                }
            }
            else if (btn.Name == "s2")
            {

                if (s2star.Source.ToString() == "pack://application:,,,/dstar.png")
                {
                    ImageSource source = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s1star.Source = source;

                    ImageSource source2 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s2star.Source = source2;
                    ratetext.Foreground = Brushes.LightYellow;
                    ratetext.Content = "Upset";

                    Rating = Rating.Upset;
                }
                else
                {
                    ImageSource source2 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s2star.Source = source2;

                    ImageSource source3 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s3star.Source = source3;

                    ImageSource source4 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s4star.Source = source4;

                    ImageSource source5 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s5star.Source = source5;

                    ratetext.Foreground = Brushes.Red;
                    ratetext.Content = "Angry";

                    Rating = Rating.Angry;
                }

            }
            else if (btn.Name == "s3")
            {
                ImageSource source1 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                s1star.Source = source1;

                ImageSource source2 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                s2star.Source = source2;

                if (s3star.Source.ToString() == "pack://application:,,,/dstar.png")
                {

                    ImageSource source3 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s3star.Source = source3;

                    ratetext.Foreground = Brushes.LightGreen;
                    ratetext.Content = "Neutral";

                    Rating = Rating.Neutral;
                }
                else
                {
                    ImageSource source3 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s3star.Source = source3;

                    ImageSource source4 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s4star.Source = source4;

                    ImageSource source5 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s5star.Source = source5;

                    ratetext.Foreground = Brushes.LightYellow;
                    ratetext.Content = "Upset";

                    Rating = Rating.Upset;
                }

            }
            else if (btn.Name == "s4")
            {
                if (s4star.Source.ToString() == "pack://application:,,,/dstar.png")
                {
                    ImageSource source1 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s1star.Source = source1;

                    ImageSource source2 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s2star.Source = source2;

                    ImageSource source3 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s3star.Source = source3;

                    ImageSource source4 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s4star.Source = source4;

                    ratetext.Foreground = Brushes.Blue;
                    ratetext.Content = "Happy";

                    Rating = Rating.Happy;
                }
                else
                {
                    ImageSource source4 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s4star.Source = source4;

                    ImageSource source5 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s5star.Source = source5;

                    ratetext.Foreground = Brushes.LightGreen;
                    ratetext.Content = "Neutral";

                    Rating = Rating.Neutral;
                }
            }
            else if (btn.Name == "s5")
            {
                ImageSource source1 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                s1star.Source = source1;

                ImageSource source2 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                s2star.Source = source2;

                ImageSource source3 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                s3star.Source = source3;

                ImageSource source4 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                s4star.Source = source4;

                if (s5star.Source.ToString() == "pack://application:,,,/dstar.png")
                {
                    ImageSource source5 = new BitmapImage(new Uri("/estar.png", UriKind.RelativeOrAbsolute));
                    s5star.Source = source5;

                    ratetext.Foreground = Brushes.Orange;
                    ratetext.Content = "Excited";
                    Rating = Rating.Excited;
                }
                else
                {

                    ImageSource source5 = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
                    s5star.Source = source5;

                    ratetext.Foreground = Brushes.Blue;
                    ratetext.Content = "Happy";
                    Rating = Rating.Happy;
                }
            

            }
        }

        public void skipbtn_Click()
        {
         
            ImageSource source = new BitmapImage(new Uri("/dstar.png", UriKind.RelativeOrAbsolute));
            s1star.Source = source;
            s2star.Source = source;
            s3star.Source = source;
            s4star.Source = source;
            s5star.Source = source;

            ratetext.Content = "";
        }


    }
}
