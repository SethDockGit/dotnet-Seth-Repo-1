//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using PartOrderingApp.Models;

//namespace PartOrderingApp.Data
//{
//    public class TxtUserData : IUserData
//    {
//        public List<User> Users { get; set; }

//        private string Filepath { get; set; }

//        public TxtUserData()
//        {
//            Filepath = @"C:\Data\PartOrderingApp\";
//            Users = new List<User>(); 
                
//            PopulateUsers();
//        }

//        private List<User> PopulateUsers() //to inmemory
//        {

//            //only needs to populate list of users

//            string[] users = Directory.GetDirectories(Filepath);

//            for(int i = 0; i < users.Length; i++)
//            {
//                new User()
//                {
//                    Username = Directory.GetDirectoryRoot(users[i]) //probably wrong. Wanna get the name of folder at that spot
//                    //how the hell to determine premium or regular
//                };

//                string[] orders = Directory.GetDirectories(users[i]);

//                for (int j = 0; j < orders.Length; j++)
//                {

//                }
//                return new List<User>;
//            }



//            string[] filePaths = Directory.GetFiles(Filepath); //this is the last stage.

//            //for (int i = 0; i < filePaths.??; i++)
//        }

//        public User GetUser(string username)
//        {
//            if (Users.Any(u => u.Username == username))
//            {
//                User user = Users.Single(u => u.Username == username);
//                return user;
//            }
//            else
//            {
//                return null;
//            }
//        }
//        //public User PopulateUserInfo();
//    }

//}
