using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class ListingResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Listing Listing { get; set; }
    }
}
