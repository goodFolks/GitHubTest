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
using Microsoft.Phone.Tasks;
using M_government_Application.Resources;

namespace M_government_Application


{
    public partial class Page9 : PhoneApplicationPage
    {
        Service1Client client;
       
        public Page9()
        {
            InitializeComponent();
            client = new Service1Client();

        }

      

        private void hom_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
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
            client.getDocumentsAsync(serId, lang);
            client.getDocumentsCompleted += client_getDocumentsCompleted;
        }
        List<Document> docs;

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Sername.Text = AppResources.Ser;
            Fees.Text = AppResources.Fees;

            Time.Text = AppResources.Process;
            Apply.Text = AppResources.Loca;
        }

        void  client_getDocumentsCompleted(object sender, getDocumentsCompletedEventArgs e)
        {
            client.getDocumentsCompleted -= client_getDocumentsCompleted;
         
            docs = e.Result.ToList();
            listdoc.ItemsSource = e.Result;

            
        }



        void client_getSingleServiceCompleted(object sender, getSingleServiceCompletedEventArgs e)
        {
            client.getSingleServiceCompleted -= client_getSingleServiceCompleted;
           
            Sername.Text += " " + e.Result.Name;
            Fees.Text += " " + e.Result.Fees;
            Time.Text += " " + e.Result.DaysTaken;
            Apply.Text += "\n" + e.Result.Location;
            
        }

       

      


        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            SmsComposeTask smsComposeTask = new SmsComposeTask();
           
            smsComposeTask.To = "";
            string body = Sername.Text + " \n" + Fees.Text + "\n" + Time.Text + "\n" + Apply.Text + "\n"+Doc.Text+"\n\n";
            for (int i = 0; i < docs.Count;i++ )
            {
                body += docs[i].Index + ". " + docs[i].Name + "\n";
            }

            smsComposeTask.Body = body;



                smsComposeTask.Show();

        }

        private void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = Sername.Text;
            string body = Sername.Text + " \n\n" + Fees.Text + "\n\n" + Time.Text + "\n\n" + Apply.Text + "\n\n" + Doc.Text + "\n\n";
            for (int i = 0; i < docs.Count; i++)
            {
                body += docs[i].Index + ". " + docs[i].Name + "\n\n";
            }

            emailComposeTask.Body = body;
            emailComposeTask.To = "";
            emailComposeTask.Cc = "";
            emailComposeTask.Bcc = "";

            emailComposeTask.Show();
        }

    }
}