using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class Stay
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int HostId { get; set; }
        public int ListingId { get; set; }
        public Review Review { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
