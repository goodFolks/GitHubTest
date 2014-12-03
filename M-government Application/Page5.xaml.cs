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
using System.Windows.Media;
using M_government_Application.Resources;


namespace M_government_Application
{
    public partial class Page5 : PhoneApplicationPage
    {
        Service1Client client;
        
        public Page5()
        {
            InitializeComponent();
            client = new Service1Client();
            
           
     

        }
        List<CheckBox> myCheckboxes;

        private void getTextBlock(DependencyObject lb)
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(lb);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(lb, i);


                if (child is TextBlock)
                {

                    myTxtblock = child as TextBlock;
                    return;
                }
               

               getTextBlock(child);
            }
            
        }
        TextBlock myTxtblock;

        private void GetItemsRecursive(DependencyObject lb)
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(lb);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(lb, i);


                if (child is CheckBox)
                {
                    myCheckboxes.Add((CheckBox)child);
                    return;
                }

                GetItemsRecursive(child);
            }
        }
        
        

        private void bac_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page4.xaml", UriKind.Relative));
        }


        int lang;
        int serId;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            lang = (int)PhoneApplicationService.Current.State["language"];
            serId = (int)PhoneApplicationService.Current.State["serviceId"];
            client.getDocumentsAsync(serId, lang);
            client.getDocumentsCompleted += client_getDocumentsCompleted;

            
        }

        void client_getDocumentsCompleted(object sender, getDocumentsCompletedEventArgs e)
        {
            list.ItemsSource =e.Result;
            
        }

        private void nex_btn_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            myCheckboxes = new List<CheckBox>();

            GetItemsRecursive(list); // our list , have all the checkboxes
            bool done = true;
            for (int i = 0; i < myCheckboxes.Count; i++)
            {

                if (myCheckboxes[i].IsChecked == false)
                {
                    done = false;

                    (myCheckboxes[i].Content as TextBlock).Foreground = new SolidColorBrush(Colors.Red);


                }
                else
                {

                    (myCheckboxes[i].Content as TextBlock).Foreground = new SolidColorBrush(Colors.Black);


                }
                myTxtblock = null;


            }
            list.UpdateLayout();


            if (done)
                NavigationService.Navigate(new Uri("/Page6.xaml", UriKind.Relative));
            else
            {
                list.UpdateLayout();
                MessageBox.Show(AppResources.Docdone);
                list.UpdateLayout();
            }

        }

       
        
       
    }
}