using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Test.Resources;
using Test.ServiceReference1;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;
namespace Test
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        Service1Client client;
        public MainPage()
        {
            InitializeComponent();
            client = new Service1Client();
          

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            client.getFriendsPostsAsync(12);
            client.getFriendsPostsCompleted += client_getFriendsPostsCompleted;
        }

        void client_getFriendsPostsCompleted(object sender, getFriendsPostsCompletedEventArgs e)
        {

            list.ItemsSource = e.Result;
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
            Image image = value as Image;
            BitmapImage myImage = image.Source as BitmapImage;
            MemoryStream ms = new MemoryStream();
            WriteableBitmap wi = new WriteableBitmap(myImage);
            wi.SaveJpeg(ms, myImage.PixelWidth, myImage.PixelHeight, 0, 100);
            return ms.ToArray();
        }
    }
}