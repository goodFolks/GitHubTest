using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using M_government_Application.WorkflowService;
using M_government_Application.Resources;
using System.Globalization;
using System.Resources;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Windows.Markup;




namespace M_government_Application
{
    public partial class Page1 : PhoneApplicationPage
    {
        int clientType = 0;
   
        public Page1()
        {
            InitializeComponent();
        }

       
        private void empl_btn_Click(object sender, RoutedEventArgs e)
        {
            clientType = 1;
            
            
           
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void bac_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);


            PhoneApplicationService.Current.State["clientType"] = clientType;

         
            
          
        }

      
       
        private void empl2_btn_Click(object sender, RoutedEventArgs e)
        {

            clientType = 2;



            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

      
    }
}