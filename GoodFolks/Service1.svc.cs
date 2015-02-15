using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GoodFolks
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        SqlConnection con;
        SqlCommand com;

        




        void newConnection()
        {

            con = new SqlConnection((ConfigurationManager.ConnectionStrings["GOODFOLKSConnectionString"].ConnectionString).ToString());
            con.Open();

        }


        public bool newUser(Member newUser)//insert a new user from the registration form
        {
            newConnection();
            string query = "INSERT INTO member (u_name,u_email,f_name,l_name,u_gender,u_password,create_date,u_type,b_date)"
                           +"VALUES (@Name,@Email,@FName,@LName,@Gender,@Password,@TimeCreate,@Type,@BirthDate)";
            com = new SqlCommand(query,con);
            com.Parameters.Add("@Name", SqlDbType.VarChar).Value = newUser.UserName;
            com.Parameters.Add("@Email", SqlDbType.VarChar).Value = newUser.UserEmail;
            com.Parameters.Add("@FName", SqlDbType.VarChar).Value = newUser.UserFName;
            com.Parameters.Add("@LName", SqlDbType.VarChar).Value = newUser.UserLName;
            com.Parameters.Add("@Gender", SqlDbType.Char).Value = newUser.UserGender;
            com.Parameters.Add("@Password", SqlDbType.VarChar).Value = newUser.Userpassword;
            com.Parameters.Add("@TimeCreate", SqlDbType.Date).Value = newUser.UserTimeCreated.Date;
            com.Parameters.Add("@Type", SqlDbType.Char).Value = newUser.UserType;
            com.Parameters.Add("@BirthDate", SqlDbType.Date).Value = newUser.UserBirthDate.Date;

            int status = com.ExecuteNonQuery();

            con.Close();

            return false;
          
        }

        public int Authenticate(UserAuth user)
        {
            newConnection();
            string query = "SELECT u_name,u_password,u_type,u_id FROM member where u_name=@Name";
            com = new SqlCommand(query,con);
            com.Parameters.Add("@Name", SqlDbType.VarChar).Value = user.UserName;
            
          
           SqlDataReader result = com.ExecuteReader();
           
            if(result.HasRows)
            {
                result.Read();
                if (user.Password == (string)result["u_password"])
                {
                    
                    string type = (string)result["u_type"];
                    int userId = (int)result["u_id"];
                  con.Close();
                  return userId;
                  
                    
                }

            }
            con.Close();
            
            return -1;

        }


        public bool ForgotPassword(string emailOrUser)
        {
            return false;
        }

        public List<Member> getFriendList(int userId)
        {
            throw new NotImplementedException();
        }

        public Member getMemberInfo(int userId)
        {
            throw new NotImplementedException();
        }

        public int getSalutes(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Post> getMyPosts(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Post> getFriendsPosts(int userId)
        {

            newConnection();
            string query = "select post_id, post_txt , post_image , post_location  , post_user , post_time from post where post_user in (select friend2 from friend where friend1=@User)";
            
            com = new SqlCommand(query, con);
            com.Parameters.Add("@User", SqlDbType.Int).Value = userId;

            SqlDataReader result = com.ExecuteReader();
            List<Post> posts = new List<Post>();

            while(result.Read())
            {
                Post temp = new Post();
                temp.txtContent = result["post_txt"] as string;
                temp.postImage = result["post_image"] as byte[];
                temp.postLocation = result["post_location"] as string;
                temp.postUser = (int)result["post_user"];
                temp.postTime = (DateTime)result["post_time"];
                temp.postId = (int)result["post_id"];
                string query2 = "select u_name , u_image from member where u_id=@Id";
                SqlConnection con2 = new SqlConnection((ConfigurationManager.ConnectionStrings["GOODFOLKSConnectionString"].ConnectionString).ToString());
                con2.Open();

                SqlCommand com2 = new SqlCommand(query2, con2);
                com2.Parameters.Add("@Id", SqlDbType.Int).Value = temp.postUser;
                SqlDataReader reader2 = com2.ExecuteReader();
                
                reader2.Read();
                temp.userImage = reader2["u_image"] as byte[];
                temp.userName = reader2["u_name"]as string;
                con2.Close();
                con2.Open();
                query2 = "select count(*) as counts from salute where salute_post=@postID";
                com2 = new SqlCommand(query2,con2);
                com2.Parameters.Add("@postID", SqlDbType.Int).Value = result["post_id"];
                reader2 = com2.ExecuteReader();
                reader2.Read();
                temp.Salutes = (int)reader2["counts"];
                con2.Close();
                posts.Add(temp);
                
                
            }
            con.Close();
            return posts;
        }

        public List<Event> getEvents()
        {
            throw new NotImplementedException();
        }

        public List<Post> getStrangerPosts()
        {
            throw new NotImplementedException();
        }

        public void insertImage(byte[] image)
        {
            throw new NotImplementedException();
        }

        public List<UserAuth> Test()
        {
            throw new NotImplementedException();
        }


        public bool newPost(Post newPost)
        {
            newConnection();
            string query = "insert into post (post_user,post_txt,post_time,post_location,post_image) values(@UserID,@Text,@Time,@Location,@Image)";

            com = new SqlCommand(query, con);
            com.Parameters.Add("@UserID", SqlDbType.Int).Value = newPost.postUser;
            com.Parameters.Add("@Text", SqlDbType.VarChar).Value = newPost.txtContent;
            com.Parameters.Add("@Time", SqlDbType.DateTime).Value = newPost.postTime;
            com.Parameters.Add("@Location", SqlDbType.VarChar).Value = newPost.postLocation;
            com.Parameters.Add("@Image", SqlDbType.Image).Value = newPost.postImage;
            int result = com.ExecuteNonQuery();
            con.Close();
            return result > 0;
          
               
            
            
        }


        public void addSalute(int postId, int userID)
        {
            newConnection();
            string query = "insert into salute (salute_post,salute_user) values (@PID,@UID)";
            com = new SqlCommand(query, con);
            com.Parameters.Add("@PID", SqlDbType.Int).Value = postId;
            com.Parameters.Add("@UID", SqlDbType.Int).Value = userID;
            int result = com.ExecuteNonQuery();
            con.Close();
        }

        public bool isSaluted(int postId, int userID)
        {
            newConnection();
            string query = "select * from salute where salute_post=@PID and salute_user=@UID";
            com = new SqlCommand(query, con);
            com.Parameters.Add("@PID", SqlDbType.Int).Value = postId;
            com.Parameters.Add("@UID", SqlDbType.Int).Value = userID;
            SqlDataReader reader = com.ExecuteReader();
            bool result = reader.HasRows;
            con.Close();
            return result;
      
            
        }
    }

}
