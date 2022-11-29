using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class TestUserData : IUserData
    {
        public List<User> User { get; set; }
    }
}
