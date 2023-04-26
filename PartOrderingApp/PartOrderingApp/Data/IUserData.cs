using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public interface IUserData
    {
        public User GetUser(string username);
        public void ReWriteFile();
    }
}
