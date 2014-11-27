using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PhoneApp1._1__H.myService;
using Microsoft.Phone.Shell;



namespace PhoneApp1._1__H
{
    public partial class MainPage : PhoneApplicationPage
    {
        Service1Client client;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            client = new Service1Client();
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/signUp.xaml",UriKind.Relative));
        }

        private void fpass_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new Uri("/fpass.xaml", UriKind.Relative));
            
        }

        void client_getFriendsPostsCompleted(object sender, getFriendsPostsCompletedEventArgs e)
        {
            List<Post> res = e.Result.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            
        }

        void client_AuthenticateCompleted(object sender, AuthenticateCompletedEventArgs e)
        {
            if (e.Result != -1)
            {
                userId = e.Result;

                NavigationService.Navigate(new Uri("/feed.xaml", UriKind.Relative));

            }


            else
            {
                MessageBox.Show("User Not Found");
                client.AuthenticateCompleted -= client_AuthenticateCompleted;//remove the stack call from the service.
            }

            

            
        }
        int userId;
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            PhoneApplicationService.Current.State["CurrentUser"] = userId;
        
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string userName = userTxt.Text;
            string userPassword = userPass.Password;
            UserAuth user = new UserAuth();
            user.Password = userPassword;
            user.UserName = userName;
            client.AuthenticateAsync(user);
            client.AuthenticateCompleted += client_AuthenticateCompleted;
        }
    }
}