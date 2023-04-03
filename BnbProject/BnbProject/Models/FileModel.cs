using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BnbProject.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
