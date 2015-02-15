using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GoodFolks
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        
       

        [OperationContract]
        bool newUser(Member newUser);
        [OperationContract]
        bool newPost(Post newPost);

        [OperationContract]
        int Authenticate(UserAuth user);

        [OperationContract]
        bool ForgotPassword(string emailOrUser);

        [OperationContract]
        List<Member> getFriendList(int userId);

        [OperationContract]
        Member getMemberInfo(int userId);

        [OperationContract]
        int getSalutes(int userId);

        [OperationContract]
        List<Post> getMyPosts(int userId);

        [OperationContract]
        List<Post> getFriendsPosts(int userId);

        [OperationContract]
        List<Event> getEvents();

        [OperationContract]
        List<Post> getStrangerPosts();

        [OperationContract]
        void addSalute(int postId, int userID);

        [OperationContract]
        bool isSaluted(int postId, int userID);
        

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class UserAuth
    {
        string userName = string.Empty;
        string password = string.Empty;

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        [DataMember]
        public int UserId { get; set; }
    }

    [DataContract]
    public class Member
    {
        string userName = string.Empty;
        int userId = 0;
        string userEmail = string.Empty;
        DateTime userTimeCreated;
        string userFName = string.Empty;
        string userLName = string.Empty;
        DateTime userBirthDate ;
        string userType;
        string userGender;
        string userAddress = string.Empty;
        string userEducation = string.Empty;
        byte[] userImage;
        [DataMember]
        public string Userpassword;

        [DataMember]
        public string UserEducation
        {
            get { return userEducation; }
            set { userEducation = value; }
        }


        [DataMember]
        public string UserAdress
        {
            get { return userAddress; }
            set { userAddress = value; }
        }


        [DataMember]
        public string UserGender
        {
            get { return userGender; }
            set { userGender = value; }
        }


        [DataMember]
        public string UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        [DataMember]
        public DateTime UserBirthDate
        {
            get { return userBirthDate; }
            set { userBirthDate = value; }
        }


        [DataMember]
        public string UserLName
        {
            get { return userLName; }
            set { userLName = value; }
        }


        [DataMember]
        public string UserFName
        {
            get { return userFName; }
            set { userFName = value; }
        }


        [DataMember]
        public DateTime UserTimeCreated
        {
            get { return userTimeCreated; }
            set { userTimeCreated = value; }
        }


        [DataMember]
        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; }
        }


        [DataMember]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

    }


    [DataContract]
    public class Post
    {
        [DataMember]
      public  string txtContent { get; set; }

        [DataMember]
     public   int postId { get; set; }

        [DataMember]
        public int postUser { get; set; }

        [DataMember]
     public   DateTime postTime { get; set; }

        [DataMember]
        public byte[] postImage { get; set; }

        [DataMember]
      public  char postType { get; set; }

        [DataMember]
        public string postLocation { get; set; }

        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public byte[] userImage { get; set; }
        [DataMember]
        public int Salutes { get; set; }

         

        





    }

    [DataContract]
    public class Event
    {
        [DataMember]
        int eventId;
        [DataMember]
        int eventHost;
        [DataMember]
        int eventType;
        [DataMember]
        string eventName;
        [DataMember]
        string eventDate;
        [DataMember]
        string eventCaption;
        [DataMember]
        string eventLocation;
        [DataMember]
        int currentAttending;
        [DataMember]
        int maxAttend;


    }

}
