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
    public partial class Page2 : PhoneApplicationPage
    {
        Service1Client client;

        private bool IsInit { get; set; }
        private bool IsInitSer { get; set; }
        private bool IsInitSub { get; set; }
     
          public Page2()

        {
          
            InitializeComponent();
            client = new Service1Client();
            IsInit = false;
            IsInitSer=false;
            IsInitSub = false;
            isSerTapped = false;
            isSubTapped = false;
            
        }

         
         

        private void nex_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (Ser.SelectedItem == ff1 && Subser.SelectedItem==ff)
                MessageBox.Show(AppResources.Selectser);
            else
                NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
            
        }

        private void bac_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            bar.IsIndeterminate = true;
            int lang = 1;
            base.OnNavigatedTo(e);
            int clientType = (int)PhoneApplicationService.Current.State["clientType"];
            lang = (int)PhoneApplicationService.Current.State["language"];
            if (!IsInit)
            {
                client.getMainCategoryAsync(clientType, lang);
                client.getMainCategoryCompleted += client_getMainCategoryCompleted;
                IsInit = true;
            }
            if (index != -1)
            {
                Mainser.SelectedIndex = index;

            }




        }
        
      MainCategory selectedItem;
          

        void client_getMainCategoryCompleted(object sender, getMainCategoryCompletedEventArgs e)
        {
         Mainser.ItemsSource=e.Result;
         bar.IsIndeterminate = false;

        }

        private void Mainser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int lang = 1;
            int clientType = (int)PhoneApplicationService.Current.State["clientType"];
            lang = (int)PhoneApplicationService.Current.State["language"];
          
             index = (sender as ListPicker).SelectedIndex;
             selectedItem = (sender as ListPicker).SelectedItem as MainCategory;

          

             if (index != -1)
             {
                 if (!IsInitSer)
                 {
                     client.getServiceAsync(selectedItem.Id, lang);
                     client.getServiceCompleted += client_getServiceCompleted;
                     IsInitSer = true;
                 }
                 if (!IsInitSub)
                 {
                     client.getSubServiceAsync(selectedItem.Id,lang);
                     client.getSubServiceCompleted += client_getSubServiceCompleted;
                     IsInitSub = true;
                 }

             }
             



        }
        Service ff1;
        void client_getSubServiceCompleted(object sender, getSubServiceCompletedEventArgs e)
        {

            List<Service> ss = e.Result.ToList();
         //   Ser.ItemsSource = e.Result;
           ff1 = new Service();
            ff1.Name = "    ";
            ss.Add(ff1);
            Ser.ItemsSource = ss;
            Ser.SelectedItem = ff1;
            Ser.UpdateLayout();
            IsInitSer = true;
            
        }

        Service ff;

        void client_getServiceCompleted(object sender, getServiceCompletedEventArgs e)
        {
            List<Service> ss = e.Result.ToList();
            //   Ser.ItemsSource = e.Result;
            ff = new Service();
            ff.Name = "    ";
            ss.Add(ff);
            Subser.ItemsSource = ss;
            Subser.SelectedItem = ff;
            Subser.UpdateLayout();
            IsInitSub = true;
        }
        int index = -1;

        private void Subser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if((sender as ListPicker).SelectedItem!=ff   )
           {
              
               SerId=((sender as ListPicker).SelectedItem as Service).Id;
               Ser.SelectedItem = ff1;
               Ser.UpdateLayout();

               
           }
            
        }
        int SerId = -1;
       // int tst = -1;
        private void Ser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if ((sender as ListPicker).SelectedItem !=ff1 )
            {
               
                SerId = ((sender as ListPicker).SelectedItem as Service).Id;
                Subser.SelectedItem = ff;
                Subser.UpdateLayout();
               
            }
        }

        private void Mainser_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            /*
            IsInitSub = false;
            IsInitSer = false;
            MessageBox.Show("main tapped");
             */
        }

        private void Mainser_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            IsInitSub = false;
            IsInitSer = false;
        }

        private void Subser_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            isSubTapped = true;
            isSerTapped = false;
        }

        bool isSerTapped {get; set;}
        bool isSubTapped { get; set; }

        private void Ser_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            isSubTapped = false;
            isSerTapped = true;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            int id = 0;
            if ((Subser.SelectedItem as Service) != ff)
                id = (Subser.SelectedItem as Service).Id;
            else
                if ((Ser.SelectedItem as Service) != ff1)
                    id = (Ser.SelectedItem as Service).Id;


            PhoneApplicationService.Current.State["serviceId"] = id;


        }

       
       
    }

   

    

}