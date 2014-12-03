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

namespace M_government_Application
{
    public partial class Page3 : PhoneApplicationPage
    {
        public Page3()
        {
            InitializeComponent();
            client = new Service1Client();
          //  LayoutRoot.FlowDirection = FlowDirection.LeftToRight;

        }
        Service1Client client;
        int serId;

        private void nex_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page4.xaml", UriKind.Relative));
        }

        private void bac_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            serId =(int) PhoneApplicationService.Current.State["serviceId"];
            int    lang = (int)PhoneApplicationService.Current.State["language"];
          
            client.getSingleServiceAsync(serId,lang);
            client.getSingleServiceCompleted += client_getSingleServiceCompleted;
            
            

        }

        void client_getSingleServiceCompleted(object sender, getSingleServiceCompletedEventArgs e)
        {
            int serSubType = e.Result.SubType;
            string type = "";
            switch(serSubType)
            {
                case 1: type = AppResources.SerType; break;
                case 2: type = AppResources.SerType2; break;
            }
            serTypetxt.Text += " " + type;
            string def = e.Result.Description;
            serDefTxt.Text += " " + def;
        }
    }
}