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

namespace M_government_Application
{
    public partial class Page6 : PhoneApplicationPage
    {
        Service1Client client;
        public Page6()
        {
            InitializeComponent();
            client = new Service1Client();
        }

        private void nex_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page7.xaml", UriKind.Relative));
        }

        private void bac_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page5.xaml", UriKind.Relative));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            serId = (int)PhoneApplicationService.Current.State["serviceId"];
            int lang = (int)PhoneApplicationService.Current.State["language"];

            client.getSingleServiceAsync(serId, lang);
            client.getSingleServiceCompleted += client_getSingleServiceCompleted;



        }
        int serId;
        int lang;
        void client_getSingleServiceCompleted(object sender, getSingleServiceCompletedEventArgs e)
        {
            Service temp = e.Result;
            feesTxt.Text += "\n\n" + temp.Fees;
            locationTxt.Text += "\n\n" + temp.Location;
        }
    }
}