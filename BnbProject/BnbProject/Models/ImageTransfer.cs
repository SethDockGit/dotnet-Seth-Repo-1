using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BnbProject.Models
{
    public class ImageTransfer
    {
        public IFormFile ImageFile { get; set; }
        public int ListingId { get; set; }
    }
}
