using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using M_government_Application.Resources;
using System.Globalization;
using System.Resources;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Windows.Markup;


// set session variable to determine what language we will use for the database 


namespace M_government_Application
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        int language = 0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }


        private void SetUILanguage(string locale)
        {
            // Set this thread's current culture to the culture associated with the selected locale.
            CultureInfo newCulture = new CultureInfo(locale);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            // Set the FlowDirection of the RootFrame to match the new culture.
            FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection),
                AppResources.ResourceFlowDirection);
            App.RootFrame.FlowDirection = flow;

            // Set the Language of the RootFrame to match the new culture.
            App.RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

            // Refresh the page in order to localize already rendered elements
            NavigationService.Navigate(new Uri(NavigationService.Source + "?Refresh=true", UriKind.Relative));

        }
      

       

        private void arb_btn_Click(object sender, RoutedEventArgs e)
        {

           
            language = 2;
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
            SetUILanguage("ar-SA");
         
           

        }

        private void eng_btn_Click(object sender, RoutedEventArgs e)
        {
            language = 1;
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));

            SetUILanguage("");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            PhoneApplicationService.Current.State["language"] = language;
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
    }
}