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
    /// Interaction logic for SideNavigationPanel.xaml
    /// </summary>
    public partial class SideNavigationPanel : UserControl
    {
        public SideNavigationPanel()
        {

            InitializeComponent();

            sourcestackpanel.Visibility = Visibility.Hidden;
            destinationstackpanel.Visibility = Visibility.Hidden;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;

            sourcestackpanel.Visibility = Visibility.Visible;
            destinationstackpanel.Visibility = Visibility.Visible;

            PlacesList.Visibility = Visibility.Visible;

            searchbtn.Visibility = Visibility.Visible;

            circleimg.Visibility = Visibility.Visible;
            pushpinimg.Visibility = Visibility.Visible;

            historyls.Visibility = Visibility.Collapsed;

            driverls.Visibility = Visibility.Collapsed;

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;

            sourcestackpanel.Visibility = Visibility.Hidden;
            destinationstackpanel.Visibility = Visibility.Hidden;

            PlacesList.Visibility = Visibility.Collapsed;


            searchbtn.Visibility = Visibility.Collapsed;

            circleimg.Visibility = Visibility.Collapsed;
            pushpinimg.Visibility = Visibility.Collapsed;

            historyls.Visibility = Visibility.Collapsed;
            driverls.Visibility = Visibility.Collapsed;

        }

      

        private void historybtn_Click(object sender, RoutedEventArgs e)
        {
            historyls.Visibility = Visibility.Visible;

            ButtonCloseMenu.Visibility = Visibility.Visible;

            sourcestackpanel.Visibility = Visibility.Hidden;
            destinationstackpanel.Visibility = Visibility.Hidden;

            PlacesList.Visibility = Visibility.Collapsed;


            searchbtn.Visibility = Visibility.Collapsed;

            circleimg.Visibility = Visibility.Collapsed;
            pushpinimg.Visibility = Visibility.Collapsed;

            driverls.Visibility = Visibility.Collapsed;
        }

        private void driverlsbtn_Click(object sender, RoutedEventArgs e)
        {

            sourcestackpanel.Visibility = Visibility.Hidden;
            destinationstackpanel.Visibility = Visibility.Hidden;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            historyls.Visibility = Visibility.Collapsed;
            PlacesList.Visibility = Visibility.Collapsed;
            driverls.Visibility = Visibility.Visible;
        }
    }
}
