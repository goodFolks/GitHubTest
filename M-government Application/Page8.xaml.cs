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
    public partial class Page8 : PhoneApplicationPage
    {
        Service1Client client;
        public Page8()
        {
            InitializeComponent();
            client = new Service1Client();
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
            timeTxt.Text = temp.DaysTaken;
        }

        private void hom_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page9.xaml", UriKind.Relative));
        }
        
    }
}