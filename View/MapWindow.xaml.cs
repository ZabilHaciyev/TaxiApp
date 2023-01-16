using System.Windows;
using TaxiApp.Model;
using TaxiApp.View;
using TaxiApp.ViewModels;

namespace TaxiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow(User user)
        {
            InitializeComponent();
            this.DataContext = new MapViewModel(user);
        }
    }
}
