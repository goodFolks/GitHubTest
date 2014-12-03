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
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Phone.Tasks;
namespace M_government_Application
{
    public partial class Page7 : PhoneApplicationPage
    {
        Service1Client client;
        public Page7()
        {
            InitializeComponent();
            client = new Service1Client();
        }

        private void nex_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page8.xaml", UriKind.Relative));
        }

        private void bac_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page6.xaml", UriKind.Relative));
        }

        int lang;
        int serId;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            lang = (int)PhoneApplicationService.Current.State["language"];
            serId = (int)PhoneApplicationService.Current.State["serviceId"];

            client.getSingleServiceAsync(serId, lang);
            client.getSingleServiceCompleted += client_getSingleServiceCompleted;
          

           
        }

        void client_getSingleServiceCompleted(object sender, getSingleServiceCompletedEventArgs e)
        {
            BitmapImage temp = new BitmapImage();
            MemoryStream ms = new MemoryStream(e.Result.Procedure);
            temp.SetSource(ms);
            procImage.Source = temp;
         
       
        }

       

    }
}