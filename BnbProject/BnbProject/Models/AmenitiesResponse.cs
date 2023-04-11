using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class AmenitiesResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Amenities { get; set; }
    }
}
