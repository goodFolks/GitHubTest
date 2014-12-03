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
    public partial class Page4 : PhoneApplicationPage
    {
        Service1Client client;
          public Page4()
        {
            InitializeComponent();
            client = new Service1Client();
        }

          private void nex_btn_Click(object sender, RoutedEventArgs e)
          {
              if(counter<listSize)
              {
                  MessageBox.Show(AppResources.Condnext);
              }
              else
              NavigationService.Navigate(new Uri("/Page5.xaml", UriKind.Relative));

          }

          private void bac_btn_Click(object sender, RoutedEventArgs e)
          {
              NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
          }

          int lang;
          int serId;
          protected override void OnNavigatedTo(NavigationEventArgs e)
          {
              base.OnNavigatedTo(e);
              lang = (int)PhoneApplicationService.Current.State["language"];
              serId = (int)PhoneApplicationService.Current.State["serviceId"];
              client.getConditionsAsync(serId, lang);
              client.getConditionsCompleted += client_getConditionsCompleted;


          }
          List<Condition> cons;
          int listSize = 0;
          int counter = 0;

          void client_getConditionsCompleted(object sender, getConditionsCompletedEventArgs e)
          {
              if (e.Result.Count > 0)
              {
                  cons = e.Result.ToList();
                  listSize = cons.Count;
                  questionCounter.Text = "(" + (counter + 1) + "/" + listSize + ")";
                  questionTxt.Text = cons[counter].Name;
                  counter++;
              }
              else
              {
                  yesBtn.Visibility = Visibility.Collapsed;
                  noBtn.Visibility = Visibility.Collapsed;
                  questionCounter.Visibility = Visibility.Collapsed;
                  questionTxt.Text = AppResources.Nocon;
              }
             

          }

          private void yesBtn_Tap(object sender, System.Windows.Input.GestureEventArgs e)
          {
              // counter 1 , listsize 5,
              if (counter < listSize)
              {
                  
                  questionCounter.Text = "(" + (counter + 1) + "/" + listSize + ")";
                  questionTxt.Text = cons[counter].Name;
                  
                  
                  counter++;
                  
              }
              else
              {
                  MessageBox.Show(AppResources.Condq);
                  yesBtn.Visibility = Visibility.Collapsed;
                  noBtn.Visibility = Visibility.Collapsed;
              }

              

             

          }

          private void noBtn_Tap(object sender, System.Windows.Input.GestureEventArgs e)
          {
              MessageBox.Show(AppResources.Condno);
          }


        
    }

}