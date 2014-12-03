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
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace PhoneApp1._1__H
{
    public partial class writePost : PhoneApplicationPage
    {
        CameraCaptureTask upload;
        Service1Client client;
        public writePost()
        {
            InitializeComponent();
            client = new Service1Client();
            upload = new CameraCaptureTask();
           upload.Completed += upload_Completed;
          
        }

        void upload_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage postImage = new BitmapImage();
                postImage.SetSource(e.ChosenPhoto);
                MemoryStream ms = new MemoryStream();
                WriteableBitmap wi = new WriteableBitmap(postImage);
                wi.SaveJpeg(ms, postImage.PixelWidth, postImage.PixelHeight, 0, 50);
                
                imageBytes = ms.ToArray();

            
            }
        }
        byte[] imageBytes;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            myStoryboard.Begin();
            
            userId = (int)PhoneApplicationService.Current.State["CurrentUser"];
        }
        int userId;
        
       

        void client_newPostCompleted(object sender, newPostCompletedEventArgs e)
        {
            
            if (e.Result)
            {
                NavigationService.Navigate(new Uri("/feed.xaml", UriKind.Relative));
            }
        }

        
        private void uploadButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            upload.Show();
        }

        private void postButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
            Post newPost = new Post();
            newPost.postLocation = "Abdoun";
            newPost.postTime = DateTime.Now;
            newPost.postUser = userId;
            newPost.txtContent = postBox.Text;
            newPost.postImage = imageBytes;

            client.newPostAsync(newPost);
            client.newPostCompleted += client_newPostCompleted;
            
        }
    }
}