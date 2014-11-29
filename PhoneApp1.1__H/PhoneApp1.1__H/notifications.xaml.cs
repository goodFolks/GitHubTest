using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp1._1__H
{
    public partial class notifications : PhoneApplicationPage
    {
        public notifications()
        {
            InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            List<Notification> nots = new List<Notification>();
            Notification n1 = new Notification("images/volunteerwork1.jpg", "images/volunteer1.jpg", new DateTime(1992, 3, 24), "Zeko  Zakarneh ");
            Notification n2 = new Notification("images/volunteerwork2.jpg", "images/volunteer2.jpg", new DateTime(1992, 3, 24), "Feko Saluted your work ");
            Notification n3 = new Notification("images/volunteerwork3.jpg", "images/volunteer3.jpg", new DateTime(1992, 3, 24), "Weko Saluted your work ");
            Notification n4 = new Notification("images/volunteerwork1.jpg", "images/volunteer1.jpg", new DateTime(1992, 3, 24), "Zeko Saluted your work ");
            Notification n5 = new Notification("images/volunteerwork2.jpg", "images/volunteer2.jpg", new DateTime(1992, 3, 24), "Feko Saluted your work ");
            Notification n6 = new Notification("images/volunteerwork3.jpg", "images/volunteer3.jpg", new DateTime(1992, 3, 24), "Weko Saluted your work ");
            Notification n7 = new Notification("images/volunteerwork2.jpg", "images/volunteer2.jpg", new DateTime(1992, 3, 24), "Feko Saluted your work ");
            Notification n9 = new Notification("images/volunteerwork2.jpg", "images/volunteer2.jpg", new DateTime(1992, 3, 24), "Feko Saluted your work ");
            Notification n8 = new Notification("images/volunteerwork2.jpg", "images/volunteer2.jpg", new DateTime(1992, 3, 24), "Feko Saluted your work ");
            nots.Add(n1);
            nots.Add(n2);
            nots.Add(n3);
            nots.Add(n4);
            nots.Add(n5);
            nots.Add(n6);
            nots.Add(n7);
            nots.Add(n8);
            nots.Add(n9);
            mylist.ItemsSource = nots;
            myStoryboard.Begin();
        }
    }
    public class Notification
    {
        public string postImage;
        public string userImage;
        public DateTime postTime;
        public string postWords;

        public Notification(string postImage,string userImage,DateTime postTime,string postWords)
        {
            this.postImage = postImage;
            this.userImage = userImage;
            this.postWords = postWords;
            this.postTime = postTime;
        }
        public string PostWords
        {
            set { postWords = value; }
            get { return postWords; }
        }
        public string PostImage
        {
            set { postImage = value; }
            get { return postImage; }
        }
        public string UserImage
        {
            set { userImage = value; }
            get { return userImage; }
        }
        public DateTime Date
        {
            set { postTime = value; }
            get { return postTime; }
        }

    }
}