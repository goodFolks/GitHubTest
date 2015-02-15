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
         bar.IsIndeterminate = true;
           

        }

        void client_getFriendsPostsCompleted(object sender, getFriendsPostsCompletedEventArgs e)
        {
            
            listt.ItemsSource = e.Result;
            bar.IsIndeterminate = false;
           
           
            
           
           
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/notifications.xaml",UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/writePost.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/feed.xaml?" + DateTime.Now.Ticks, UriKind.Relative));
        }

      
        int PID;
        private void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
             PID= int.Parse((sender as Image).Tag.ToString());
          client.isSalutedAsync(PID, userId);
          client.isSalutedCompleted += client_isSalutedCompleted;
        }

        void client_isSalutedCompleted(object sender, isSalutedCompletedEventArgs e)
        {
            
            if(e.Result)
            {
                MessageBox.Show("saluted already");
            }
            if(!e.Result)
            {

                client.addSaluteAsync(PID, userId);
                client.addSaluteCompleted+=client_addSaluteCompleted;

            }
        }

        void client_addSaluteCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
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
               
                try
                {
                    
                    ff.SetSource(ms);
                    
                }
                catch(Exception e)
                {
                    ms.Close();
                }
                finally
                {
                  
                }
               
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