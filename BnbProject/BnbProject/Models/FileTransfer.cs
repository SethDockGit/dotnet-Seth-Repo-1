using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BnbProject.Models
{
    public class FileTransfer
    {
        public List<IFormFile> Files { get; set; }

        //public int ListingId { get; set; }
    }
}
