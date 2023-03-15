using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class WorkflowResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Listing> Listings { get; set; }
        public List<string> Amenities { get; set; }
        //public List<User> Users { get; set; }
    }
}
