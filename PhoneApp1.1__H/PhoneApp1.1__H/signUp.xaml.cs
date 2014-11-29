using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1._1__H.myService;
using CS.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;
/*
 * Controls Names used for signup Page :
 * 
 * user name : userTxt
 * password : passTxt
 * confirm password : passTxtConf
 * email : emailTxt
 * first name : fName
 * last name : lName
 * birthdate : bDate
 * male gender : malBtn
 * female gender : femalBtn
 * indvidual user type : indUser
 * organisation user type : orgUser
 * 
 * 
 * 
 * methods : create account tap handler
 * get data from fields and make an object , then send it to service into database.
 * 
 * 
 * 
 * 
 * 
 * 
 */
namespace PhoneApp1._1__H
{
    public partial class signUp : PhoneApplicationPage
    {
        Service1Client client;     
        public signUp()
        {
            InitializeComponent();
            client = new Service1Client();
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // email regex
            Regex f = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");


                
                Member newUser = new Member();
                newUser.UserName = userTxt.Text.ToLower();
                newUser.Userpassword = passTxt.Password;
                newUser.UserEmail = emailTxt.Text;
            
                newUser.UserFName = fName.Text;
                newUser.UserLName = lName.Text;

                newUser.UserBirthDate = bDate.Value.Value;
                if (malBtn.IsChecked == true)
                    newUser.UserGender = "M";
                else
                    if (femalBtn.IsChecked == true)
                        newUser.UserGender = "F";

                if (indUser.IsChecked == true)
                    newUser.UserType = "I";
                else
                    if (orgUser.IsChecked == true)
                        newUser.UserType = "O";
                newUser.UserTimeCreated = DateTime.Now;


                client.newUserAsync(newUser);
                client.newUserCompleted += client_newUserCompleted;
                MessageBoxResult result = MessageBox.Show("Welcome to goodfolks , now you can sign in with your username", "Congratulations", MessageBoxButton.OK);

                if (result == MessageBoxResult.OK)
            {
               NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
             
        }

        void client_newUserCompleted(object sender, newUserCompletedEventArgs e)
        {
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private void bDate_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bDate.ValueStringFormat = null;
        }

        
    }
}