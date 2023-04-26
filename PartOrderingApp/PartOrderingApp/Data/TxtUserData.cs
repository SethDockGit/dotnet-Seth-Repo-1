using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class TxtUserData : IUserData
    {
        public User User { get; set; }

        private string Filepath { get; set; }

        private string SaveFile { get; set; }

        public TxtUserData()
        {
            Filepath = @"C:\Data\PartOrderingApp\";
        }

        public User GetUser(string username)
        {

            User user = new User();

            string[] users = Directory.GetDirectories(Filepath);

            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] == "C:\\Data\\PartOrderingApp\\" + username)
                {
                    user.Username = username;

                    user.UserFilepath = "C:\\Data\\PartOrderingApp\\" + username; //property not neccessary, could just pass the string to GetUserInfo

                    User = user;

                    GetUserInfo();

                    return User;
                }
            }
            return null;

        }
        private void GetUserInfo() 
        {
           
            GetCategoryAndSaveFile();

            using (StreamReader sr = File.OpenText(SaveFile))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    string[] orders = line.Split('*');

                    for (int i = 1; i < orders.Length; i++)  
                    {
                        Order order = new Order();

                        order.Parts = new List<Part>();

                        string[] orderProps = orders[i].Split('&'); 

                        order.OrderID = int.Parse(orderProps[0]);  

                        order.DateTime = DateTime.Parse(orderProps[1]);

                        string[] partsAndTotal = orderProps[2].Split('%');

                        order.Total = decimal.Parse(partsAndTotal[0]);

                        for (int j = 1; j < partsAndTotal.Length; j++) 
                        {
                            Part part = new Part();

                            string[] partProps = partsAndTotal[j].Split('#');

                            part.Id = int.Parse(partProps[0]);

                            Part revised = SetPartProps(part.Id, part);

                            revised.SerialNumber = int.Parse(partProps[1]);

                            order.Parts.Add(revised);

                        }

                        User.Orders.Add(order);

                    }
                }
            }
        }
        private Part SetPartProps(int id, Part part) 
        {
         
            if(id == 1)
            {
                part.Name = "GTX 5020";
                part.Category = PartCategory.GPU;
                part.Cost = 900.40m;
            }
            if (id == 2)
            {
                part.Name = "GTX 4020";
                part.Category = PartCategory.GPU;
                part.Cost = 400.23m;
            }
            if (id == 3)
            {
                part.Name = "Horizon 3050";
                part.Category = PartCategory.CPU;
                part.Cost = 500.53m;
            }
            if (id == 4)
            {
                part.Name = "Horizon 2050";
                part.Category = PartCategory.CPU;
                part.Cost = 200.74m;
            }
            if (id == 5)
            {
                part.Name = "Aero Case";
                part.Category = PartCategory.Case;
                part.Cost = 152.81m;
            }
            if (id == 6)
            {
                part.Name = "Cardboard Box";
                part.Category = PartCategory.Case;
                part.Cost = 3.33m;
            }
            if (id == 7)
            {
                part.Name = "AMX 1050";
                part.Category = PartCategory.Motherboard;
                part.Cost = 329.35m;
            }
            if (id == 8)
            {
                part.Name = "AMX 2040";
                part.Category = PartCategory.Motherboard;
                part.Cost = 632.05m;
            }
            return part;
        }
        public void ReWriteFile()
        {
            File.Delete(SaveFile);

            File.Create(SaveFile).Close();

            foreach (Order order in User.Orders)
            {
                if (User.Orders.Count > 0)
                {
                    using (StreamWriter sw = File.AppendText(SaveFile))
                    {
                        if (order != User.Orders.First())
                        {
                            sw.Write("\n");
                        }
                        sw.Write($"*{order.OrderID}&{order.DateTime}&{order.Total}");

                        string parts = "";

                        foreach(Part part in order.Parts)
                        {
                            sw.Write($"%{part.Id}#{part.SerialNumber}");
                        }
                    }
                }
            }
        }
        private void GetCategoryAndSaveFile()
        {
            string[] fileNames = Directory.GetFiles(User.UserFilepath);

            if (fileNames[0] == User.UserFilepath + "\\Premium.txt")
            {
                User.Category = UserCategory.Premium;
                SaveFile = "C:\\Data\\PartOrderingApp\\" + User.Username + "\\Premium.txt";
            }
            else
            {
                User.Category = UserCategory.Regular;
                SaveFile = "C:\\Data\\PartOrderingApp\\" + User.Username + "\\Regular.txt";
            }
        }
    }

}
