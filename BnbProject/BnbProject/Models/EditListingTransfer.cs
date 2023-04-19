using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BnbProject.Models
{
    public class EditListingTransfer
    {
        public int Id { get; set; }
        public int HostId { get; set; }
        public string Title { get; set; }
        public decimal Rate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; }

    }
}
