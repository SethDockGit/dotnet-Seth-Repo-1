using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class StayTransfer
    {
        public int GuestId { get; set; }
        public int HostId { get; set; }
        public int ListingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
