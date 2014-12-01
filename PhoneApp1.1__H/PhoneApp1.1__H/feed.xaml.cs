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
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using System.IO;
using PhoneApp1._1__H.myService;

namespace PhoneApp1._1__H
{
    public partial class feed : PhoneApplicationPage
    {
        Service1Client client;
        int userId;
        
        public feed()
        {
            InitializeComponent();
            client = new Service1Client();
            




            //listt.ItemsSource = posts;
           
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            userId = (int)PhoneApplicationService.Current.State["CurrentUser"];
         client.getFriendsPostsAsync(userId);
         client.getFriendsPostsCompleted += client_getFriendsPostsCompleted;
           

        }

        void client_getFriendsPostsCompleted(object sender, getFriendsPostsCompletedEventArgs e)
        {
            listt.ItemsSource = e.Result;
           
            
           
           
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/notifications.xaml",UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/writePost.xaml", UriKind.Relative));
        }

    }

    public class ByetConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapImage ff = new BitmapImage();
            if (value != null)
            {
                byte[] imageBytes = value as byte[];
              //  BitmapImage ff = new BitmapImage();
                MemoryStream ms = new MemoryStream(imageBytes);
                ff.SetSource(ms);
            }
            return ff;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            BitmapImage myImage = value as BitmapImage;
            MemoryStream ms = new MemoryStream();
            WriteableBitmap wi = new WriteableBitmap(myImage);
            wi.SaveJpeg(ms, myImage.PixelWidth, myImage.PixelHeight, 0, 100);
            return  ms.ToArray();
        }
    }

    public class DateStringConv:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime date = DateTime.Parse(value.ToString());
            return date.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}