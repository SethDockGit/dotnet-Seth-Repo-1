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
        public Listing Listing { get; set; }
        public List<string> Amenities { get; set; }
        public UserAccount User { get; set; }
    }
}
