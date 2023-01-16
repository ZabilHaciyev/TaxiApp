using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using TaxiApp.Data.DataContext;
using TaxiApp.Model;
using TaxiApp.Services;
using TaxiApp.ViewModels;

namespace TaxiApp.View
{
    public partial class LoginRegisterControl : UserControl
    {
        public LoginRegisterControl()
        {
            InitializeComponent();
        }

        private Random _random = new Random();
        UserRepoService<User> _service =
            new UserRepoService<User>(new DataContext());
        
        private int _rint;
        private string _verify;

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            last.Visibility = Visibility.Visible;
            first.Visibility = Visibility.Visible;
            Confpass.Visibility = Visibility.Visible;
            Mail.FontSize = 15;
            Password.FontSize = 15;
            Mail.Margin = new Thickness(20, 15, 20, 10);
            Password.Margin = new Thickness(20, 5, 20, 10);
            signup.Margin = new Thickness(140, 0, 130, 10);
            Confpass.Margin = new Thickness(20, 5, 20, 10);
            ques.Margin = new Thickness(115, 0, 70, 29);
            ques.Text = "  Have an account?   ";
            login.Visibility = Visibility.Visible;
            signup.Visibility = Visibility.Collapsed;
            logbttn.Visibility = Visibility.Collapsed;
            signbttn.Visibility = Visibility.Visible;
            loglbl.Content = "SIGN UP";
            Mail.Text = null;
            first.Text = null;
            last.Text = null;
            Password.Password = null;
            Confpass.Password = null;
        }
        private void login_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            last.Visibility = Visibility.Collapsed;
            first.Visibility = Visibility.Collapsed;
            Confpass.Visibility = Visibility.Collapsed;
            Mail.Margin = new Thickness(20, 30, 0, 0);
            Mail.FontSize = 18;
            Password.Margin = new Thickness(20, 30, 0, 0);
            Password.FontSize = 18;
            signup.Margin = new Thickness(139, 0, 135, 10);
            ques.Margin = new Thickness(106, 0, 97, 29);
            ques.Text = "  Don't have an account?   ";
            signup.Visibility = Visibility.Visible;
            login.Visibility = Visibility.Collapsed;
            logbttn.Visibility = Visibility.Visible;
            signbttn.Visibility = Visibility.Collapsed;
            loglbl.Content = "LOG IN";
        }
        private void signbttn_Click(object sender, RoutedEventArgs e)
        {
            if (_service.Get(Mail.Text) != null)
            {
                return;
            }
            if (first.Text == string.Empty || Mail.Text == string.Empty || last.Text == string.Empty || Password.Password == string.Empty || Confpass.Password == string.Empty)
            {
                return;
            }
            else if (first.Text != string.Empty && Mail.Text != string.Empty && last.Text != string.Empty && (Password.Password == string.Empty || Password.Password.Length > 7) && Confpass.Password != string.Empty && Password.Password == Confpass.Password)
            {
                _random = new Random();
                _rint = _random.Next(100000, 999999);
                _verify = _rint.ToString();



                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("hazirtaxi@gmail.com", "cetinkod123"),
                    EnableSsl = true
                };



                client.Send("hazirtaxi@gmail.com", Mail.Text, "Verification Code", "Hi " + first.Text + "," + "Welcome to HAZIRTaxi! " + " Your verification code: " + _verify);



                vrftxt.Visibility = Visibility.Visible;
                Mail.Visibility = Visibility.Collapsed;
                first.Visibility = Visibility.Collapsed;
                last.Visibility = Visibility.Collapsed;
                Password.Visibility = Visibility.Collapsed;
                Confpass.Visibility = Visibility.Collapsed;
                signbttn.Visibility = Visibility.Collapsed;
                vrfbttn.Visibility = Visibility.Visible;
            }
            else if (Password.Password != Confpass.Password)
            {



                MessageBox.Show("Your password and confirmation password do not match!");
                return;
            }
        }
        private void vrfbttn_Click(object sender, RoutedEventArgs e)
        {
            if (vrftxt.Text == _verify)
            {
                User user = new User
                {
                    Firstname = first.Text,
                    Lastname = last.Text,
                    Email = Mail.Text.ToLower(),
                    Password = Password.Password,
                };



                _service.Add(user);



                MapWindow _mapWindow = new MapWindow(user);
                MessageBox.Show("succed register");
                _mapWindow.Show();



                Close();
            }
        }
        private void logbttn_Click(object sender, RoutedEventArgs e)
        {
            User user = _service.Get(Mail.Text.ToLower());
            if (user != null)
            {
                if (user.Password == Password.Password)
                {
                    MapWindow mapWindow = new MapWindow(user);
                    mapWindow.Show();
                    Close();
                }
            }
        }
        private void Close()
        {
            var myWindow = (Window)VisualParent.GetSelfAndAncestors().FirstOrDefault(a => a is Window);
            myWindow.Close();
        }
    }
}
