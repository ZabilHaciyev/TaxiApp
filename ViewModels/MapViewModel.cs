using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Device.Location;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using TaxiApp.Converters;
using TaxiApp.Data.DataContext;
using TaxiApp.Model;
using TaxiApp.Models;
using TaxiApp.RelayCommands;
using TaxiApp.Repository;
using TaxiApp.Services;
using TaxiApp.View;

namespace TaxiApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MapViewModel
    {
        public ObservableCollection<History> HistoryList { get; set; }
        public PolylineService polylineService { get; set; }
        public string PolyLineLocations { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; }
        public Driver NearDriver { get; set; }
        public ApplicationIdCredentialsProvider Provider { get; set; }
        public Location UserLocation { get; set; }
        private GeoCoordinateWatcher Watcher { get; set; }
        public SearchResult selectedPlaceWhereTo;
        public SearchResult SelectedPlaceWhereTo { get { return selectedPlaceWhereTo; } set { selectedPlaceWhereTo = value; SelectedPlaceVisibility = "Visible"; } }
        public string NearDriverVisibility { get; set; } = "Hidden";
        public string SelectedPlaceVisibility { get; set; } = "Hidden";
        public string TaxiOrderVisibility { get; set; } = "Hidden";
        public string RatingPanelVisibility { get; set; } = "Hidden";
        public string UserVisibility { get; set; } = "Visible";
        public DriverRepoService<Driver> DriverRepoService { get; set; }
        public UserRepoService<User> UserRepoService { get; set; }

        public User MainUser { get; set; } = new User();
        //new User() {  Firstname = "Iman",  Lastname = "Nesibov", Email = "  ", Password = "123" };
        public ObservableCollection<SearchResult> Places { set; get; }
        public DriverRepository repo { get; set; }
        public ICommand Search { get; set; }
        public ICommand WhereToSelected { get; set; }
        public ICommand TaxiAccepted { get; set; }
        public ICommand TaxiDenied { get; set; }
        public ICommand GiveRating { get; set; }
        public double Angle { get; set; }

        private Timer timer;
        private Timer timer1;


        public double CalculateAngle(Location startPoint, Location endPoint)
        {
            double lat1 = startPoint.Latitude;// 1E6;
            double lat2 = endPoint.Latitude;  // 1E6;
            double long1 = startPoint.Longitude; // 1E6;
            double long2 = endPoint.Longitude;  // 1E6;
            double dy = lat2 - lat1;
            double dx = Math.Cos(Math.PI / 180 * lat1) * (long2 - long1);
            double angle = Math.Atan2(dy, dx);
            return angle;
        }

        public double AngleFromCoordinate(Location startPoint, Location endPoint)
        {

            var angleDeg = Math.Atan2(endPoint.Longitude - startPoint.Longitude, endPoint.Latitude - startPoint.Latitude) * 180 / Math.PI;


            return angleDeg;

        }

        public MapViewModel(User user)
        {
            MainUser = user;
            WhereToSelected = new RelayCommand(WhereToSelectedExecute);
            DriverRepoService = new DriverRepoService<Driver>(new DataContext());
            UserRepoService = new UserRepoService<User>(new DataContext());
            GiveRating = new RelayCommand(GiveRatingExecute);
            Search = new RelayCommand(SearchExecute);
            Watcher = new GeoCoordinateWatcher();
            Provider = new ApplicationIdCredentialsProvider(ConfigurationManager.ConnectionStrings["BingApiKey"].ConnectionString);
            TaxiAccepted = new RelayCommand(TaxiAcceptedExecute);
            TaxiDenied = new RelayCommand(TaxiDeniedExecute);
            Watcher.StatusChanged += Watcher_StatusChanged;
            repo = new DriverRepository();
           
            var service = new DriverRepoService<Driver>(new DataContext());

            var drivers = new ObservableCollection<Driver>();


            foreach (var driver in service.GetAll())
            {
                driver.Location = new Location(repo.GetRandomLocation());
                drivers.Add(driver);
            }

            Drivers = drivers;
           
            Watcher.Start();
        }

        private void GiveRatingExecute(object obj)
        {
            RatingControl ratingControl =obj as RatingControl;
          
                
           RatingToStringConverter toRating = new RatingToStringConverter();
            NearDriver.Rating = (Model.Rating)toRating.ConvertBack(ratingControl.Rating, null, null, null);

            var service = new DriverRepoService<Driver>(new DataContext());

            var drivers = new ObservableCollection<Driver>();


            foreach (var driver in service.GetAll())
            {
                driver.Location = new Location(repo.GetRandomLocation());
                service.Update(driver);
                drivers.Add(driver);
            }

            Drivers = drivers;

            Drivers.Where(i => i.Firstname == NearDriver.Firstname).FirstOrDefault().Location = NearDriver.Location;
            Drivers.Where(i => i.Firstname == NearDriver.Firstname).FirstOrDefault().Rating = NearDriver.Rating;

            DriverRepoService.Update(NearDriver);
            UserRepoService.Update(MainUser);
            NearDriver = new Driver();
            RatingPanelVisibility = "Hidden";
            ratingControl.skipbtn_Click();
        }

        private void TaxiAcceptedExecute(object obj)
        {
            NearDriverVisibility = "Visible";
            TaxiOrderVisibility = "Hidden";
            NearDriver = new Driver();

            double min = (Math.Abs(UserLocation.Longitude - Drivers[0].Location.Longitude)) + (Math.Abs(UserLocation.Latitude - Drivers[0].Location.Latitude));

            foreach (var item in Drivers)
            {
                double PolyLineLocations = (Math.Abs(UserLocation.Longitude - item.Location.Longitude)) + (Math.Abs(UserLocation.Latitude - item.Location.Latitude));

                if (PolyLineLocations < min)
                {
                    min = PolyLineLocations;
                    NearDriver = item;
                }
            }
            Drivers.Clear();
            History history = new History { Cash = polylineService.PriceForDrive, DriverName = NearDriver.ToString(), Driverİmage = NearDriver.Image, StartPoint = "Your Location", EndPoint = SelectedPlaceWhereTo.Address, TripDate = DateTime.Now.ToString() };
            MainUser.HistoryList.Add(history);
           
            polylineService = new PolylineService(NearDriver.Location, UserLocation);
            PolyLineLocations = polylineService.GetPolyLineStr();
            SelectedPlaceVisibility = "Hidden";


            timer = new Timer();
            timer.Interval = 3;
            timer.Tick += DiverComing;
            timer.Start();
        }

        private void TaxiDeniedExecute(object obj)
        {
            TaxiOrderVisibility = "Hidden";
            PolyLineLocations = null;
            Places.Clear();
            SelectedPlaceVisibility = "Hidden";
        }


        private void WhereToSelectedExecute(object obj)
        {
            SelectedPlaceWhereTo = obj as SearchResult;
            Places.Clear();

            polylineService = new PolylineService(UserLocation, SelectedPlaceWhereTo.Location);
            PolyLineLocations = polylineService.GetPolyLineStr();
            Places.Add(SelectedPlaceWhereTo);
            TaxiOrderVisibility = "Visible";


        }

        private void DiverComing(object sender, EventArgs e)
        {
            if (PolyLineLocations.Length > 0)
            {
                int count1 = PolyLineLocations.IndexOf(',');
                double Lat = Double.Parse(PolyLineLocations.Substring(0, count1).ToString());
                PolyLineLocations = PolyLineLocations.Remove(0, count1 + 1);

                int count2 = PolyLineLocations.IndexOf(' ');
                double Long = Double.Parse(PolyLineLocations.Substring(0, count2).ToString());
                PolyLineLocations = PolyLineLocations.Remove(0, count2 + 1);

                Location location = new Location(Lat, Long);
                Angle = AngleFromCoordinate(NearDriver.Location, location);
                NearDriver.Location = new Location(location);

            }
            else
            {

                timer.Stop();
                UserVisibility = "Hidden";
                polylineService = new PolylineService(UserLocation, SelectedPlaceWhereTo.Location);
                PolyLineLocations = polylineService.GetPolyLineStr();
                SelectedPlaceVisibility = "Visible";
                timer1 = new Timer();
                timer1.Interval = 3;
                timer1.Tick += ArrivingToPlace;
                timer1.Start();

            }
        }

        private void ArrivingToPlace(object sender, EventArgs e)
        {
            if (PolyLineLocations.Length > 0)
            {
                int count1 = PolyLineLocations.IndexOf(',');
                double Lat = Double.Parse(PolyLineLocations.Substring(0, count1).ToString());
                PolyLineLocations = PolyLineLocations.Remove(0, count1 + 1);

                int count2 = PolyLineLocations.IndexOf(' ');
                double Long = Double.Parse(PolyLineLocations.Substring(0, count2).ToString());
                PolyLineLocations = PolyLineLocations.Remove(0, count2 + 1);

                Location location = new Location(Lat, Long);
                Angle = AngleFromCoordinate(NearDriver.Location, location);
                NearDriver.Location = new Location(location);
            }
            else
            {
                timer1.Stop();
                UserVisibility = "Vsible";
                SelectedPlaceVisibility = "Hidden";
                NearDriverVisibility = "Hidden";
                UserLocation = selectedPlaceWhereTo.Location;
                Places.Clear();
                RatingPanelVisibility = "Visible";
           
               
            }
        }

        private void SearchExecute(object obj)
        {
            var str = obj as string;
            if (str != null) { Places = new ObservableCollection<SearchResult>(GetSearchResultService.GetSearchResults(str)); }
        }

        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {

            if (e.Status == GeoPositionStatus.Ready)
            {
                if (!Watcher.Position.Location.IsUnknown)
                {
                    UserLocation = new Location(double.Parse(Watcher.Position.Location.Latitude.ToString()), double.Parse(Watcher.Position.Location.Longitude.ToString()));
                }

            }
        }

    }
}
