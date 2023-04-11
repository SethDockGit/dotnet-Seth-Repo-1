using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class GetListingsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Listing> Listings { get; set; }
    }
}
